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
                InteractionEffect.FatalEffect => "There was a fatal effect.",
                InteractionEffect.ItemUsedUp => "The item was used up.",
                InteractionEffect.NoEffect => "There was no effect.",
                InteractionEffect.SelfContained => "The effect was self contained.",
                InteractionEffect.TargetUsedUp => "The target was used up.",
                _ => throw new NotImplementedException(),
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