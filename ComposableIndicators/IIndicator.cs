namespace ComposableIndicators
{
    /// <summary>
    /// Represents an indicator of a one-dimensional source signal. The canonical
    /// example being the SimpleMovingAverage which averages the last N input values
    /// to produce a single output value.
    /// </summary>
    /// <remarks>
    /// The inputs/outputs of this type are symmetric, thereby allowing composition.
    /// In addition, the type fires an event when its value has been updated, thereby
    /// allowing chaining of indicators and 'wiring up' of an indicator's source data
    /// to another indicator's output data.
    /// </remarks>
    public interface IIndicator
    {
        /// <summary>
        /// Event fired when this indicator has produced a new output value
        /// </summary>
        event IndicatorOutputUpdated OutputUpdated;

        /// <summary>
        /// Gets the current output value of this indicator
        /// </summary>
        double Output { get; }

        /// <summary>
        /// Gets whether or not this indicator is in the ready state.
        /// </summary>
        /// <remarks>
        /// Some indicators require a certain number of data points before their
        /// output reaches an accurate value.
        /// </remarks>
        bool IsReady { get; }

        /// <summary>
        /// Process the new input value through this indicator.
        /// </summary>
        /// <param name="input">The new input value to be processed</param>
        void Process(double input);
    }
}