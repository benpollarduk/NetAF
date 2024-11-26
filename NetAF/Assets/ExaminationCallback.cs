namespace NetAF.Assets
{
    /// <summary>
    /// Represents the callback for examinations.
    /// </summary>
    /// <param name="request">The examination request.</param>
    /// <returns>The examination.</returns>
    public delegate Examination ExaminationCallback(ExaminationRequest request);
}