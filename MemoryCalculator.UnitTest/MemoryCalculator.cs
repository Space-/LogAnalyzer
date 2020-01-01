namespace MemoryCalculator.UnitTest
{
    public class MemoryCalculator
    {
        private int _sum = 0;

        public double Sum()
        {
            return _sum;
        }

        public void Add(int number)
        {
            _sum += number;
        }
    }
}