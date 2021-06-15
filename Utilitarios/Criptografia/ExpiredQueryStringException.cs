using System;

namespace Utilitarios.Criptografia
{
    /// <summary>
    /// Thrown when a queryString has expired and is therefore no longer valid.
    /// </summary>
    public class ExpiredQueryStringException : System.Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public ExpiredQueryStringException() : base() { }
    }
}
