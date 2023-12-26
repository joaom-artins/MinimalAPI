using ApiCatalogo.Models;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApiCatalogo.ApiEndpoints
{
    public static class AutenticacaoEndpoints
    {
        public static void MapAutenticacaoEndpoints(this WebApplication app)
        {
            app.MapPost("/login", [AllowAnonymous] (UserModel user, ITokenService tokenService) =>
            {
                if (user == null)
                {
                    return Results.BadRequest();
                }
                if (user.UserName == "joao" && user.Password == "numsey#123")
                {
                    var tokenString = tokenService.GerarToken(app.Configuration["Jwt:Key"],
                        app.Configuration["Jwt:Issuer"],
                        app.Configuration["Jwt:Audience"],
                        user);
                    return Results.Ok(new { token = tokenString });
                }
                else
                {
                    return Results.BadRequest("Login invalido");
                }
            }).Produces(StatusCodes.Status400BadRequest).Produces(StatusCodes.Status200OK).WithName("Login").WithTags("Autenticacao");
        }
    }
}
