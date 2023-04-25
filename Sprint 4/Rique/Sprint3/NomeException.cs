using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint3
{
    internal class NomeException : Exception
    {
        public NomeException(string mensagem)
            : base (mensagem)
        {

        }
    }
}
