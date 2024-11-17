using System;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents an interaction.
    /// </summary>
    public sealed class Interaction : Result
    {
        #region Properties

        /// <summary>
        /// Get the result.
        /// </summary>
        public InteractionResult Result { get; }

        /// <summary>
        /// Get the item used in the interaction.
        /// </summary>
        public Item Item { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Interaction class.
        /// </summary>
        /// <param name="result">The result of the interaction.</param>
        /// <param name="item">The item used in the interaction.</param>
        public Interaction(InteractionResult result, Item item)
        {
            Result = result;
            Item = item;

            Description = result switch
            {
                InteractionResult.NoChange => "There was no effect.",
                InteractionResult.ItemExpires => "The item expires.",
                InteractionResult.TargetExpires => "The target expires.",
                InteractionResult.ItemAndTargetExpires => "Both the item and target expires.",
                InteractionResult.PlayerDies => "The player dies.",
                _ => throw new NotImplementedException($"No implementation for ${result}."),
            };
        }

        /// <summary>
        /// Initializes a new instance of the Interaction class.
        /// </summary>
        /// <param name="result">The result of the interaction.</param>
        /// <param name="item">The item used in the interaction.</param>
        /// <param name="descriptionOfEffect">A description of the effect.</param>
        public Interaction(InteractionResult result, Item item, string descriptionOfEffect)
        {
            Result = result;
            Item = item;
            Description = descriptionOfEffect;
        }

        #endregion
    }
}