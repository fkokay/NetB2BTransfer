using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Logo.Library.Class
{
    [DataContract]
    public class ErrorFault
    {
        [DataMember(Name = "ErrMessage")]
        public string ErrMessage { get; set; }
        [DataMember(Name = "ExceptionMessage")]
        public string ExceptionMessage { get; set; }
        [DataMember(Name = "ErrorCode")]
        public int ErrorCode { get; set; }


        public override string ToString()
        {
            return "ErrorCode=" + ErrorCode + " ExceptionMessage=" + ErrMessage + " ExceptionMessage=" + ExceptionMessage;
        }
    }
}

