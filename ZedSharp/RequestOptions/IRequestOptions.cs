using System.Collections.Generic;

namespace ZedSharp.RequestOptions
{
    public interface IRequestOptions
    {
        Dictionary<string, object> GetRiotOptions();
    }
}
