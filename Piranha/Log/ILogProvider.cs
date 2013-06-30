using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piranha.Log
{
    public interface ILogProvider
    {
        /// <summary>
        /// Logs an error to the log file.
        /// </summary>
        /// <param name="origin">The origin of the error</param>
        /// <param name="message">The message</param>
        /// <param name="details">Optional error details</param>
        void Error(string origin, string message, Exception details = null);

    }
}
