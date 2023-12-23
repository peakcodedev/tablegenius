using System.Collections.Generic;

namespace TableGenius.Api.Web.Default;

public class DataJsonResult<T> : InfoJsonResult where T : class
{
    public DataJsonResult(int statusCode, string message, IEnumerable<T> data) : base(statusCode, message)
    {
        Data = data;
    }

    public IEnumerable<T> Data { get; set; }
}