namespace SIS.HTTP.Contracts
{
    public interface IHttpSession
    {
        string Id { get; }


        object GetParameters(string parameterName);

        bool ContainsParameters(string parameterName);

        void AddParameters(string parameterName , object parameter);

        void ClearParameters();
    }
}
