using NetAF.Assets.Attributes;
using NetAF.Commands;
using NetAF.Serialization.Assets;
using NetAF.Serialization;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents any object that is examinable.
    /// </summary>
    public interface IExaminable : IPlayerVisible
    {
        /// <summary>
        /// Get this objects identifier.
        /// </summary>
        Identifier Identifier { get; }
        /// <summary>
        /// Get a description of this object.
        /// </summary>
        IDescription Description { get; }
        /// <summary>
        /// Get this objects commands.
        /// </summary>
        CustomCommand[] Commands { get; }
        /// <summary>
        /// Get the attribute manager for this object.
        /// </summary>
        AttributeManager Attributes { get; }
        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <param name="scene">The scene this object is being examined from.</param>
        /// <returns>The examination.</returns>
        Examination Examine(ExaminationScene scene);
    }
}