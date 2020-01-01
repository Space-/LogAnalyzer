namespace MemoryCalculator.UnitTest
{
    public class MemoryCalculator
    {
        private int _sum = 0;

        public double Sum()
        {
            int temp = _sum;
            _sum = 0;
            return temp;
        }

        public void Add(int number)
        {
            _sum += number;
        }
    }
}