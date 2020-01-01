using NUnit.Framework;

namespace MemoryCalculator.UnitTest
{
    [TestFixture]
    internal class MemoryCalculatorTest
    {
        private MemoryCalculator _calc;

        [SetUp]
        public void Init()
        {
            _calc = new MemoryCalculator();
        }

        [Test]
        public void Sum_ByDefault_ReturnsZero()
        {
            ResultShouldBe(0);
        }

        [Test]
        public void Add_WhenCalled_Sum()
        {
            _calc.Add(1);
            ResultShouldBe(1);
        }

        private void ResultShouldBe(int expectedSum)
        {
            Assert.AreEqual(expectedSum, _calc.Sum());
        }
    }
}