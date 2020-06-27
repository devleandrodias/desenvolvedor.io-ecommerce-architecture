using System.Collections.Generic;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain.Entities
{
    public class Categoria : Entity
    {
        protected Categoria() { }

        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;

            Validar();
        }

        public string Nome { get; private set; }
        public int Codigo { get; private set; }

        // Entity Framework
        public ICollection<Produto> Produtos { get; set; }

        public override string ToString() => $"{Nome} - {Codigo}";

        public void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O nom da categoria não pode ser vazio");
            Validacoes.ValidarSeIgual(Codigo, 0, "O código não pode ser 0");
        }
    }
}