using System.Text.Json;
using System.Text.Json.Serialization;

namespace Second_Lesson_ASP.Core_MVC.Helper
{
    public class JsonHelper
    {
        private static JsonHelper jsonHelper;
        public static JsonHelper GetJsonHelperInstace(){
            if(jsonHelper is null) jsonHelper = new JsonHelper();
            return jsonHelper;
        }

    }
}