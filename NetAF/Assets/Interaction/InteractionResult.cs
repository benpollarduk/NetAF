using System;

namespace NetAF.Assets.Interaction
{
    /// <summary>
    /// Represents a result of an interaction.
    /// </summary>
    public sealed class InteractionResult : Result
    {
        #region Properties

        /// <summary>
        /// Get the effect.
        /// </summary>
        public InteractionEffect Effect { get; }

        /// <summary>
        /// Get the item used in the interaction.
        /// </summary>
        public Item Item { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the InteractionResult class.
        /// </summary>
        /// <param name="effect">The effect of this interaction.</param>
        /// <param name="item">The item used in this interaction.</param>
        public InteractionResult(InteractionEffect effect, Item item)
        {
            Effect = effect;
            Item = item;

            Description = effect switch
            {
                InteractionEffect.NeitherItemOrTargetExpired => "There was no effect.",
                InteractionEffect.ItemExpired => "The item expired.",
                InteractionEffect.TargetExpired => "The target expired.",
                InteractionEffect.ItemAndTargetExpired => "Both the item and target expired.",
                _ => throw new NotImplementedException($"No implementation for ${effect}."),
            };
        }

        /// <summary>
        /// Initializes a new instance of the InteractionResult class.
        /// </summary>
        /// <param name="effect">The effect of this interaction.</param>
        /// <param name="item">The item used in this interaction.</param>
        /// <param name="descriptionOfEffect">A description of the effect.</param>
        public InteractionResult(InteractionEffect effect, Item item, string descriptionOfEffect)
        {
            Effect = effect;
            Item = item;
            Description = descriptionOfEffect;
        }

        #endregion
    }
}