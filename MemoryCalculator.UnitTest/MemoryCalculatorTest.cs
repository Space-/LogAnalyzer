using NUnit.Framework;

namespace MemoryCalculator.UnitTest
{
    [TestFixture]
    internal class MemoryCalculatorTest
    {
        [Test]
        public void Sum_ByDefault_ReturnsZero()
        {
            var calc = new MemoryCalculator();
            Assert.AreEqual(0, calc.Sum());
        }

        [Test]
        public void Add_WhenCalled_Sum()
        {
            var calc = new MemoryCalculator();
            calc.Add(1);
            Assert.AreEqual(1, calc.Sum());
        }
    }
}