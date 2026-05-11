namespace Server.Common.Result
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public ErrorType? ErrorType { get; }

        protected Result(bool isSuccess, string? error, ErrorType? errorType)
        {
            IsSuccess = isSuccess;
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
