using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Exeptions
{
    public class InternalServerErrorException : Exception
    {
        private const string InternalServerErrorMessage = "The Server has encountered an error.";

        public  InternalServerErrorException () 
            : this(InternalServerErrorMessage)
        {

        }

        public InternalServerErrorException(string name ) 
            : base(name)
        {

        }
    }
}
