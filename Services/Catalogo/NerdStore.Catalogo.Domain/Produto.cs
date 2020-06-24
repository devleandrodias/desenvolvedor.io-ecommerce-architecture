using System;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain
{
    public class Produto : Entity, IAggregateRoot // Interfaces de marcação
    {
        public Produto(
            string nome, string descricao, bool ativo, decimal valor, DateTime dataCadastro, string imagem, Guid categoriaId)
        {
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            DataCadastro = dataCadastro;
            Imagem = imagem;
            CategoriaId = categoriaId;
            Validar();
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Imagem { get; private set; }
        public int QuantidadeEstoque { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Categoria Categoria { get; private set; }

        public void Ativar() => Ativo = true; // Add Rock Seter
        public void Desativar() => Ativo = false;
        public void AlterarCategoria(Categoria categoria)
        {
            Categoria = categoria;
            CategoriaId = categoria.Id;
        }
        public void AlterarDescricao(string novaDescricao)
        {
            Validacoes.ValidarSeVazio(novaDescricao, "O campo descrição do produto não pode ser vazio");
            Descricao = novaDescricao;
        }
        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;
            if (!PossuiEstoque(quantidade)) throw new DomainException("Estoque insuficiente");
            QuantidadeEstoque -= quantidade;
        }
        public void ReporEstoque(int quantidade) => QuantidadeEstoque += quantidade;
        public bool PossuiEstoque(int quantidade) => QuantidadeEstoque > quantidade;
        public void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo nome do produto não pode ser vazio");
            Validacoes.ValidarSeVazio(Descricao, "O campo descricao não pode ser vazio");
            Validacoes.ValidarSeDiferente(CategoriaId, Guid.Empty, "O campo categoriaId não pode ser nulo");
            Validacoes.ValidarSeMenorQue(Valor, 0, "O campo valor não pode ser menor ou igual a zero");
            Validacoes.ValidarSeVazio(Imagem, "O campo imagem do produto não pode ser vazio");
        }
    }
}