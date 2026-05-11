namespace Server.Common
{
    public class Result
    {
        public bool IsSucces { get; }
        public string? Error { get; }
        public ErrorType? ErrorType { get; }

        protected Result(bool isSucces, string? error, ErrorType? errorType)
        {
            IsSucces = isSucces;
            Error = error;
            ErrorType = errorType;
        }

        public static Result Success()
        {
            return new Result(true, null, null);
        }

        public static Result Failure(string error, ErrorType errorType)
        {
            return new Result(false, error, errorType);
        }
    }
}
