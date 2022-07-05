using System;
using System.Runtime.Serialization;

namespace DbEntidades.Operators
{
    [Serializable]
    public class PermisoException : Exception
    {
        public PermisoException() : base("El usuario no tiene permisos para realizar esta operación.")
        {
        }

        public PermisoException(string message) : base(message)
        {
        }

        public PermisoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PermisoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}