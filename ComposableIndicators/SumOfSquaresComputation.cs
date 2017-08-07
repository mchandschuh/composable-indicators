namespace ComposableIndicators
{
    public class SumOfSquaresComputation : IWindowComputation
    {
        private double sumOfSquares;

        public double? Compute(RollingWindow<double> window)
        {
            double removed = 0;
            if (window.HasOverflowed)
            {
                removed = window.MostRecentlyRemoved;
            }

            sumOfSquares = sumOfSquares + window[0]*window[0] - removed*removed;
            return sumOfSquares;
        }
    }
}