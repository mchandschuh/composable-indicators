﻿using System.Linq;
using Accord.Statistics;
using NUnit.Framework;

namespace ComposableIndicators.Tests
{
    [TestFixture]
    public class VarianceComputationTests
    {
        [Test]
        public void WhenComputing_Then_ResultIsThePopulationVariance()
        {
            const int period = 5;
            var sut = new VarianceComputation();

            var window = new RollingWindow<double>(period);
            window.Add(0);
            Assert.AreEqual(null, sut.Compute(window));

            for (int i = 1; i < 2 * period; i++)
            {
                window.Add(i);

                var actual = sut.Compute(window);
                var expected = window.ToArray().Variance(false);
                Assert.AreEqual(expected, actual, 1e-10);
            }
        }
    }
}