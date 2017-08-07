using System.Linq;
using NUnit.Framework;

namespace ComposableIndicators.Tests
{
    [TestFixture]
    public class SumComputationTests
    {
        [Test]
        public void WhenComputing_Then_ResultIsTheSumOfAllDataInTheWindow()
        {
            const int period = 5;
            var sut = new SumComputation();

            var window = new RollingWindow<double>(period);
            for (int i = 0; i < 2*period; i++)
            {
                window.Add(i);

                var result = sut.Compute(window);
                Assert.AreEqual(window.Sum(), result);
            }
        }
    }
}
