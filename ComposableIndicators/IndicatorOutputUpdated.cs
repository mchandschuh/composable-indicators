namespace ComposableIndicators
{
    /// <summary>
    /// Defines the delegate for handling <see cref="IIndicator.OutputUpdated"/> events
    /// </summary>
    /// <param name="sender">The indicator who's value was updated</param>
    /// <param name="output">The new output value of the indicator</param>
    public delegate void IndicatorOutputUpdated(IIndicator sender, double output);
}