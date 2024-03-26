using System.Collections.Generic;

namespace Common.Model
{
    public class Response<T>
    {
        public Response()
        {
            ListData = new List<T>();
            Message = string.Empty;
            MessageType = "Error";
            MessageClass = "danger";
            MessageTitle = "¡Algo salió mal!";
        }

        public ResponseCode Code { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
        public string MessageClass { get; set; }
        public string MessageTitle { get; set; }
        public List<T> ListData { get; set; }
        public T Data { get; set; }
    }
}