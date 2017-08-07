namespace ComposableIndicators
{
    public class SumComputation : IWindowComputation
    {
        private double sum;

        public double? Compute(RollingWindow<double> window)
        {
            double removed = 0;
            if (window.HasOverflowed)
            {
                removed = window.MostRecentlyRemoved;
            }

            sum = sum + window[0] - removed;
            return sum;
        }
    }
}