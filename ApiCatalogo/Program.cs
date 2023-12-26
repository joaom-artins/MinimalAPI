using ApiCatalogo.ApiEndpoints;
using ApiCatalogo.AppServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAuthenticationJwt();
    
var app = builder.Build();

app.MapAutenticacaoEndpoints();

app.MapCategoriasEndpoints();

app.MapProdutosEndpoints();

var environment = app.Environment;
app.UseExcenptionHandler(environment)
    .UseSwaggerMiddleware()
    .UseAppCors();




app.UseAuthentication();
app.UseAuthorization();
app.Run();

