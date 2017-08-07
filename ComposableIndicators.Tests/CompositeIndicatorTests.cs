using NUnit.Framework;

namespace ComposableIndicators.Tests
{
    [TestFixture]
    public class CompositeIndicatorTests
    {
        [Test]
        public void WhenProcessingDataByLeftOrRightIndicator_Then_OutputUpdated_IsRaised()
        {
            var left = new IdentityIndicator();
            var right = new IdentityIndicator();
            var sut = new CompositeIndicator(left, right, (l, r) => l.Output + r.Output);

            var output = 0d;
            sut.OutputUpdated += (sender, o) => output = o;

            left.Process(1);
            Assert.AreEqual(1, output);

            right.Process(2);
            Assert.AreEqual(3, output);
        }
    }
}
