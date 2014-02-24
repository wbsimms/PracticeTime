using System.Collections.Generic;

namespace PracticeTime.Web.Controllers
{
    public class ResponseMessage
    {
        public ResponseMessage()
        {
            this.Errors = new List<string>();
        }

        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public bool HasMessage
        {
            get { return !string.IsNullOrEmpty(Message); }
        }
        public bool HasErrors
        {
            get { return !(Errors == null || Errors.Count == 0); }
        }
    }
}