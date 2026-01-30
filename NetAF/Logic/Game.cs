using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Commands.Scene;
using NetAF.Extensions;
using NetAF.Interpretation;
using NetAF.Logging.Events;
using NetAF.Logging.History;
using NetAF.Logging.Notes;
using NetAF.Logic.Arrangement;
using NetAF.Logic.Callbacks;
using NetAF.Logic.Modes;
using NetAF.Serialization;
using NetAF.Serialization.Assets;
using NetAF.Utilities;
using NetAF.Variables;

namespace NetAF.Logic
{
    /// <summary>
    /// Represents a game.
    /// </summary>
    public sealed class Game : IRestoreFromObjectSerialization<GameSerialization>
    {
        #region Fields

        private readonly List<PlayableCharacterLocation> inactivePlayerLocations = [];
        private IGameMode endMode;

        #endregion

        #region Properties

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
        public GameConfiguration Configuration { get; private set; }

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

        /// <summary>
        /// Get the game state.
        /// </summary>
        internal GameState State { get; private set; }

        /// <summary>
        /// Get the note manager.
        /// </summary>
        public NoteManager NoteManager { get; private set; } = new NoteManager();

        /// <summary>
        /// Get the history manager.
        /// </summary>
        public HistoryManager HistoryManager { get; private set; } = new HistoryManager();

        /// <summary>
        /// Get the variable manager.
        /// </summary>
        public VariableManager VariableManager { get; private set; } = new VariableManager();

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
        private Game(GameInfo info, string introduction, PlayableCharacter player, Overworld overworld, GameEndConditions endConditions, GameConfiguration configuration)
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
        /// Update to the next frame of the game.
        /// </summary>
        /// <param name="input">Any input that should be passed to the game.</param>
        /// <returns>The result of the action.</returns>
        internal UpdateResult Update(string input = "")
        {
            var result = State switch
            {
                GameState.NotStarted => UpdateWhenNotStarted(this),
                GameState.Active => UpdateWhenActive(this, input),
                GameState.EndConditionMet => UpdateWhenEndConditionMet(this),
                GameState.Finishing => UpdateWhenFinishing(this),
                GameState.Finished => UpdateWhenFinished(this),
                _ => new(false, $"Cannot move to next when state is {State}."),
            };

            EventBus.Publish(new GameUpdated(this));

            return result;
        }

