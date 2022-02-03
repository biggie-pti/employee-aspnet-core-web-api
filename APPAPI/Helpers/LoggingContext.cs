using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.API.Helpers
{
    /// <summary>
    /// Context log headers file
    /// </summary>
    public class LoggingContext
    {
        /// <summary>
        /// construct log context
        /// </summary>
        public LoggingContext(string serverName, string apiName, string controllerName, string actionName, int statusCode)
        {
            this.ServerName = serverName;
            this.APIName = apiName;
            this.ControllerName = controllerName;
            this.ActionName = actionName;
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// server name
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// api name
        /// </summary>
        public string APIName { get; set; }

        public string ControllerName { get; set; }
        /// <summary>
        /// action name
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// satus code
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// error message
        /// </summary>
        public string ErrorMessage { get; }

    }
}
