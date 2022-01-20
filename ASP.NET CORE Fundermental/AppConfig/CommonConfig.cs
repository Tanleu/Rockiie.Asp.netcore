namespace Second_Lesson_ASP.Core_MVC.AppConfig
{
    public class CommonConfig
    {
        private static CommonConfig commonConfig;
        public string CaptureRequestSavingPath { get; set; }
        public string CaptureRequestFileName { get; set; }
        
        public static CommonConfig GetConfig()
        {
            if (commonConfig is null) commonConfig = new CommonConfig();
            return commonConfig;
        }

        private CommonConfig()
        {
            CaptureRequestSavingPath = "./Log/Request";
            CaptureRequestFileName = "RequestHist.json";
        }
    }
}