        /// <summary>
        /// Handle a reaction.
        /// </summary>
        /// <param name="reaction">The reaction to handle.</param>
        private void HandleReaction(Reaction reaction)
        {
            // 1. check if needed to display the reaction
            if (reaction.Result == ReactionResult.Error || reaction.Result == ReactionResult.Inform)
            {
                // log
                HistoryManager.Add(reaction.Description, reaction.Description);

                // display the reaction
                ChangeMode(new ReactionMode(reaction));
                return;
            }
            
            // 2. check that there is a room - it may be that the region changed and needs to be entered
            if (Overworld.CurrentRegion.CurrentRoom == null)
            {
                // enter the region
                var regionEnterReaction = Overworld.CurrentRegion.Enter();

                // if the reaction wasn't silent then show reaction, else revert back to scene mode
                if (regionEnterReaction.Result != ReactionResult.Silent)
                {
                    // log the introduction
                    HistoryManager.Add(reaction.Description, reaction.Description);

                    // change mode to display the reaction
                    ChangeMode(new ReactionMode(regionEnterReaction));
                }
                else
                {
                    // revert to scene
                    ChangeMode(new SceneMode(Configuration.InterpreterProvider.Find(typeof(SceneMode))));
                }

                return;
            }

            // 3. check if command didn't change the mode and the current mode type is single frame information, essentially the mode has expired
            if (reaction.Result != ReactionResult.GameModeChanged && Mode.Type == GameModeType.SingleFrameInformation)
            {
                // revert back to scene mode as the 
                ChangeMode(new SceneMode(Configuration.InterpreterProvider.Find(typeof(SceneMode))));
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

            Room currentRoom = Overworld.CurrentRegion.CurrentRoom ?? Overworld.CurrentRegion.ToMatrix().ToRooms()[0];
            inactivePlayerLocations.Add(new(Player.Identifier.IdentifiableName, Overworld.CurrentRegion.Identifier.IdentifiableName, currentRoom.Identifier.IdentifiableName));

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
        /// Process input.
        /// </summary>
        /// <param name="input">The input to process.</param>
        /// <returns>The reaction to the input.</returns>
        private Reaction ProcessInput(string input)
        {
            // if just a single frame information mode no additional processing needed
            if (Mode.Type == GameModeType.SingleFrameInformation)
                return new Reaction(ReactionResult.Silent, string.Empty);

            // preen input to help with processing
            input = StringUtilities.PreenInput(input);

            // try interpretation
            var interpretation = Mode.Interpreter?.Interpret(input, this) ?? new InterpretationResult(false, new Unactionable("No interpreter."));

            // if interpretation was successful then process
            if (interpretation.WasInterpretedSuccessfully)
                return interpretation.Command.Invoke(this);

            // something was entered, but can't be processed
            if (!string.IsNullOrEmpty(input))
                return new(ReactionResult.Error, $"{input} was not valid input.");

            // empty string
            return new(ReactionResult.Silent, string.Empty);
        }

        /// <summary>
        /// End the game.
        /// </summary>
        internal void End()
        {
            State = GameState.Finished;
        }

        /// <summary>
        /// Get all interaction targets for this game.
        /// </summary>
        /// <returns>An array containing all interaction targets.</returns>
        public IInteractWithItem[] GetAllInteractionTargets()
        {
            var all = new List<IInteractWithItem>
            {
                Player,
                Overworld.CurrentRegion?.CurrentRoom
            };

            all.AddRange(Overworld.CurrentRegion?.CurrentRoom?.GetAllInteractionTargets() ?? []);
            all.AddRange(Player?.Items?.Where(x => x is IInteractWithItem) ?? []);

            return [.. all.Where(x => x != null)];
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

            Overworld.CurrentRegion.CurrentRoom.FindInteractionTarget(name, out var target);
            return target;
        }

        /// <summary>
        /// Get all examinables that are currently visible to the player.
        /// </summary>
        /// <returns>An array of all examinables that are currently visible to the player.</returns>
        public IExaminable[] GetAllPlayerVisibleExaminables()
        {
            var examinables = new List<IExaminable> { Player, Overworld, Overworld?.CurrentRegion, Overworld?.CurrentRegion?.CurrentRoom };
            examinables.AddRange(Player?.Items?.Where(x => x.IsPlayerVisible) ?? []);
            examinables.AddRange(Overworld?.CurrentRegion?.CurrentRoom?.Items?.Where(x => x.IsPlayerVisible) ?? []);
            examinables.AddRange(Overworld?.CurrentRegion?.CurrentRoom?.Characters?.Where(x => x.IsPlayerVisible) ?? []);
            examinables.AddRange(Overworld?.CurrentRegion?.CurrentRoom?.Exits?.Where(x => x.IsPlayerVisible) ?? []);
            return [.. examinables.Where(x => x != null)];
        }

        /// <summary>
        /// Get all commands that are valid in the current context.
        /// </summary>
        /// <returns>An array of all commands that are valid in the current context.</returns>
        public CommandHelp[] GetContextualCommands()
        {
            var commands = Mode?.Interpreter?.GetContextualCommandHelp(this) ?? [];
            return [.. commands.Distinct()];
        }

        /// <summary>
        /// Get all prompts for a command.
        /// </summary>
        /// <param name="command">The command to get the prompts for.</param>
        /// <returns>An array of prompts.</returns>
        public Prompt[] GetPromptsForCommand(CommandHelp command)
        {
            return GetPromptsForCommand(command.Command);
        }

        /// <summary>
        /// Get all prompts for a command.
        /// </summary>
        /// <param name="command">The command to get the prompts for.</param>
        /// <returns>An array of prompts.</returns>
        public Prompt[] GetPromptsForCommand(string command)
        {
            if (GeneralHelp.CommandHelp.Command.InsensitiveEquals(command) || GeneralHelp.CommandHelp.Shortcut.InsensitiveEquals(command))
                return new GeneralHelp(null, null).GetPrompts(this);

            var result = Mode?.Interpreter?.Interpret(command, this) ?? InterpretationResult.Fail;

            if (result.WasInterpretedSuccessfully)
                return result.Command.GetPrompts(this);

            return [];
        }

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        public void RestoreFrom(GameSerialization serialization)
        {
            // resolve asset locations
            AssetArranger.Arrange(this, serialization);

            // restore player locations
            inactivePlayerLocations.Clear();

            // restore all inactive player locations
            foreach (var location in serialization.InactivePlayerLocations)
                inactivePlayerLocations.Add(PlayableCharacterLocation.FromSerialization(location));

            // restore all players
            foreach (var player in serialization.Players)
            {
                var match = Array.Find(Catalog.Players, x => x.Identifier.Equals(player.Identifier));
                ((IRestoreFromObjectSerialization<CharacterSerialization>)match)?.RestoreFrom(player);
            }

            // restore active player
            Player = Array.Find(Catalog.Players, x => x.Identifier.Equals(serialization.ActivePlayerIdentifier));

            // restore note manager
            NoteManager = NoteManager.FromSerialization(serialization.NoteManager);

            // restore variable manager
            VariableManager = VariableManager.FromSerialization(serialization.VariableManager);

            // restore overworld
            ((IObjectSerialization<Overworld>)serialization.Overworld).Restore(Overworld);
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Perform an update, when the start is GameState.NotStarted.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The update result.</returns>
        private static UpdateResult UpdateWhenNotStarted(Game game)
        {
            game.State = GameState.Active;
            game.endMode = null;

            // setup the adapter for this game
            game.Configuration.Adapter.Setup(game);

            switch (game.Configuration.StartMode)
            {
                case StartModes.TitleScreen:

                    // change mode to show the title screen
                    game.ChangeMode(new TitleMode());

                    break;

                case StartModes.Scene:

                    // handle a silent reaction, forcing transition to scene mode
                    game.HandleReaction(Reaction.Silent);

                    break;

                default:

                    throw new NotImplementedException();
            }

            game.Mode.Render(game);

            EventBus.Publish(new GameStarted(game));

            return new(true);
        }

        /// <summary>
        /// Perform an update, when the start is GameState.Active.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="input">Any input that should be passed to the game.</param>
        /// <returns>The update result.</returns>
        private static UpdateResult UpdateWhenActive(Game game, string input)
        {
            // process the input
            var reaction = game.ProcessInput(input);

            // handle the reaction
            game.HandleReaction(reaction);

            // check if the game has ended, and if so end
            if (game.CheckForGameEnd(game.EndConditions, out game.endMode))
                game.State = GameState.EndConditionMet;

            // providing the game hasn't finished render
            if (game.State != GameState.Finished)
                game.Mode.Render(game);

            return new(true);
        }

        /// <summary>
        /// Perform an update, when the start is GameState.EndConditionMet.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The update result.</returns>
        private static UpdateResult UpdateWhenEndConditionMet(Game game)
        {
            // set and render the end mode
            game.ChangeMode(game.endMode);
            game.Mode.Render(game);

            // finishing
            game.State = GameState.Finishing;

            return new(true);
        }

        /// <summary>
        /// Perform an update, when the start is GameState.Finishing.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The update result.</returns>
        private static UpdateResult UpdateWhenFinishing(Game game)
        {
            // finished execution
            game.State = GameState.Finished;

            return new(true);
        }

        /// <summary>
        /// Perform an update, when the start is GameState.Finished.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The update result.</returns>
        private static UpdateResult UpdateWhenFinished(Game game)
        {
            EventBus.Publish(new GameFinished(game));
            return new(false, "Cannot update when finished.");
        }

        /// <summary>
        ///  Create a new callback for generating instances of a game.
        /// </summary>
        /// <param name="info">Information about the game.</param>
        /// <param name="introduction">An introduction to the game.</param>
        /// <param name="assetGenerator">The generator to use to create game assets.</param>
        /// <param name="conditions">The game conditions.</param>
        /// <param name="configuration">The configuration for the game.</param>
        /// <param name="setup">A setup function to run on the created game after it has been created.</param>
        /// <returns>A GameCreator that will create instances of the game.</returns>
        public static GameCreator Create(GameInfo info, string introduction, AssetGenerator assetGenerator, GameEndConditions conditions, GameConfiguration configuration, GameSetupCallback setup = null)
        {
            return new(() =>
            {
                var game = new Game(info, introduction, assetGenerator.GetPlayer(), assetGenerator.GetOverworld(), conditions, configuration);
                setup?.Invoke(game);
                return game;
            });
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<GameSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<GameSerialization>.RestoreFrom(GameSerialization serialization)
        {
            RestoreFrom(serialization);
        }

        #endregion
    }
}
