namespace Licenses.ViewModels
{
    public class ResultViewModel<T>
    {
        public string Message { get; set; }
        public bool State { get; set; }
        public T? Result { get; set; }
        public static ResultViewModel<T> Success(T data,string message="done")
        {
            return new ResultViewModel<T>()
            {
                State = true,
                Result = data,
                Message = message
            };

        }
        public static ResultViewModel<T> Failure(string message)
        {
            return new ResultViewModel<T>()
            {
                State = false,
                Message = message,
                Result=default

                
            };
        }

    }
}
