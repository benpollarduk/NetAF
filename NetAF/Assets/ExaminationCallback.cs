namespace NetAF.Assets
{
    /// <summary>
    /// Represents the callback for examinations.
    /// </summary>
    /// <param name="request">The examination request.</param>
    /// <returns>A string representing the result of the examination.</returns>
    public delegate ExaminationResult ExaminationCallback(ExaminationRequest request);
}
