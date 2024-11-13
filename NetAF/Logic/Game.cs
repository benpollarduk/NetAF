using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Commands.Scene;
using NetAF.Extensions;
using NetAF.Interpretation;
using NetAF.Logic.Arrangement;
using NetAF.Rendering.Frames;
using NetAF.Serialization;
using NetAF.Utilities;

namespace NetAF.Logic
{
    /// <summary>
    /// Represents a game.
    /// </summary>
    public sealed class Game : IRestoreFromObjectSerialization<GameSerialization>
    {
        #region Fields

        private readonly List<PlayableCharacterLocation> inactivePlayerLocations = [];

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the current state.
        /// </summary>
        private GameState State { get; set; }

        /// <summary>
        /// Get the active converser.
        /// </summary>
        public IConverser ActiveConverser { get; private set; }

        /// <summary>
        /// Get the player.
        /// </summary>
        public PlayableCharacter Player { get; private set; }

        /// <summary>
        /// Get the overworld.
        /// </summary>
        public Overworld Overworld { get; }

        /// <summary>
        /// Get the info.
        /// </summary>
        public GameInfo Info { get; }

        /// <summary>
        /// Get the introduction.
        /// </summary>
        public string Introduction { get; }

        /// <summary>
        /// Get the configuration.
        /// </summary>
        public IGameConfiguration Configuration { get; private set; }

        /// <summary>
        /// Get the end conditions.
        /// </summary>
        public GameEndConditions EndConditions { get; private set; }

        /// <summary>
        /// Get if this is executing.
        /// </summary>
        public bool IsExecuting { get; private set; }

        /// <summary>
        /// Get or set the current Frame.
        /// </summary>
        private IFrame CurrentFrame { get; set; }

        /// <summary>
        /// Get the catalog of assets for this game.
        /// </summary>
        public AssetCatalog Catalog { get; private set; }

        /// <summary>
        /// Occurs when the game begins drawing a frame.
        /// </summary>
        internal event EventHandler<IFrame> StartingFrameDraw;

