﻿using System.Text.Json.Serialization;

namespace ApiCatalogo.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public string? ImagemUrl { get; set; }
        public DateTime DataDaCompra { get; set; }
        public int Estoque { get; set; }
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public Categoria Categoria { get; set; }
    }
}
