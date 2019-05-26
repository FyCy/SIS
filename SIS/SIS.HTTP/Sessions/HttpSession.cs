using SIS.HTTP.Common;
using SIS.HTTP.Contracts;
using System.Collections.Generic;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> sessionParameters;
        public HttpSession(string id)
        {
            this.Id = id;
            this.sessionParameters = new Dictionary<string, object>();
        }
        public string Id { get; }

        public object GetParameters(string parameterName)
        {
            CoreValidator.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));
            return this.sessionParameters[parameterName];
        }
        public void AddParameters(string parameterName,object parameter)
        {
            CoreValidator.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));
            CoreValidator.ThrowIfNull(parameter, nameof(parameter));

            this.sessionParameters[parameterName] = parameter;
        }

        public bool ContainsParameters(string parameterName)
        {
            CoreValidator.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));

            return this.sessionParameters.ContainsKey(parameterName);
        }

        public void ClearParameters()
        {
            this.sessionParameters.Clear();
        }
    }
}
