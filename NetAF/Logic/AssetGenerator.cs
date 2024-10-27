using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.Logic
{
    /// <summary>
    /// Represents a generator for game assets.
    /// </summary>
    public sealed class AssetGenerator
    {
        #region Properties

        /// <summary>
        /// Get the overworld generator.
        /// </summary>
        private OverworldCreationCallback OverworldGenerator { get; }

        /// <summary>
        /// Get the player generator.
        /// </summary>
        private PlayerCreationCallback PlayerGenerator { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AssetGenerator class.
        /// </summary>
        /// <param name="overworldGenerator">The overworld generator.</param>
        /// <param name="playerGenerator">The player generator.</param>
        private AssetGenerator(OverworldCreationCallback overworldGenerator, PlayerCreationCallback playerGenerator)
        {
            OverworldGenerator = overworldGenerator;
            PlayerGenerator = playerGenerator;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the overworld.
        /// </summary>
        /// <returns>The overworld.</returns>
        public Overworld GetOverworld()
        {
            return OverworldGenerator();
        }

        /// <summary>
        /// Get the player.
        /// </summary>
        /// <returns>The player.</returns>
        public PlayableCharacter GetPlayer()
        {
            return PlayerGenerator();
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create an asset generator that uses retained value. The same instance of overworld and player will be returned on each call to GetOverworld and GetPlayer.
        /// </summary>
        /// <param name="overworld">The overworld.</param>
        /// <param name="player">The player.</param>
        /// <returns>Asset generation that will always return the same instance overworld and the same instance of the player.</returns>
        public static AssetGenerator Retained(Overworld overworld, PlayableCharacter player)
        {
            return new AssetGenerator(() => overworld, () => player);
        }

        /// <summary>
        /// Create an asset generator that creates new values. Different instances of overworld and player will be returned on each call to GetOverworld and GetPlayer.
        /// </summary>
        /// <param name="overworldTemplate">The overworld template.</param>
        /// <param name="playerTemplate">The player template.</param>
        /// <returns>Asset generation that will always return a new overworld and a new player.</returns>
        public static AssetGenerator New(IAssetTemplate<Overworld> overworldTemplate, IAssetTemplate<PlayableCharacter> playerTemplate)
        {
            return new AssetGenerator(() => overworldTemplate.Instantiate(), () => playerTemplate.Instantiate());
        }

        /// <summary>
        /// Create an asset generator that creates new values. Different instances of overworld and player will be returned on each call to GetOverworld and GetPlayer.
        /// </summary>
        /// <param name="overworldMaker">The overworld maker.</param>
        /// <param name="playerTemplate">The player template.</param>
        /// <returns>Asset generation that will always return a new overworld and a new player.</returns>
        public static AssetGenerator New(OverworldMaker overworldMaker, IAssetTemplate<PlayableCharacter> playerTemplate)
        {
            return new AssetGenerator(() => overworldMaker.Make(), () => playerTemplate.Instantiate());
        }

        /// <summary>
        /// Create an asset generator that creates custom values. Callbacks determine the overworld and player that will be returned on each call to GetOverworld and GetPlayer.
        /// </summary>
        /// <param name="overworldCreationCallback">The overworld creation callback.</param>
        /// <param name="playerCreationCallback">The player creation callback.</param>
        /// <returns>Asset generation that will always return an overworld and a player as defined by the callbacks.</returns>
        public static AssetGenerator Custom(OverworldCreationCallback overworldCreationCallback, PlayerCreationCallback playerCreationCallback)
        {
            return new AssetGenerator(overworldCreationCallback, playerCreationCallback);
        }

        #endregion
    }
}
