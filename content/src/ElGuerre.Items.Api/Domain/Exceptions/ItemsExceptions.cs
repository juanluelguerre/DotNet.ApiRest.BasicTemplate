using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElGuerre.Items.Api.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class ItemsException : Exception
    {
        public ItemsException()
        { }

        public ItemsException(string message)
            : base(message)
        { }

        public ItemsException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
