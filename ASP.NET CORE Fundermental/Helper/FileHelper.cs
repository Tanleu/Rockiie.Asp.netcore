using System.Text.Json;
using System.Text.Json.Serialization;

namespace Second_Lesson_ASP.Core_MVC.Helper
{
    public class FileHelper
    {
        private static FileHelper fileHelper;
        public static FileHelper GetFileHelperInstace(){
            if(fileHelper is null) fileHelper = new FileHelper();
            return fileHelper;
        }

        public void SerializeThenSaveFileToAPath<T>(string path, string fileName,T obj)
        {
            try
            {
                //Save path by date
                string savingFolderPath = Path.Combine(path, DateTime.Now.ToString("yyyy/MM/dd"));
                string savingFilePath = Path.Combine(savingFolderPath, fileName);
                
                //Check part exists
                if(!Directory.Exists(savingFolderPath))
                {
                    Directory.CreateDirectory(savingFolderPath);
                }

                //Convert object to string            
                string content = JsonSerializer.Serialize<T>(obj);

                //Check file exists
                if(File.Exists(savingFilePath))
                {
                    File.Delete(savingFilePath);
                }

                //Save file
                // File.Create(savingFilePath);
                File.WriteAllText(savingFilePath, content);                
            }
            catch(Exception exception)
            {
                throw new Exception($"FileHelper's error, SerializeThenSaveFileToAPath, fail to save file to path {exception.Message}");
            }
            
        }
    
        public T DeserializeFromAPath<T>(string path, string fileName)
        {
            //Save path by date
            string savingFolderPath = Path.Combine(path, DateTime.Now.ToString("yyyy/MM/dd"));
            string savingFilePath = Path.Combine(savingFolderPath, fileName);

            if(!File.Exists(savingFilePath))
                return default(T);

            return JsonSerializer.Deserialize<T>(File.ReadAllText(savingFilePath));
        }
    }
}