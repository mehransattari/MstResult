namespace Mst.Results;

public static class ResultExtensions
{
    static ResultExtensions()
    {
    }

    public static Result ConvertToMstResult(this FluentResults.Result result)
    {
        Result mstResult = new()
        {
            IsFailed = result.IsFailed,
            IsSuccess = result.IsSuccess,
        };

        if (result.Errors != null)
        {   
            foreach (var item in result.Errors)
            {
                mstResult.AddErrorMessage(message: item.Message);
            }
        }

        if (result.Successes != null)
        {
            foreach (var item in result.Successes)
            {
                mstResult.AddSuccessMessage(message: item.Message);
            }
        }

        return mstResult;
    }

    public static Result<T> ConvertToMstResult<T>(this FluentResults.Result<T> result)
    {
        Result<T> mstResult = new()
        {
            IsFailed = result.IsFailed,
            IsSuccess = result.IsSuccess,
        };

        if (result.IsFailed == false)
        {
            mstResult.Value = result.Value;
        }

        if (result.Errors != null)
        {
            foreach (var item in result.Errors)
            {
                mstResult.AddErrorMessage(message: item.Message);
            }
        }

        if (result.Successes != null)
        {
            foreach (var item in result.Successes)
            {
                mstResult.AddSuccessMessage(message: item.Message);
            }
        }

        return mstResult;
    }
}