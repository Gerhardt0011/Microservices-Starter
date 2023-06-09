using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Common.Logging;

public static class Extensions
{
    public static ILoggingBuilder AddJsonLogging(this ILoggingBuilder builder)
    {
        return builder.AddJsonConsole(opt =>
        {
            opt.JsonWriterOptions = new JsonWriterOptions
            {
                Indented = true
            };
        });
    }
}