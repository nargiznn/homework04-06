using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace University.API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string logDirectory;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                    await _next(context);

                    context.Response.Body.Position = 0;
                    var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                    context.Response.Body.Position = 0;

                    var logText = $"{DateTime.Now}: {context.Response.StatusCode} - {context.Request.GetDisplayUrl()}\n{responseBodyText}\n";

                    await WriteLogAsync(logText);

                    await Console.Out.WriteLineAsync(logText);

                    await responseBody.CopyToAsync(originalBodyStream);
                    context.Response.Body = originalBodyStream;

            }
        }

        private async Task WriteLogAsync(string logText)
        {
            var logFilePath = Path.Combine(logDirectory, $"{DateTime.Now:yyyy-MM-dd}.txt");

            await using (var streamWriter = new StreamWriter(logFilePath, true))
            {
                await streamWriter.WriteLineAsync(logText);
            }
        }
    }
}
