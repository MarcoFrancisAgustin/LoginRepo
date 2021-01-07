namespace Login
{
    public interface IBusinessLogic
    {
        void ProcessLogin(string userName, string password);
        void ProcessRecords();
    }
}