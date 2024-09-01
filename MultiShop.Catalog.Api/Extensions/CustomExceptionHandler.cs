using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Api.Exceptions.Exceptions;
using MultiShop.Catalog.Api.ResponceDtos.Dtos;
using System.Text.Json;

namespace MultiShop.Catalog.Api.Extensions
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (errorFeature != null)
                    {
                        var ex = errorFeature.Error;

                        ErrorDto error = null;

                        if (ex is CustomException)
                        {
                            error = new ErrorDto(ex.Message, true);
                        }
                        else
                        {
                            error = new ErrorDto(ex.Message, false);
                        }

                        var response = Response<NoContentResult>.Fail(error, 500);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                });
            });
        }
    }
}
