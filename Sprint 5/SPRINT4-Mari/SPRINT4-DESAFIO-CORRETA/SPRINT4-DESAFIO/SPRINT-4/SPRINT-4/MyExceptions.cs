using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NomeException
{
    public class NomeExceptions : Exception

    {
        public NomeExceptions() { }

        public NomeExceptions(string message) : base(message) { }

        public NomeExceptions(string message, Exception innerException) : base(message, innerException) { }
    }
}
