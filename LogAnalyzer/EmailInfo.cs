namespace LogAn.UnitTest
{
    public class EmailInfo
    {
        public string Body;
        public string To;
        public string Subject;

        public bool Equals(EmailInfo emailInfo)
        {
            return Body.Equals(emailInfo.Body) && To.Equals(emailInfo.To) && Subject.Equals(emailInfo.Subject);
        }
    }
}