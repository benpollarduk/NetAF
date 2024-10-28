using NetAF.Logic;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents a request to examine an IExaminable.
    /// </summary>
    /// <param name="examinable">The object being examined.</param>
    /// <param name="scene">The scene the object is being examined from.</param>
    public class ExaminationRequest(IExaminable examinable, ExaminationScene scene)
    {
        #region Properties

        /// <summary>
        /// Get the examinable object.
        /// </summary>
        public IExaminable Examinable { get; private set; } = examinable;

        /// <summary>
        /// Get the examination scene.
        /// </summary>
        public ExaminationScene Scene { get; private set; } = scene;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ExaminationRequest class.
        /// </summary>
        /// <param name="examinable">The object being examined.</param>
        /// <param name="game">The executing game.</param>
        public ExaminationRequest(IExaminable examinable, Game game) : this(examinable, new ExaminationScene(game))
        {
        }

        #endregion
    }
}
