using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class DbAccessException : Exception
    {

        public DbAccessException()
        {
        }

        public DbAccessException(string message)
            : base(message)
        {
        }

        public DbAccessException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
