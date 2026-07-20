namespace Licenses.ViewModels
{
    public class ErrorViewModelLicenses
    {
        public ErrorViewModelLicenses(string title,string message) 
        {
            Title = title;
            Message= message;

        }
        public string Title { get; }

        public string Message { get;  }

    }
}
