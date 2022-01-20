namespace Second_Lesson_ASP.Core_MVC.Models
{
    public class CaptureRequestSavingFileModel
    {
        public CaptureRequestSavingFileModel()
        {
            CaptureRequests = new List<CaptureRequestModel>();
        }
        public List<CaptureRequestModel> CaptureRequests { get; set; }
        
    }

    public class CaptureRequestModel
    {
        public CaptureRequestModel()
        {
            Querystring = new List<QueryStringModel>();
        }
        public string Schema { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public List<QueryStringModel> Querystring { get; set; }
        public string RequestBody { get; set; }

    }

    public class QueryStringModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

}