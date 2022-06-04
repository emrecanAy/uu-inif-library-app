using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Core.Exceptions
{
    public class NotNullException : Exception
    {
        //public NotNullException() : base() {}
        
        public NotNullException(string message) : base(message)
        {
           
        }
        //protected NotNullException(SerializationInfo info, StreamingContext ctxt)
        //: base(info, ctxt)
        //{ }
    


}
}
