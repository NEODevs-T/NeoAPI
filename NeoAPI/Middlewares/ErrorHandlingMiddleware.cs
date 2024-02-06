
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Text.Json;

namespace NeoAPI.Middlewares
{
    public class ErrorHandlerMiddleware 
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next) 
        { 
            _next = next; 
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "aplication/json";
       
                switch (ex)
                {
                    case NeoAPI.Exeptions.ApiException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                  
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                  
                }
                var result = JsonSerializer.Serialize(new {message=ex?.Message});
                await context.Response.WriteAsync(result);
            }
        }

    }
}
