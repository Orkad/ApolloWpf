using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Apollo.Sample.Model.Business
{
    /// <summary>
    /// Représente une exception fonctionnelle
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException() { }
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, Exception inner) : base(message, inner) { }
        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
