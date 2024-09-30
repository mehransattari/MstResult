
namespace Mst.Results;

public class Result<T> : Result
{
    public Result() : base()
    {
    }

    public T Value { get; set; }
}
