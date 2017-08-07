namespace ComposableIndicators
{
    /// <summary>
    /// Defines some extension methods that allow you to wire indicators into chains.
    /// </summary>
    public static class IndicatorExtensions
    {
        /// <summary>
        /// Configures the first indicator to forward its output to the second indicator
        /// </summary>
        /// <param name="second">The indicator that receives data from the first</param>
        /// <param name="first">The indicator that sends data via DataConsolidated even
        /// to the second</param>
        /// <param name="waitForFirstToReady">True to only send updates to the second if
        /// first.IsReady returns true,false to alway send updates to second</param>
        /// <returns>The reference to the second indicator to allow for method chaining
        /// </returns>
        public static IIndicator Of(this IIndicator second, IIndicator first, bool waitForFirstToReady)
        {
            first.OutputUpdated += (sender, consolidated) =>
            {
                // only send the data along if we're ready
                if (!waitForFirstToReady || first.IsReady)
                {
                    second.Process(consolidated);
                }
            };

            return second;
        }

        public static IIndicator Plus(this IIndicator left, IIndicator right)
        {
            return new CompositeIndicator(right, left, (l, r) => l.Output + r.Output);
        }

        public static IIndicator Minus(this IIndicator left, IIndicator right)
        {
            return new CompositeIndicator(right, left, (l, r) => l.Output - r.Output);
        }

        public static IIndicator Times(this IIndicator left, IIndicator right)
        {
            return new CompositeIndicator(right, left, (l, r) => l.Output * r.Output);
        }

        public static IIndicator Over(this IIndicator left, IIndicator right)
        {
            return new CompositeIndicator(right, left, (l, r) => l.Output / r.Output);
        }
    }
}
