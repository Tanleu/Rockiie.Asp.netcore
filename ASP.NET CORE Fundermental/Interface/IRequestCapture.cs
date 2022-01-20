namespace Second_Lesson_ASP.Core_MVC.Interface
{
    public interface IRequestCapture
    {
        void CaptureThenDoAction(HttpRequest request);
    }
}