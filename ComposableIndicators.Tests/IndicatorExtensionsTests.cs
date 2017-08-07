using System.Linq;
using NUnit.Framework;

namespace ComposableIndicators.Tests
{
    [TestFixture]
    public class IndicatorExtensionsTests
    {
        [Test]
        public void WhenChainingIndicatorsUsingOf_Then_FirstIndicatorOutput_IsForwarded_ToSecondIndicatorInput()
        {
            var first = new IdentityIndicator();
            var second = new IdentityIndicator();

            second.Of(first, true);

            first.Process(1);

            Assert.AreEqual(1, second.Output);
        }

        [Test]
        public void WhenChainingManyIndicatorsUsingOf_Then_FirstIndicatorOutput_IsForwarded_ToSecondIndicatorInput_AndSoOn()
        {
            const int count = 10;
            var indicators = Enumerable.Range(0, count)
                .Select(i => new IdentityIndicator())
                .ToList();

            for (int i = 1; i < indicators.Count; i++)
            {
                indicators[i].Of(indicators[i - 1], true);
            }

            indicators[0].Process(1);
            Assert.AreEqual(1, indicators.Last().Output);
        }
    }
}
