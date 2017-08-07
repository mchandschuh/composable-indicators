namespace ComposableIndicators
{
    /// <summary>
    /// Delegate type used to combine two indicators using <see cref="CompositeIndicator"/>
    /// </summary>
    /// <param name="left">The left indicator</param>
    /// <param name="right">The right indicator</param>
    /// <returns>The result of combining the left and right indicators</returns>
    public delegate double CombineIndicators(IIndicator left, IIndicator right);
}