namespace TableGenius.Api.Web.Default;

public class SingleDataJsonResult<T> : InfoJsonResult where T : class
{
    public SingleDataJsonResult(int statusCode, string message, T data) : base(statusCode, message)
    {
        Data = data;
    }

    public T Data { get; set; }
}