using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint4
{
    internal class StatusException : Exception
    {
        public StatusException(string mensagem) : base(mensagem)
        {

        }
    }
}
