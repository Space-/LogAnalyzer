namespace LogAn.UnitTest.Interface
{
    public interface IWebService
    {
        void LogError(string fileName);

        void Write(string message);
    }
}