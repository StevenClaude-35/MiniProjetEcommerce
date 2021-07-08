using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCommercial.Services.Exceptions
{
    public class AjouterClientException : Exception
    {
        public AjouterClientException(string message) : base(message)
        {
        }
    }
}
