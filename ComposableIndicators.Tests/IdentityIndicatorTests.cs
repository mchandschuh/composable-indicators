using NUnit.Framework;

namespace ComposableIndicators.Tests
{
    [TestFixture]
    public class IdentityIndicatorTests
    {
        [Test]
        public void WhenConstructingNewInstance_Then_IsReady_IsFalse()
        {
            var sut = new IdentityIndicator();
            Assert.IsFalse(sut.IsReady);
        }

        [Test]
        public void WhenConstructingNewInstance_Then_Output_IsZero()
        {
            var sut = new IdentityIndicator();
            Assert.AreEqual(0, sut.Output);
        }

        [Test]
        public void WhenProcessingInput_Then_Output_EqualsInput()
        {
            const double input = 3d;
            var sut = new IdentityIndicator();
            sut.Process(input);
            Assert.AreEqual(input, sut.Output);
        }

        [Test]
        public void WhenProcessingInput_Then_IsReady_IsTrue()
        {
            var sut = new IdentityIndicator();
            sut.Process(0);
            Assert.IsTrue(sut.IsReady);
        }

        [Test]
        public void WhenProcessingInput_Then_OutputUpdated_IsRaised()
        {
            var sut = new IdentityIndicator();


            var output = 0d;
            sut.OutputUpdated += (sender, o) => output = o;
            sut.Process(1);

            Assert.AreEqual(1, output);
        }
    }
}
