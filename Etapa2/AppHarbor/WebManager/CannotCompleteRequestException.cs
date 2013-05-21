using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebManager
{
    public class CannotCompleteRequestException : Exception
    {
        public CannotCompleteRequestException(string text)
            : base(text)
        {}
    }
}
