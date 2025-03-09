using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Commands.Scene;
using NetAF.Extensions;
using NetAF.Interpretation;
using NetAF.Logic.Arrangement;
using NetAF.Logic.Callbacks;
using NetAF.Logic.Modes;
using NetAF.Serialization;
using NetAF.Serialization.Assets;
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
            try
            {
                switch (State)
                {
                    case GameState.NotStarted:

                        State = GameState.Active;
                        endMode = null;

                        // setup the adapter for this game
                        Configuration.Adapter.Setup(this);

                        // change mode to show the title screen
                        ChangeMode(new TitleMode());

                        Mode.Render(this);

                        return new(true);

                    case GameState.Active:

                        // process the input
                        var reaction = ProcessInput(input);

                        // handle the reaction
                        HandleReaction(reaction);

                        // check if the game has ended, and if so end
                        if (CheckForGameEnd(EndConditions, out endMode))
                            State = GameState.Finishing;

                        // providing the game hasn't finished render
                        if (State != GameState.Finished)
                            Mode.Render(this);

                        return new(true);

                    case GameState.Finishing:

                        // if an end mode specified
                        if (endMode != null)
                        {
                            // set and render the end mode
                            ChangeMode(endMode);
                            Mode.Render(this);
                            endMode = null;
                        }
                        else
                        {
                            // finished execution
                            State = GameState.Finished;
                        }

                        return new(true);

                    default:

                        return new(false, $"Cannot move to next when state is {State}.");
                }
            }
            catch (Exception e)
            {
                return new(false, $"Exception caught during update: {e.Message}");
            }
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
                // display the reaction
                ChangeMode(new ReactionMode(Overworld.CurrentRegion?.CurrentRoom?.Identifier.Name, reaction));
                return;
            }
            
            // 2. check that there is a room - it may be that the region changed and needs to be entered
            if (Overworld.CurrentRegion.CurrentRoom == null)
            {
                // enter the region
                var regionEnterReaction = Overworld.CurrentRegion.Enter();

                // if the reaction wasn't silent then show reaction, else revert back to scene mode
                if (regionEnterReaction.Result != ReactionResult.Silent)
                    ChangeMode(new ReactionMode(Overworld.CurrentRegion.CurrentRoom.Identifier.Name, regionEnterReaction));
                else
                    ChangeMode(new SceneMode());

                return;
            }

            // 3. check if command didn't change the mode and the current mode type is information, essentially the mode has expired
            if (reaction.Result != ReactionResult.GameModeChanged && Mode.Type == GameModeType.Information)
            {
                // revert back to scene mode as the 
                ChangeMode(new SceneMode());
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
        /// Get all commands that are valid in the current context.
        /// </summary>
        /// <returns>An array of all commands that are valid in the current context.</returns>
        public CommandHelp[] GetContextualCommands()
        {
            List<CommandHelp> commands = 
            [
                .. Configuration.Interpreter.SupportedCommands,
                .. Configuration.Interpreter.GetContextualCommandHelp(this),
                .. Mode?.Interpreter?.SupportedCommands ?? [],
                .. Mode?.Interpreter?.GetContextualCommandHelp(this) ?? [],
            ];

            return [.. commands.Distinct()];
        }

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
                ((IRestoreFromObjectSerialization<CharacterSerialization>)match)?.RestoreFrom(player);
            }

            // restore active player
            Player = Array.Find(Catalog.Players, x => x.Identifier.Equals(serialization.ActivePlayerIdentifier));

            // restore overworld
            ((IObjectSerialization<Overworld>)serialization.Overworld).Restore(Overworld);

            // restore player locations
            inactivePlayerLocations.Clear();
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
        public static GameCreationCallback Create(GameInfo info, string introduction, AssetGenerator assetGenerator, GameEndConditions conditions, GameConfiguration configuration, GameSetupCallback setup = null)
        {
            return () =>
            {
                var game = new Game(info, introduction, assetGenerator.GetPlayer(), assetGenerator.GetOverworld(), conditions, configuration);
                setup?.Invoke(game);
                return game;
            };
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