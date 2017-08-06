using System;
using System.Linq;
using NUnit.Framework;

namespace ComposableIndicators.Tests
{
    [TestFixture]
    public class RollingWindowTests
    {
        [Test]
        public void WhenConstructingNewInstance_Then_Count_IsZero()
        {
            var sut = new RollingWindow<int>(1);
            Assert.AreEqual(0, sut.Count);
        }

        [Test]
        public void WhenConstructingNewInstance_Then_Samples_IsZero()
        {
            var sut = new RollingWindow<int>(1);
            Assert.AreEqual(0, sut.Samples);
        }

        [Test]
        public void WhenConstructingNewInstance_Then_Size_IsSetToConstructorArgument()
        {
            const int size = 1;
            var sut = new RollingWindow<int>(size);
            Assert.AreEqual(size, sut.Size);
        }

        [Test]
        public void WhenConstructingNewInstance_Then_IsFull_IsFalse()
        {
            var sut = new RollingWindow<int>(1);
            Assert.IsFalse(sut.IsFull);
        }

        [Test]
        public void WhenConstructingNewInstance_Then_MostRecentlyRemoved_ThrowsInvalidOperationException()
        {
            var sut = new RollingWindow<int>(1);
            Assert.That(() => sut.MostRecentlyRemoved, Throws.InvalidOperationException);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void WhenConstructingNewInstanceAndSizeIsZeroOrNegative_Then_ThrowsArgumentException(int size)
        {
            Assert.That(() => new RollingWindow<int>(size), Throws.ArgumentException);
        }

        [Test]
        public void WhenAddingItem_Then_IndexZero_IsEqualToItem()
        {
            const int item = 3;
            var sut = new RollingWindow<int>(1);
            sut.Add(item);
            Assert.AreEqual(item, sut[0]);
        }

        [Test]
        public void WhenAddingSizeNumberOfItems_Then_IsFull_IsTrue()
        {
            const int size = 3;
            var sut = new RollingWindow<int>(size);
            for (int i = 0; i < size; i++)
            {
                sut.Add(0);
            }
            Assert.IsTrue(sut.IsFull);
        }

        [Test]
        public void WhenAddingSizeNumberOfItems_Then_MostRecentlyRemoved_ThrowsInvalidOperationException()
        {
            const int size = 3;
            var sut = new RollingWindow<int>(size);
            for (int i = 0; i < size; i++)
            {
                sut.Add(0);
            }
            Assert.That(() => sut.MostRecentlyRemoved, Throws.InvalidOperationException);
        }

        [Test]
        public void WhenAddingLessThanSizeNumberOfItems_Then_IsFull_IsFalse()
        {
            const int size = 3;
            var sut = new RollingWindow<int>(size);
            for (int i = 0; i < size - 1; i++)
            {
                sut.Add(0);
            }
            Assert.IsFalse(sut.IsFull);
        }

        [Test]
        public void WhenAddingLessThanSizeNumberOfItems_Then_HasOverflowed_IsFalse()
        {
            const int size = 3;
            var sut = new RollingWindow<int>(size);
            for (int i = 0; i < size - 1; i++)
            {
                sut.Add(0);
            }
            Assert.IsFalse(sut.HasOverflowed);
        }

        [Test]
        public void WhenAddingOneMoreThanSizeNumberOfItems_Then_MostRecentlyRemoved_IsEqualToTheFirstItemAdded()
        {
            const int size = 3;
            var sut = new RollingWindow<int>(size);
            for (int i = 0; i < size + 1; i++)
            {
                sut.Add(i + 1);
            }
            Assert.AreEqual(1, sut.MostRecentlyRemoved);
        }

        [Test]
        public void WhenAddingOneMoreThanSizeNumberOfItems_Then_HasOverflowed_IsTrue()
        {
            const int size = 3;
            var sut = new RollingWindow<int>(size);
            for (int i = 0; i < size + 1; i++)
            {
                sut.Add(0);
            }
            Assert.IsTrue(sut.HasOverflowed);
        }

        [Test]
        public void WhenIndexingIntoWindow_Then_Items_AreInReverseChronologicalOrder()
        {
            const int size = 5;
            var items = Enumerable.Range(1, size).ToList();
            var sut = new RollingWindow<int>(size);
            items.ForEach(sut.Add);

            for (int i = 0; i < size; i++)
            {
                Assert.AreEqual(size - i, sut[i]);
            }
        }

        [Test]
        [TestCase(-2)]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(2)]
        public void WhenIndexingIntoWindowAndIndexIsOutOfBounds_Then_ThrowsInvalidOperationException(int index)
        {
            var sut = new RollingWindow<int>(1);
            Assert.That(() => sut[index], Throws.Exception.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void WhenEnumerating_Then_ItemsAreInReverseChronologicalOrder()
        {
            const int size = 5;
            var items = Enumerable.Range(1, size).ToList();
            var sut = new RollingWindow<int>(size);
            items.ForEach(sut.Add);

            items.Reverse();
            Assert.That(sut.AsEnumerable(), Is.EquivalentTo(items));
        }
    }
}
