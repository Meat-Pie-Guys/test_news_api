using System;

namespace NewsApi.Exceptions
{
    /// <summary>
    /// Thrown if news with specific id is not found
    /// </summary>
    public class NewsNotFoundException : Exception { }
}