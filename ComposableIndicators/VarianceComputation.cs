namespace ComposableIndicators
{
    public class VarianceComputation : IWindowComputation
    {
        private readonly MeanComputation meanComputation = new MeanComputation();
        private readonly SumOfSquaresComputation sumOfSquaresComputation = new SumOfSquaresComputation();

        public double? Compute(RollingWindow<double> window)
        {
            var mean = meanComputation.Compute(window);
            var sumOfSquares = sumOfSquaresComputation.Compute(window);

            if (window.Samples < 2)
            {
                return null;
            }

            return sumOfSquares/window.Count - mean * mean;
        }
    }
}