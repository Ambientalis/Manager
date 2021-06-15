using System;

namespace Utilitarios.Criptografia
{
    /// <summary>
    /// Thrown when attempting to decrypt or deserialize an invalid encrypted queryString.
    /// </summary>
    public class InvalidQueryStringException : System.Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public InvalidQueryStringException() : base() { }
    }
}
