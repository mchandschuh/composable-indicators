using NUnit.Framework;

namespace ComposableIndicators.Tests
{
    [TestFixture]
    public class WindowIndicatorTests
    {
        [Test]
        public void WhenConstructingNewInstance_Then_Output_IsZero()
        {
            var sut = new FakeWindowIndicator(1);
            Assert.AreEqual(0, sut.Output);
        }

        [Test]
        public void WhenConstructingNewInstance_Then_IsReady_IsFalse()
        {
            var sut = new FakeWindowIndicator(1);
            Assert.IsFalse(sut.IsReady);
        }

        [Test]
        public void WhenConstructingNewInstance_Then_Period_IsSetToConstructorArgument()
        {
            const int period = 3;
            var sut = new FakeWindowIndicator(period);
            Assert.AreEqual(period, sut.Period);
        }

        [Test]
        public void WhenProcessingInput_Then_RollingWindowIsPopulated()
        {
            var function = new FakeWindowComputation();
            var sut = new FakeWindowIndicator(function, 2);
            sut.Process(1);
            Assert.AreEqual(1, function.WindowPassedToCompute[0]);
        }

        [Test]
        public void WhenProcessingPeriodNumberOfInputs_Then_IsReady_FlipsToTrue()
        {
            const int period = 5;
            var sut = new FakeWindowIndicator(period);
            for (int i = 0; i < period - 1; i++)
            {
                sut.Process(0);
                Assert.IsFalse(sut.IsReady);
            }

            sut.Process(0);
            Assert.IsTrue(sut.IsReady);
        }

        [Test]
        public void WhenSettingOutput_Then_OutputUpdated_IsRaised()
        {
            var sut = new FakeWindowIndicator(2);

            var output = 0d;
            sut.OutputUpdated += (sender, o) => output = o;
            sut.SetOutput(1);

            Assert.AreEqual(1, output);
        }

        private class FakeWindowIndicator : WindowIndicator
        {
            public FakeWindowIndicator(IWindowComputation computation, int period)
                : base(computation, period)
            {
            }
            public FakeWindowIndicator(int period)
                : base(new FakeWindowComputation(), period)
            {
            }

            public void SetOutput(double output)
            {
                Output = output;
            }
        }

        private class FakeWindowComputation : IWindowComputation
        {
            public RollingWindow<double> WindowPassedToCompute
            {
                get; private set;
            }

            public double? Compute(RollingWindow<double> window)
            {
                WindowPassedToCompute = window;
                return 0;
            }
        }
    }
}