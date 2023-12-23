namespace TableGenius.Api.Web.Default;

public class InfoJsonResult
{
    public InfoJsonResult(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }
}