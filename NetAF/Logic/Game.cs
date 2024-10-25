﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Commands.Game;
using NetAF.Extensions;
using NetAF.Interpretation;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.Frames;
using NetAF.Utilities;

namespace NetAF.Logic
{
    /// <summary>
    /// Represents the structure of the game
    /// </summary>
    public sealed class Game
    {
        #region Constants

        /// <summary>
        /// Get the default error prefix.
        /// </summary>
        public const string DefaultErrorPrefix = "Oops";

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get the default interpreter.
        /// </summary>
        public static IInterpreter DefaultInterpreter => new InputInterpreter(
            new FrameCommandInterpreter(),
            new GlobalCommandInterpreter(),
            new GameCommandInterpreter(), 
            new CustomCommandInterpreter(),
            new ConversationCommandInterpreter());

        /// <summary>
        /// Get the default size.
        /// </summary>
        public static Size DefaultSize { get; } = new Size(80, 50);

        #endregion

        #region Fields

        private FrameBuilderCollection frameBuilders;

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
        /// Get or set if the command list is displayed in scene frames.
        /// </summary>
        public bool DisplayCommandListInSceneFrames { get; set; } = true;

        /// <summary>
        /// Get or set the type of key to use on the scene map.
        /// </summary>
        public KeyType SceneMapKeyType { get; set; } = KeyType.Dynamic;

        /// <summary>
        /// Get the player.
        /// </summary>
        public PlayableCharacter Player { get; private set; }

        /// <summary>
        /// Get the overworld.
        /// </summary>
        public Overworld Overworld { get; }

        /// <summary>
        /// Get the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Get the introduction.
        /// </summary>
        public string Introduction { get; }

        /// <summary>
        /// Get the description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Get or set the name of the author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Get or set the error prefix.
        /// </summary>
        public string ErrorPrefix { get; set; }

        /// <summary>
        /// Get if this is executing.
        /// </summary>
        public bool IsExecuting { get; private set; }

        /// <summary>
        /// Get or set the interpreter.
        /// </summary>
        private IInterpreter Interpreter { get; set; }

        /// <summary>
        /// Get the size of the display area.
        /// </summary>
        public Size DisplaySize { get; }

        /// <summary>
        /// Get or set the exit mode for this game.
        /// </summary>
        internal ExitMode ExitMode { get; set; } = ExitMode.ReturnToTitleScreen;

        /// <summary>
        /// Get or set the collection of frame builders used to render this game.
        /// </summary>
        public FrameBuilderCollection FrameBuilders
        {
            get { return frameBuilders; }
            set
            {
                frameBuilders = value;

                if (State == GameState.Active)
                    Refresh(CurrentFrame);
            }
        }

        /// <summary>
        /// Get or set the current Frame.
        /// </summary>
        private IFrame CurrentFrame { get; set; }

        /// <summary>
        /// Get or set the completion condition.
        /// </summary>
        internal EndCheck CompletionCondition { get; set; }

        /// <summary>
        /// Get or set the game over condition.
        /// </summary>
        internal EndCheck GameOverCondition { get; set; }

