namespace ComposableIndicators
{
    public class MeanComputation : IWindowComputation
    {
        private readonly SumComputation sumComputation = new SumComputation();

        public double? Compute(RollingWindow<double> window)
        {
            var sum = sumComputation.Compute(window);
            return sum / window.Count;
        }
    }
}