        /// <summary>
        /// Occurs when the game finishes drawing a frame.
        /// </summary>
        internal event EventHandler<IFrame> FinishedFrameDraw;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Game class.
        /// </summary>
        /// <param name="info">The information about this game..</param>
        /// <param name="introduction">An introduction to the game.</param>
        /// <param name="player">The playable character for this game.</param>
        /// <param name="overworld">The games overworld.</param>
        /// <param name="endConditions">The games end conditions.</param>
        /// <param name="configuration">The configuration to use for this game.</param>
        private Game(GameInfo info, string introduction, PlayableCharacter player, Overworld overworld, GameEndConditions endConditions, IGameConfiguration configuration)
        {
            Info = info;
            Introduction = introduction;
            Player = player;
            Overworld = overworld;
            Configuration = configuration;
            EndConditions = endConditions;
            Catalog = AssetCatalog.FromGame(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get an array of inactive player locations.
        /// </summary>
        /// <returns>An array containing all locations of inactive platers.</returns>
        public PlayableCharacterLocation[] GetInactivePlayerLocations()
        {
            return [.. inactivePlayerLocations];
        }

        /// <summary>
        /// Execute the game.
        /// </summary>
        internal void Execute()
        {
            if (IsExecuting)
                return;

            IsExecuting = true;

            Configuration.Adapter.Setup(this);

            Refresh(Configuration.FrameBuilders.TitleFrameBuilder.Build(Info.Name, Introduction, Configuration.DisplaySize.Width, Configuration.DisplaySize.Height));

            do
            {
                if (ActiveConverser != null)
                    Refresh(Configuration.FrameBuilders.ConversationFrameBuilder.Build($"Conversation with {ActiveConverser.Identifier.Name}", ActiveConverser, Configuration.Interpreter.GetContextualCommandHelp(this), Configuration.DisplaySize.Width, Configuration.DisplaySize.Height));

                var input = GetInput();
                var reaction = ExecuteLogicOnce(input, out var displayReactionToInput);

                if (reaction?.Result == ReactionResult.Fatal)
                    Player.Kill();

                if (displayReactionToInput)
                    DisplayReaction(reaction);

                var complete = TestAndHandleGameCompletion();

                if (!complete)
                    TestAndHandleGameOver();
            }
            while (State != GameState.Finished);

            IsExecuting = false;
        }

        /// <summary>
        /// Execute game logic once to get a reaction.
        /// </summary>
        /// <param name="input">The input to process.</param>
        /// <param name="displayReaction">Will be set to true if the reaction should be displayed.</param>
        /// <returns>The reaction to the input.</returns>
        private Reaction ExecuteLogicOnce(string input, out bool displayReaction)
        {
            switch (State)
            {
                case GameState.NotStarted:
                    Enter();
                    displayReaction = false;
                    return null;
                case GameState.Finished:
                    displayReaction = false;
                    return null;
                case GameState.Active:
                    return ProcessInput(input, out displayReaction);
                default:
                    throw new ArgumentOutOfRangeException($"State {State} is not handled.");
            }
        }

        /// <summary>
        /// Change to a specified player.
        /// </summary>
        /// <param name="player">The player to change to.</param>
        /// <param name="jumpToLastLocation">Jump to the last location, if it is known. Then true the player will be added at the last location, when false the current location will be used. By default this is true.</param>
        public void ChangePlayer(PlayableCharacter player, bool jumpToLastLocation = true)
        {
            if (player == null)
                return;

            if (Player == player)
                return;

            inactivePlayerLocations.Add(new(Player.Identifier.IdentifiableName, Overworld.CurrentRegion.Identifier.IdentifiableName, Overworld.CurrentRegion.CurrentRoom.Identifier.IdentifiableName));

            var previous = Array.Find(inactivePlayerLocations.ToArray(), x => player.Identifier.Equals(x.PlayerIdentifier));

            if (previous != null)
                inactivePlayerLocations.Remove(previous);

            Player = player;

            if (jumpToLastLocation && previous?.RegionIdentifier != null && previous.RoomIdentifier != null)
            {
                var region = Array.Find(Overworld.Regions.ToArray(), x => x.Identifier.Equals(previous.RegionIdentifier));
                var room = Array.Find(region.ToMatrix().ToRooms(), x => x.Identifier.Equals(previous.RoomIdentifier));

                Overworld.Move(region);
                var location = Overworld.CurrentRegion.GetPositionOfRoom(room);
                Overworld.CurrentRegion.JumpToRoom(location.X, location.Y, location.Z);
            }
        }

        /// <summary>
        /// Get input from the user.
        /// </summary>
        /// <returns>The user input.</returns>
        private string GetInput()
        {
            if (CurrentFrame.AcceptsInput)
                return Configuration.Adapter.WaitForInput();
            
            while (!Configuration.Adapter.WaitForAcknowledge())
                DrawFrame(CurrentFrame);

            return string.Empty;
        }

        /// <summary>
        /// Test and handle the game over condition.
        /// </summary>
        private void TestAndHandleGameOver()
        {
            var gameOverCheckResult = EndConditions.GameOverCondition(this) ?? EndCheckResult.NotEnded;

            if (!gameOverCheckResult.HasEnded)
                return;

            GetInput();
            Refresh(Configuration.FrameBuilders.GameOverFrameBuilder.Build(gameOverCheckResult.Title, gameOverCheckResult.Description, Configuration.DisplaySize.Width, Configuration.DisplaySize.Height));
            GetInput();
            End();
        }

        /// <summary>
        /// Test and handle the completion condition.
        /// </summary>
        /// <returns>True if the condition was met.</returns>
        private bool TestAndHandleGameCompletion()
        {
            var endCheckResult = EndConditions.CompletionCondition(this) ?? EndCheckResult.NotEnded;

            if (!endCheckResult.HasEnded) 
                return false;

            GetInput();
            Refresh(Configuration.FrameBuilders.CompletionFrameBuilder.Build(endCheckResult.Title, endCheckResult.Description, Configuration.DisplaySize.Width, Configuration.DisplaySize.Height));
            GetInput();
            End();

            return true;
        }

        /// <summary>
        /// Process input to get a reaction.
        /// </summary>
        /// <param name="input">The input to process.</param>
        /// <param name="displayReaction">Will be set to true if the reaction should be displayed.</param>
        /// <returns>The reaction to the input.</returns>
        private Reaction ProcessInput(string input, out bool displayReaction)
        {
            if (!CurrentFrame.AcceptsInput)
            {
                Refresh(string.Empty);
                displayReaction = false;
                return new Reaction(ReactionResult.OK, string.Empty);
            }

            displayReaction = true;
            input = StringUtilities.PreenInput(input);
            var interpretation = Configuration.Interpreter.Interpret(input, this) ?? new InterpretationResult(false, new Unactionable("No interpreter."));

            if (interpretation.WasInterpretedSuccessfully)
                return interpretation.Command.Invoke(this);

            if (!string.IsNullOrEmpty(input))
                return new(ReactionResult.Error, $"{input} was not valid input.");

            return new(ReactionResult.OK, string.Empty);
        }

        /// <summary>
        /// Display a reaction.
        /// </summary>
        /// <param name="reaction">The reaction to handle.</param>
        private void DisplayReaction(Reaction reaction)
        {
            switch (reaction.Result)
            {
                case ReactionResult.Error:
                    var message = Configuration.ErrorPrefix + ": " + reaction.Description;
                    Refresh(message);
                    break;
                case ReactionResult.OK:
                    Refresh(reaction.Description);
                    break;
                case ReactionResult.Internal:
                    break;
                case ReactionResult.Fatal:
                    Refresh(reaction.Description);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Start a conversation with a converser.
        /// </summary>
        /// <param name="converser">The element to engage conversation with.</param>
        internal void StartConversation(IConverser converser)
        {
            ActiveConverser = converser;
            ActiveConverser?.Conversation?.Next(this);
        }

        /// <summary>
        /// End a conversation with a converser.
        /// </summary>
        internal void EndConversation()
        {
            ActiveConverser = null;
        }

        /// <summary>
        /// Draw a Frame onto the output stream.
        /// </summary>
        /// <param name="frame">The frame to draw.</param>
        private void DrawFrame(IFrame frame)
        {
            try
            {
                StartingFrameDraw?.Invoke(this, frame);
                Configuration.Adapter.RenderFrame(frame);
                FinishedFrameDraw?.Invoke(this, frame);
            }
            catch (Exception e)
            {
                Debug.WriteLine("An exception was caught drawing the frame: {0}", e.Message);
            }
        }

        /// <summary>
        /// Enter the game.
        /// </summary>
        private void Enter()
        {
            State = GameState.Active;
            Refresh(Configuration.FrameBuilders.SceneFrameBuilder.Build(Overworld.CurrentRegion.CurrentRoom, ViewPoint.Create(Overworld.CurrentRegion), Player, string.Empty, Configuration.DisplayCommandListInSceneFrames ? Configuration.Interpreter.GetContextualCommandHelp(this) : null, Configuration.SceneMapKeyType, Configuration.DisplaySize.Width, Configuration.DisplaySize.Height));
        }

        /// <summary>
        /// End the game.
        /// </summary>
        internal void End()
        {
            State = GameState.Finished;
        }

        /// <summary>
        /// Find an interaction target within the current scope for this Game.
        /// </summary>
        /// <param name="name">The targets name.</param>
        /// <returns>The first IInteractWithItem object which has a name that matches the name parameter.</returns>
        public IInteractWithItem FindInteractionTarget(string name)
        {
            if (name.EqualsExaminable(Player))
                return Player;

            if (Array.Exists(Player.Items, name.EqualsExaminable))
            {
                Player.FindItem(name, out var i);
                return i;
            }

            if (name.EqualsExaminable(Overworld.CurrentRegion.CurrentRoom))
                return Overworld.CurrentRegion.CurrentRoom;

            if (!Overworld.CurrentRegion.CurrentRoom.ContainsInteractionTarget(name))
                return null;

            Overworld.CurrentRegion.CurrentRoom.FindInteractionTarget(name, out var target);
            return target;
        }

        /// <summary>
        /// Get all examinables that are currently visible to the player.
        /// </summary>
        /// <returns>An array of all examinables that are currently visible to the player.</returns>
        public IExaminable[] GetAllPlayerVisibleExaminables()
        {
            var examinables = new List<IExaminable> { Player, Overworld, Overworld.CurrentRegion, Overworld.CurrentRegion.CurrentRoom };
            examinables.AddRange(Player.Items.Where(x => x.IsPlayerVisible));
            examinables.AddRange(Overworld.CurrentRegion.CurrentRoom.Items.Where(x => x.IsPlayerVisible));
            examinables.AddRange(Overworld.CurrentRegion.CurrentRoom.Characters.Where(x => x.IsPlayerVisible));
            examinables.AddRange(Overworld.CurrentRegion.CurrentRoom.Exits.Where(x => x.IsPlayerVisible));
            return [.. examinables];
        }

        /// <summary>
        /// Refresh the current frame.
        /// </summary>
        /// <param name="message">Any message to display.</param>
        private void Refresh(string message)
        {
            Refresh(Configuration.FrameBuilders.SceneFrameBuilder.Build(Overworld.CurrentRegion.CurrentRoom, ViewPoint.Create(Overworld.CurrentRegion), Player, message, Configuration.DisplayCommandListInSceneFrames ? Configuration.Interpreter.GetContextualCommandHelp(this) : null, Configuration.SceneMapKeyType, Configuration.DisplaySize.Width, Configuration.DisplaySize.Height));
        }

        /// <summary>
        /// Refresh the display.
        /// </summary>
        /// <param name="frame">The frame to display.</param>
        private void Refresh(IFrame frame)
        {
            CurrentFrame = frame;
            DrawFrame(frame);
        }

        /// <summary>
        /// Display the help frame.
        /// </summary>
        public void DisplayHelp()
        {
            List<CommandHelp> commands =
            [
                .. Configuration.Interpreter.SupportedCommands,
                .. Configuration.Interpreter.GetContextualCommandHelp(this),
            ];

            Refresh(Configuration.FrameBuilders.HelpFrameBuilder.Build("Help", string.Empty, commands.Distinct().ToArray(), Configuration.DisplaySize.Width, Configuration.DisplaySize.Height));
        }

        /// <summary>
        /// Display the map frame.
        /// </summary>
        public void DisplayMap()
        {
            Refresh(Configuration.FrameBuilders.RegionMapFrameBuilder.Build(Overworld.CurrentRegion, Configuration.DisplaySize.Width, Configuration.DisplaySize.Height));
        }

        /// <summary>
        /// Display the about frame.
        /// </summary>
        public void DisplayAbout()
        {
            Refresh(Configuration.FrameBuilders.AboutFrameBuilder.Build("About", this, Configuration.DisplaySize.Width, Configuration.DisplaySize.Height));
        }

        /// <summary>
        /// Display a transition frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void DisplayTransition(string title, string message)
        {
            Refresh(Configuration.FrameBuilders.TransitionFrameBuilder.Build(title, message, Configuration.DisplaySize.Width, Configuration.DisplaySize.Height));
        }

        #endregion

        #region StaticMethods

        /// <summary>
        ///  Create a new callback for generating instances of a game.
        /// </summary>
        /// <param name="info">Information about the game.</param>
        /// <param name="introduction">An introduction to the game.</param>
        /// <param name="assetGenerator">The generator to use to create game assets.</param>
        /// <param name="conditions">The game conditions.</param>
        /// <param name="configuration">The configuration for the game.</param>
        /// <param name="setup">A setup function to run on the created game after it has been created.</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified.</returns>
        public static GameCreationCallback Create(GameInfo info, string introduction, AssetGenerator assetGenerator, GameEndConditions conditions, IGameConfiguration configuration, GameSetupCallback setup = null)
        {
            return () =>
            {
                var game = new Game(info, introduction, assetGenerator.GetPlayer(), assetGenerator.GetOverworld(), conditions, configuration);
                setup?.Invoke(game);
                return game;
            };
        }

        /// <summary>
        /// Execute a game.
        /// </summary>
        /// <param name="creator">The creator to use to create the game.</param>
        public static void Execute(GameCreationCallback creator)
        {
            var run = true;

            while (run)
            {
                var game = creator.Invoke();
                game.Execute();

                switch (game.Configuration.ExitMode)
                {
                    case ExitMode.ExitApplication:
                        run = false;
                        break;
                    case ExitMode.ReturnToTitleScreen:
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<GameSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        public void RestoreFrom(GameSerialization serialization)
        {
            // resolve asset locations
            AssetArranger.Arrange(this, serialization);

            // restore all inactive player locations
            foreach (var location in serialization.InactivePlayerLocations)
                inactivePlayerLocations.Add(PlayableCharacterLocation.FromSerialization(location));

            // restore all players
            foreach (var player in serialization.Players)
            {
                var match = Array.Find(Catalog.Players, x => x.Identifier.Equals(player.Identifier));
                match?.RestoreFrom(player);
            }

            // restore active player
            Player = Array.Find(Catalog.Players, x => x.Identifier.Equals(serialization.ActivePlayerIdentifier));

            // restore overworld
            serialization.Overworld.Restore(Overworld);

            // restore player locations
            inactivePlayerLocations.Clear();
        }

        #endregion
    }
}