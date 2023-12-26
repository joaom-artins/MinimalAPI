using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ApiCatalogo.ApiEndpoints
{
    public static class CategoriaEndpoints
    {
        public static void MapCategoriasEndpoints(this WebApplication app)
        {
            //app.MapGet("/", () => "Catalogo de produtos 2023");
            app.MapGet("/categorias", async (AppDbContext db) => await db.Categorias.ToListAsync()).WithTags("Categoria").RequireAuthorization();

            app.MapGet("/categorias/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Categorias.FindAsync(id)
                is Categoria categoria ? Results.Ok(categoria) : Results.NotFound();
            });
            app.MapPost("/categorias", async (Categoria categoria, AppDbContext db) =>
            {
                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();

                return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);
            });
            app.MapPut("/categorias/{id:int}", async (int id, Categoria categoria, AppDbContext db) =>
            {
                if (categoria.CategoriaId != id)
                {
                    return Results.BadRequest();
                }

                var CategoriaDb = await db.Categorias.FindAsync(id);
                if (CategoriaDb is null) return Results.NotFound();

                CategoriaDb.Nome = categoria.Nome;
                CategoriaDb.Descricao = categoria.Descricao;

                await db.SaveChangesAsync();
                return Results.Ok(CategoriaDb);
            });

            app.MapDelete("/categorias/{id:int}", async (int id, AppDbContext db) =>
            {
                var CategoriaDb = await db.Categorias.FindAsync(id);

                if (CategoriaDb is null)
                {
                    return Results.NotFound();
                }
                db.Categorias.Remove(CategoriaDb);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
