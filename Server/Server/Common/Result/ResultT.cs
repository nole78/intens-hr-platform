namespace Server.Common.Result.Result
{
    public class Result<T> : Result
    {
        public T? Value { get; }

        protected Result(bool isSucces, T? value, string? error, ErrorType? errorType)
            : base(isSucces, error, errorType)
        {
            Value = value;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, null, null);
        }

        public static new Result<T> Failure(string error, ErrorType errorType)
        {
            return new Result<T>(false, default, error, errorType);
        }
    }
}
