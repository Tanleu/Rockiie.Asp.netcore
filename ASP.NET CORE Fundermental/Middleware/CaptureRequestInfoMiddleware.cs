using Second_Lesson_ASP.Core_MVC.AppConfig;
using Second_Lesson_ASP.Core_MVC.Helper;
using Second_Lesson_ASP.Core_MVC.Interface;
using Second_Lesson_ASP.Core_MVC.Models;
using System.Globalization;

namespace Second_Lesson_ASP.Core_MVC.Middleware
{
    /// <summary>
    /// Add the Capture middleware to pipeline user request
    ///
    /// </summary>
    public static class CaptureRequestInfoMiddlewareExtension
    {
        public static IApplicationBuilder UseCaptureRequest(this IApplicationBuilder builder)
        {
            IRequestCapture requestCapture = new CaptureRequestThenSaveToAFile();
            return builder.UseMiddleware<CaptureRequestInfoMiddleware>();
        }
    }

    /// <summary>
    /// Use dependency injection
    /// If customer want to change save request on server to send email notification or smth, we dont have to modify this class.
    /// Write middleware by reading this article : https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-6.0
    /// </summary>
    public class CaptureRequestInfoMiddleware
    {
        private IRequestCapture requestCapture1;
        /// <summary>
        /// Injector contructor
        /// </summary>
        /// <param name="requestCapture"></param>
        // public CaptureRequestInfoMiddleware(IRequestCapture requestCapture)
        // {
        //     requestCapture1 = requestCapture;
        // } exclude this contructor because we have another contructor CaptureRequestInfoMiddleware
        public void Capture(HttpRequest httpRequest)
        {
            requestCapture1.CaptureThenDoAction(httpRequest);
        }

        private readonly RequestDelegate _next;
        
        public CaptureRequestInfoMiddleware(RequestDelegate next)
        {
            requestCapture1 = new CaptureRequestThenSaveToAFile();
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            requestCapture1.CaptureThenDoAction(context.Request);
        }

    }

    /// <summary>
    /// Save request info to a file on server
    /// </summary>
    public class CaptureRequestThenSaveToAFile : IRequestCapture
    {
        public void CaptureThenDoAction(HttpRequest request)
        {
            string captureRequestSavingPath = CommonConfig.GetConfig().CaptureRequestSavingPath;
            string captureRequestFileName = CommonConfig.GetConfig().CaptureRequestFileName;

            //Get hist saved request
            CaptureRequestSavingFileModel hist
                = FileHelper.GetFileHelperInstace()
                            .DeserializeFromAPath<CaptureRequestSavingFileModel>(captureRequestSavingPath, captureRequestFileName);

            if (hist is null) hist = new CaptureRequestSavingFileModel();

            //Get request info
            CaptureRequestModel captureRequestModel = new CaptureRequestModel();
            captureRequestModel.Schema = request.Scheme;
            captureRequestModel.Host = request.Host.ToString();
            captureRequestModel.Path = request.Path.ToString();
            captureRequestModel.Querystring
                = request.QueryString.ToString() ==  "" ? 
                    new List<QueryStringModel>() : // Are there any better way to return an empty list?(Issue: if return null, a warning is rasied)
                    request.QueryString.ToString()
                        .Split('&')
                        .Select(x => new QueryStringModel() 
                                        {   Key = x.PadLeft(x.IndexOf('=')), 
                                            Value = x.PadRight(x.Length - x.IndexOf('=')) 
                                        })
                        .ToList();
            captureRequestModel.RequestBody = request.Body.ToString() ?? "";//new StreamReader(request.Body).ReadToEnd();//request.Body.ToString();

            hist.CaptureRequests.Add(captureRequestModel);

            //Save file
            FileHelper.GetFileHelperInstace()
                .SerializeThenSaveFileToAPath<CaptureRequestSavingFileModel>(captureRequestSavingPath, captureRequestFileName, hist);
        }
    }
}