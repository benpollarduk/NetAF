using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Commands.Scene;
using NetAF.Extensions;
using NetAF.Interpretation;
using NetAF.Logic.Arrangement;
using NetAF.Logic.Modes;
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
        /// Get the catalog of assets for this game.
        /// </summary>
        public AssetCatalog Catalog { get; private set; }

        /// <summary>
        /// Get the mode.
        /// </summary>
        public IGameMode Mode { get; private set; }

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
        /// Change mode to a specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public void ChangeMode(IGameMode mode)
        {
            Mode = mode;
        }

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
            // if the game is in an active state don't re-execute
            switch (State)
            {
                case GameState.Active:
                case GameState.Finishing:
                    return;
            }

            // enter the game
            Enter();

            // setup the adapter for this game
            Configuration.Adapter.Setup(this);

            // change mode to show the title screen
            ChangeMode(new TitleMode());

            // hold end mode
            IGameMode endMode;

            do
            {
                // always render the current mode
                RenderCurrentMode();

                // get the input
                var input = GetInput();

                // process the input
                var reaction = ProcessInput(input);

                // if the reaction should be displayed
                if (reaction.Result != ReactionResult.Silent)
                {
                    // display the reaction now
                    DisplayReaction(reaction);
                }
                else if (reaction.Result != ReactionResult.GameModeChanged && Mode.Type == GameModeType.Information)
                {
                    // revert back to scene mode as the command didn't change the mode and the current mode is information, essentially the mode has expired
                    ChangeMode(new SceneMode());
                }

                // check if the game has ended
                if (CheckForGameEnd(EndConditions, out endMode))
                    End();
            }
            while (State != GameState.Finishing);

            // render the last mode
            RenderCurrentMode();

            // wait for acknowledge
            GetInput();

            // if an end mode specified
            if (endMode != null)
            {
                // set and render the end mode
                ChangeMode(endMode);
                RenderCurrentMode();

                // wait for acknowledge
                GetInput();
            }

            // finished execution
            State = GameState.Finished;
        }

        /// <summary>
        /// Render the current mode.
        /// </summary>
        private void RenderCurrentMode()
        {
            // perform the render
            var result = Mode.Render(this);

            // if this was aborted by the mode
            if (result == RenderState.Aborted)
            {
                // revert back to scene mode
                ChangeMode(new SceneMode());

                // render
                Mode.Render(this);
            }
        }

        /// <summary>
        /// Check to see if the game has ended.
        /// </summary>
        /// <param name="endConditions">The end conditions.</param>
        /// <param name="mode">The game mode.</param>
        /// <returns>True if the game has ended, else false.</returns>
        private bool CheckForGameEnd(GameEndConditions endConditions, out IGameMode mode)
        {
            // check to see if the completion conditions have been met
            var completionCheckResult = endConditions.CompletionCondition(this) ?? EndCheckResult.NotEnded;
            var gameOverCheckResult = endConditions.GameOverCondition(this) ?? EndCheckResult.NotEnded;

            // check conditions and set end mode appropriately
            if (completionCheckResult.HasEnded)
                mode = new CompletionMode(completionCheckResult.Title, completionCheckResult.Description);
            else if (gameOverCheckResult.HasEnded)
                mode = new GameOverMode(gameOverCheckResult.Title, gameOverCheckResult.Description);
            else
                mode = null;

            // return if either condition was true
            return completionCheckResult.HasEnded || gameOverCheckResult.HasEnded;
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
                Overworld.CurrentRegion.JumpToRoom(location.Position);
            }
        }

        /// <summary>
        /// Get input from the user.
        /// </summary>
        /// <returns>The user input.</returns>
        private string GetInput()
        {
            // input is handled based on the current modes type
            switch (Mode.Type)
            {
                case GameModeType.Information:

                    // wait for acknowledge
                    while (!Configuration.Adapter.WaitForAcknowledge())
                    {
                        // something other was entered, render again unless that was aborted
                        if (Mode.Render(this) != RenderState.Aborted)
                            break;
                    }

                    // acknowledge complete
                    return string.Empty;

                case GameModeType.Interactive:

                    // get and return user input
                    return Configuration.Adapter.WaitForInput();

                default:
                    throw new NotImplementedException($"No handling for case {Mode.Type}.");
            }
        }

        /// <summary>
        /// Process input.
        /// </summary>
        /// <param name="input">The input to process.</param>
        /// <returns>The reaction to the input.</returns>
        private Reaction ProcessInput(string input)
        {
            // if just an information mode no additional processing needed
            if (Mode.Type == GameModeType.Information)
                return new Reaction(ReactionResult.Silent, string.Empty);

            // preen input to help with processing
            input = StringUtilities.PreenInput(input);

            // try global interpreter
            var interpretation = Configuration.Interpreter.Interpret(input, this) ?? new InterpretationResult(false, new Unactionable("No interpreter."));

            // if interpretation was successful then process
            if (interpretation.WasInterpretedSuccessfully)
                return interpretation.Command.Invoke(this);

            // try mode interpreter
            if (Mode.Interpreter != null)
            {
                // try mode specific interpreter
                interpretation = Mode.Interpreter.Interpret(input, this);

                // if interpretation was successful then process
                if (interpretation.WasInterpretedSuccessfully)
                    return interpretation.Command.Invoke(this);
            }

            // something was entered, but can't be processed
            if (!string.IsNullOrEmpty(input))
                return new(ReactionResult.Error, $"{input} was not valid input.");

            // empty string
            return new(ReactionResult.Silent, string.Empty);
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
                    ChangeMode(new ReactionMode(Overworld.CurrentRegion.CurrentRoom.Identifier.Name, message));
                    break;
                case ReactionResult.Silent:
                case ReactionResult.GameModeChanged:
                    break;
                case ReactionResult.Inform:
                    ChangeMode(new ReactionMode(Overworld.CurrentRegion.CurrentRoom.Identifier.Name, reaction.Description));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Enter the game.
        /// </summary>
        private void Enter()
        {
            State = GameState.Active;
        }

        /// <summary>
        /// End the game.
        /// </summary>
        internal void End()
        {
            State = GameState.Finishing;
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