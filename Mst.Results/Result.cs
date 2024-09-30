using System.Text.Json.Serialization;

namespace Mst.Results;

public class Result
{
    public bool IsFailed { get; set; }
    public bool IsSuccess { get; set; }

    private readonly List<string> _errors;

    private readonly List<string> _successes;

    public Result()
    {
        IsSuccess = true;
        IsFailed = false;
        _errors = new List<string>();
        _successes = new List<string>();
    }

    [JsonIgnore]
    public IReadOnlyList<string> Errors => _errors;

    [JsonIgnore]
    public IReadOnlyList<string> Successes => _successes;

    public void AddErrorMessage(string message)
    {
        message = String.Fix(message);

        if (string.IsNullOrEmpty(message) || _errors.Contains(message)) return;

        _errors.Add(message);
        IsFailed = true;
        IsSuccess = false;
    }

    public void RemoveErrorMessage(string message)
    {
        message = String.Fix(message);

        if (string.IsNullOrEmpty(message)) return;

        _errors.Remove(message);

        if (_errors.Count == 0)
        {
            IsFailed = false;
            IsSuccess = true;
        }
    }

    public void ClearErrorMessages()
    {
        _errors.Clear();
        IsFailed = false;
        IsSuccess = true;
    }

    public void AddSuccessMessage(string message)
    {
        message = String.Fix(message);

        if (string.IsNullOrEmpty(message) || _successes.Contains(message)) return;

        _successes.Add(message);
    }

    public void RemoveSuccessMessage(string message)
    {
        message = String.Fix(message);

        if (string.IsNullOrEmpty(message)) return;

        _successes.Remove(message);
    }

    public void ClearSuccessMessages() => _successes.Clear();

    public void ClearAllMessages()
    {
        ClearErrorMessages();
        ClearSuccessMessages();
    }

}
