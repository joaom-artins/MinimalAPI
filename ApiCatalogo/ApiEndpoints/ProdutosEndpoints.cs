using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.ApiEndpoints
{
    public static class ProdutosEndpoints
    {
        public static void MapProdutosEndpoints(this WebApplication app)
        {
            app.MapPost("/produtos", async (Produto produto, AppDbContext db) =>
            {
                await db.Produtos.AddAsync(produto);

                await db.SaveChangesAsync();
                return Results.Created($"/produtos/{produto.ProdutoId}", produto);
            });

            app.MapGet("/produtos", async (AppDbContext db) => await db.Produtos.ToListAsync()).WithTags("Produtos").RequireAuthorization();

            app.MapGet("/produtos/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Produtos.FindAsync(id)
                is Produto produto ? Results.Ok(produto) : Results.NotFound();
            });

            app.MapPut("/produtos/{id:int}", async (int id, Produto produto, AppDbContext db) =>
            {
                if (produto.ProdutoId != id)
                {
                    return Results.BadRequest();
                }

                var produtoAtt = await db.Produtos.FindAsync(id);
                if (produtoAtt is null) return Results.NotFound();

                produtoAtt.Nome = produto.Nome;
                produtoAtt.Descricao = produto.Descricao;
                produtoAtt.Estoque = produto.Estoque;
                produtoAtt.Preco = produto.Preco;
                produtoAtt.ImagemUrl = produto.ImagemUrl;
                produtoAtt.CategoriaId = produto.CategoriaId;

                await db.SaveChangesAsync();
                return Results.Ok(produtoAtt);
            });

            app.MapDelete("/produto/{id:int}", async (int id, AppDbContext db) =>
            {
                var produtoDb = await db.Produtos.FindAsync(id);
                if (produtoDb is null) return Results.NotFound();

                db.Produtos.Remove(produtoDb);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
