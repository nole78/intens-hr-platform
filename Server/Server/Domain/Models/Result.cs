using Server.Domain.Enums;

namespace Server.Domain.Models
{
    public class Result<T>
    {
        public bool IsSucces { get; }
        public T? Value { get; }
        public string? Error { get; }
        public ErrorType? ErrorType { get; }

        private Result(bool isSucces, T? value, string? error, ErrorType? errorType)
        {
            IsSucces = isSucces;
            Value = value;
            Error = error;
            ErrorType = errorType;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, null, null);
        }

        public static Result<T> Failure(string error, ErrorType errorType)
        {
            return new Result<T>(false, default, error, errorType);
        }
    }
}
