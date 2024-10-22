using NetAF.Logic;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents a request to examin an IExaminable.
    /// </summary>
    public class ExaminationRequest
    {
        #region Propeties

        /// <summary>
        /// Get the examinable object.
        /// </summary>
        public IExaminable Examinable { get; private set; }
        
        /// <summary>
        /// Get the examination scene.
        /// </summary>
        public ExaminationScene Scene { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ExaminationRequest class.
        /// </summary>
        /// <param name="examinable">The object being examined.</param>
        /// <param name="scene">The scene the object is being examined from.</param>
        public ExaminationRequest(IExaminable examinable, ExaminationScene scene)
        {
            Examinable = examinable;
            Scene = scene;
        }

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
