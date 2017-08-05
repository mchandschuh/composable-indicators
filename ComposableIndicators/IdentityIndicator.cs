namespace ComposableIndicators
{
    /// <summary>
    /// Represents an indicator who's output value is always equal to it's
    /// most recent input value.
    /// </summary>
    public sealed class IdentityIndicator : IIndicator
    {
        public event IndicatorOutputUpdated OutputUpdated;

        public double Output
        {
            get; private set;
        }

        public bool IsReady
        {
            get; private set;
        }

        public void Process(double input)
        {
            IsReady = true;
            Output = input;
            OnOutputUpdated(input);
        }

        private void OnOutputUpdated(double output)
        {
            OutputUpdated?.Invoke(this, output);
        }
    }
}