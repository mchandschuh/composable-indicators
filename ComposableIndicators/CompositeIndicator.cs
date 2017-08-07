namespace ComposableIndicators
{
    /// <summary>
    /// Provides a mechanism for applying a function to the output of two indicators
    /// </summary>
    public class CompositeIndicator : IIndicator
    {
        private readonly IIndicator left;
        private readonly IIndicator right;
        private readonly CombineIndicators composer;

        public event IndicatorOutputUpdated OutputUpdated;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeIndicator"/> class
        /// </summary>
        /// <param name="left">The left indicator</param>
        /// <param name="right">The right indicator</param>
        /// <param name="composer">Function used to combine indicators into a single value</param>
        public CompositeIndicator(IIndicator left, IIndicator right, CombineIndicators composer)
        {
            this.left = left;
            this.right = right;
            this.composer = composer;

            RegisterHandlers();
        }

        public double Output
        {
            get; private set;
        }

        public bool IsReady
        {
            get { return left.IsReady && right.IsReady; }
        }

        public void Process(double input)
        {
            // NOP
        }

        private void RegisterHandlers()
        {
            left.OutputUpdated += (sender, output) =>
            {
                Output = composer(left, right);
                OutputUpdated?.Invoke(this, Output);
            };

            right.OutputUpdated += (sender, output) =>
            {
                Output = composer(left, right);
                OutputUpdated?.Invoke(this, Output);
            };
        }
    }
}