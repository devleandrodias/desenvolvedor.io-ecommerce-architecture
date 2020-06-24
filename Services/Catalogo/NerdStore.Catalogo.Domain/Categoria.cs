using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain
{
    public class Categoria : Entity
    {
        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;

            Validar();
        }

        public string Nome { get; private set; }
        public int Codigo { get; private set; }

        public override string ToString() => $"{Nome} - {Codigo}";

        public void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O nom da categoria não pode ser vazio");
            Validacoes.ValidarSeIgual(Codigo, 0, "O código não pode ser 0");
        }
    }
}