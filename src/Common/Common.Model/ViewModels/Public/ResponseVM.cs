namespace Common.Model
{
    public class ResponseVM
    {
        public ResponseVM()
        {
            MessageType = "OK";
            MessageClass = "success";
            MessageText = string.Empty;
        }
        public string MessageType { get; set; }
        public string MessageClass { get; set; }
        public string MessageTitle { get; set; }
        public string MessageText { get; set; }
        public int Operation { get; set; }
    }
}