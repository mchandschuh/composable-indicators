namespace ComposableIndicators
{
    /// <summary>
    /// Represents an indicator that operates on a window of data.
    /// This type provides the infrastructure for <see cref="IWindowComputation"/>
    /// </summary>
    public class WindowIndicator : IIndicator
    {
        private double output;

        private readonly RollingWindow<double> window;
        private readonly IWindowComputation computation;

        public event IndicatorOutputUpdated OutputUpdated;

        public WindowIndicator(IWindowComputation computation, int period)
        {
            this.computation = computation;
            window = new RollingWindow<double>(period);
        }

        public double Output
        {
            get
            {
                return output;
            }
            protected set
            {
                output = value;
                OutputUpdated?.Invoke(this, output);
            }
        }

        public int Period
        {
            get { return window.Size; }
        }

        public bool IsReady
        {
            get { return window.IsFull; }
        }

        public void Process(double input)
        {
            window.Add(input);
            var result = computation.Compute(window);
            if (result.HasValue)
            {
                Output = result.Value;
            }
        }
    }
}