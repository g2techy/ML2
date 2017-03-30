using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace G2.Frameworks.Core
{
    [Serializable]
    public class BaseException : System.Exception
    {
        public static string DefaultErrorNumber = "E000000";
        public string ErrorID { get; private set; }
        public string ErrorNumber { get; private set; }
        public bool IsLogged { get; set; }

        public BaseException(string errorNumber) : this(errorNumber, string.Empty)
        {}

        public BaseException(string errorNumber, string message) : this(errorNumber, message, null)
        {}

        public BaseException(string errorNumber, string message, System.Exception inner) : base(message, inner)
        {
            ErrorNumber = errorNumber;
            AssignErrorID();
        }

        public BaseException(string errorNumber, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info != null)
            {
                this.ErrorNumber = info.GetString("ErrorNumber");
                this.ErrorID = info.GetString("ErrorID");
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("ErrorNumber", this.ErrorNumber);
                info.AddValue("ErrorID", this.ErrorID);
            }
        }

        private void AssignErrorID()
        {
            ErrorID = NewErrorID;
        }

        public static string NewErrorID
        {
            get
            {
                return DateTime.Now.ToString("yyyyMMddHHmmss");
            }
        }
    }
}
