using System;

namespace ComposableIndicators
{
    public class StandardDeviationComputation : IWindowComputation
    {
        private readonly VarianceComputation varianceComputation = new VarianceComputation();

        public double? Compute(RollingWindow<double> window)
        {
            var variance = varianceComputation.Compute(window);
            if (variance == null)
            {
                return null;
            }

            return Math.Sqrt(variance.Value);
        }
    }
}