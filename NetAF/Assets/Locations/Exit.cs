using NetAF.Commands;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Assets.Locations
{
    /// <summary>
    /// Represents an exit from a GameLocation.
    /// </summary>
    public sealed class Exit : ExaminableObject, IInteractWithItem, IRestoreFromObjectSerialization<ExitSerialization>
    {
        #region Properties

        /// <summary>
        /// Get the direction of the exit.
        /// </summary>
        public Direction Direction { get; }

        /// <summary>
        /// Get if this Exit is locked.
        /// </summary>
        public bool IsLocked { get; private set; }

        /// <summary>
        /// Get the interaction.
        /// </summary>
        public InteractionCallback Interaction { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Exit class.
        /// </summary>
        /// <param name="direction">The direction of the exit.</param>
        /// <param name="isLocked">If this exit is locked.</param>
        /// <param name="identifier">An identifier for the exit.</param>
        /// <param name="description">A description of the exit.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        /// <param name="examination">The examination.</param>
        public Exit(Direction direction, bool isLocked = false, Identifier identifier = null, IDescription description = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null)
        {
            Identifier = identifier ?? new(direction.ToString());
            Direction = direction;
            Description = description ?? GenerateDescription();
            IsLocked = isLocked;
            Commands = commands ?? [];
            Interaction = interaction ?? (i => new(InteractionResult.NoChange, i));
            Examination = examination ?? DefaultExamination;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generate a description for this exit.
        /// </summary>
        /// <returns>The completed Description.</returns>
        private IDescription GenerateDescription()
        {
            return new ConditionalDescription($"The exit {Direction.ToString().ToLower()} is locked.", $"The exit {Direction.ToString().ToLower()} is unlocked.", () => IsLocked);
        }

        /// <summary>
        /// Unlock this exit.
        /// </summary>
        public void Unlock()
        {
            IsLocked = false;
        }

        /// <summary>
        /// Lock this exit.
        /// </summary>
        public void Lock()
        {
            IsLocked = true;
        }

        #endregion

        #region Implementation of IInteractWithItem

        /// <summary>
        /// Interact with an item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The interaction.</returns>
        public Interaction Interact(Item item)
        {
            return Interaction.Invoke(item);
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<ExitSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        public void RestoreFrom(ExitSerialization serialization)
        {
            base.RestoreFrom(serialization);

            IsLocked = serialization.IsLocked;
        }

        #endregion
    }
}