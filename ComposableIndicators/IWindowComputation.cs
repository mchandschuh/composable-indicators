namespace ComposableIndicators
{
    /// <summary>
    /// Defines the computation to be performed by a <see cref="WindowIndicator"/>
    /// </summary>
    public interface IWindowComputation
    {
        double? Compute(RollingWindow<double> window);
    }
}