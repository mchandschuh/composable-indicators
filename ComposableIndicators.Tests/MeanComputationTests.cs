using System.Linq;
using NUnit.Framework;

namespace ComposableIndicators.Tests
{
    [TestFixture]
    public class MeanComputationTests
    {
        [Test]
        public void WhenComputing_The_ResultIsTheArithmeticMeanOfAllDataInTheWindow()
        {
            const int period = 5;
            var sut = new MeanComputation();

            var window = new RollingWindow<double>(period);
            for (int i = 0; i < 2 * period; i++)
            {
                window.Add(i);

                var result = sut.Compute(window);
                Assert.AreEqual(window.Average(), result);
            }
        }
    }
}