        /// <summary>
        /// Get or set the adapter for the console.
        /// </summary>
        internal IConsoleAdapter Adapter { get; set; } = new SystemConsoleAdapter();

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
        /// <param name="name">The name of this game.</param>
        /// <param name="introduction">An to the game.</param>
        /// <param name="description">A description of this game.</param>
        /// <param name="player">The Player to use for this game.</param>
        /// <param name="overworld">An Overworld to use for this game.</param>
        /// <param name="displaySize">The display size.</param>
        private Game(string name, string introduction, string description, PlayableCharacter player, Overworld overworld, Size displaySize)
        {
            Name = name;
            Introduction = introduction;
            Description = description;
            Player = player;
            Overworld = overworld;
            DisplaySize = displaySize;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Setup the adapter.
        /// </summary>
        private void SetupAdapter()
        {
            Adapter.Setup(this);
            StartingFrameDraw -= Game_StartingFrameDraw;
            FinishedFrameDraw -= Game_FinishedFrameDraw;
            StartingFrameDraw += Game_StartingFrameDraw;
            FinishedFrameDraw += Game_FinishedFrameDraw;
        }

        /// <summary>
        /// Change to a specified player.
        /// </summary>
        /// <param name="player">The player to change to.</param>
        public void ChangePlayer(PlayableCharacter player)
        {
            Player = player;
        }

        /// <summary>
        /// Execute the game.
        /// </summary>
        internal void Execute()
        {
            if (IsExecuting)
                return;

            IsExecuting = true;

            SetupAdapter();
            
            Refresh(FrameBuilders.TitleFrameBuilder.Build(Name, Introduction, DisplaySize.Width, DisplaySize.Height));

            do
            {
                if (ActiveConverser != null)
                    Refresh(FrameBuilders.ConversationFrameBuilder.Build($"Conversation with {ActiveConverser.Identifier.Name}", ActiveConverser, Interpreter?.GetContextualCommandHelp(this), DisplaySize.Width, DisplaySize.Height));

                var input = GetInput();
                var reaction = ExecuteLogicOnce(input, out var displayReactionToInput);

                if (reaction?.Result == ReactionResult.Fatal)
                    Player?.Kill();

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
        /// Get input from the user.
        /// </summary>
        /// <returns>The user input.</returns>
        private string GetInput()
        {
            if (CurrentFrame.AcceptsInput) 
                return Adapter.In.ReadLine();
            
            var frame = CurrentFrame;

            while (!Adapter.WaitForKeyPress(StringUtilities.CR) && CurrentFrame == frame)
                DrawFrame(CurrentFrame);

            return string.Empty;
        }

        /// <summary>
        /// Test and handle the game over condition.
        /// </summary>
        /// <returns>True if the condition was met.</returns>
        private bool TestAndHandleGameOver()
        {
            var gameOverCheckResult = GameOverCondition(this) ?? EndCheckResult.NotEnded;

            if (!gameOverCheckResult.HasEnded)
                return false;

            GetInput();
            Refresh(FrameBuilders.GameOverFrameBuilder.Build(gameOverCheckResult.Title, gameOverCheckResult.Description, DisplaySize.Width, DisplaySize.Height));
            GetInput();
            End();
            
            return true;
        }

        /// <summary>
        /// Test and handle the completion condition.
        /// </summary>
        /// <returns>True if the condition was met.</returns>
        private bool TestAndHandleGameCompletion()
        {
            var endCheckResult = CompletionCondition(this) ?? EndCheckResult.NotEnded;

            if (!endCheckResult.HasEnded) 
                return false;

            GetInput();
            Refresh(FrameBuilders.CompletionFrameBuilder.Build(endCheckResult.Title, endCheckResult.Description, DisplaySize.Width, DisplaySize.Height));
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
                Refresh();
                displayReaction = false;
                return new Reaction(ReactionResult.OK, string.Empty);
            }

            displayReaction = true;
            input = StringUtilities.PreenInput(input);
            var interpretation = Interpreter?.Interpret(input, this) ?? new InterpretationResult(false, new Unactionable("No interpreter."));

            if (interpretation.WasInterpretedSuccessfully)
                return interpretation.Command.Invoke(this);

            if (!string.IsNullOrEmpty(input))
                return new Reaction(ReactionResult.Error, $"{input} was not valid input.");

            return new Reaction(ReactionResult.OK, string.Empty);
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
                    var message = ErrorPrefix + ": " + reaction.Description;
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

                frame.Render(Adapter.Out);

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
            Refresh(FrameBuilders.SceneFrameBuilder.Build(Overworld.CurrentRegion.CurrentRoom, ViewPoint.Create(Overworld.CurrentRegion), Player, string.Empty, DisplayCommandListInSceneFrames ? Interpreter.GetContextualCommandHelp(this) : null, SceneMapKeyType, DisplaySize.Width, DisplaySize.Height));
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

            if (Player.Items.Any(name.EqualsExaminable))
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
            return examinables.ToArray();
        }

        /// <summary>
        /// Refresh the current frame.
        /// </summary>
        private void Refresh()
        {
            Refresh(string.Empty);
        }

        /// <summary>
        /// Refresh the current frame.
        /// </summary>
        /// <param name="message">Any message to display.</param>
        private void Refresh(string message)
        {
            Refresh(FrameBuilders.SceneFrameBuilder.Build(Overworld.CurrentRegion.CurrentRoom, ViewPoint.Create(Overworld.CurrentRegion), Player, message, DisplayCommandListInSceneFrames ? Interpreter.GetContextualCommandHelp(this) : null, SceneMapKeyType, DisplaySize.Width, DisplaySize.Height));
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
            var commands = new List<CommandHelp>();
            commands.AddRange(Interpreter.SupportedCommands);
            commands.AddRange(Interpreter.GetContextualCommandHelp(this));

            Refresh(FrameBuilders.HelpFrameBuilder.Build("Help", string.Empty, commands.Distinct().ToArray(), DisplaySize.Width, DisplaySize.Height));
        }

        /// <summary>
        /// Display the map frame.
        /// </summary>
        public void DisplayMap()
        {
            Refresh(FrameBuilders.RegionMapFrameBuilder.Build(Overworld.CurrentRegion, DisplaySize.Width, DisplaySize.Height));
        }

        /// <summary>
        /// Display the about frame.
        /// </summary>
        public void DisplayAbout()
        {
            Refresh(FrameBuilders.AboutFrameBuilder.Build("About", this, DisplaySize.Width, DisplaySize.Height));
        }

        /// <summary>
        /// Display a transition frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void DisplayTransition(string title, string message)
        {
            Refresh(FrameBuilders.TransitionFrameBuilder.Build(title, message, DisplaySize.Width, DisplaySize.Height));
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new callback for generating instances of a game.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="introduction">An introduction to the game.</param>
        /// <param name="description">A description of the game.</param>
        /// <param name="overworldGenerator">A function to generate the overworld with.</param>
        /// <param name="playerGenerator">The function to generate the player with.</param>
        /// <param name="completionCondition">The callback used to check game completion.</param>
        /// <param name="gameOverCondition">The callback used to check game over.</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified.</returns>
        public static GameCreationCallback Create(string name, string introduction, string description, OverworldCreationCallback overworldGenerator, PlayerCreationCallback playerGenerator, EndCheck completionCondition, EndCheck gameOverCondition)
        {
            return Create(
                name,
                introduction,
                description,
                overworldGenerator,
                playerGenerator,
                completionCondition,
                gameOverCondition,
                DefaultSize,
                FrameBuilderCollections.Default,
                ExitMode.ReturnToTitleScreen,
                DefaultErrorPrefix,
                DefaultInterpreter);
        }

        /// <summary>
        ///  Create a new callback for generating instances of a game.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="introduction">An introduction to the game.</param>
        /// <param name="description">A description of the game.</param>
        /// <param name="overworldGenerator">A function to generate the overworld with.</param>
        /// <param name="playerGenerator">The function to generate the player with.</param>
        /// <param name="displaySize">The display size.</param>
        /// <param name="completionCondition">The callback used to check game completion.</param>
        /// <param name="gameOverCondition">The callback used to check game over.</param>
        /// <param name="frameBuilders">The collection of frame builders to use to render the game.</param>
        /// <param name="exitMode">The exit mode.</param>
        /// <param name="errorPrefix">A prefix to use when displaying errors.</param>
        /// <param name="interpreter">The interpreter.</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified.</returns>
        public static GameCreationCallback Create(string name, string introduction, string description, OverworldCreationCallback overworldGenerator, PlayerCreationCallback playerGenerator, EndCheck completionCondition, EndCheck gameOverCondition, Size displaySize, FrameBuilderCollection frameBuilders, ExitMode exitMode, string errorPrefix, IInterpreter interpreter)
        {
            return () =>
            {
                var game = new Game(name, introduction, description, playerGenerator?.Invoke(), overworldGenerator?.Invoke(), displaySize)
                {
                    FrameBuilders = frameBuilders,
                    CompletionCondition = completionCondition,
                    GameOverCondition = gameOverCondition,
                    ExitMode = exitMode,
                    ErrorPrefix = errorPrefix,
                    Interpreter = interpreter
                };

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

                switch (game.ExitMode)
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

        #region EventHandlers

        private void Game_FinishedFrameDraw(object sender, IFrame e)
        {
            Adapter.OnGameFinishedFrameDraw(e);
        }

        private void Game_StartingFrameDraw(object sender, IFrame e)
        {
            Adapter.OnGameStartedFrameDraw(e);
        }

        #endregion
    }
}