using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain.ValueObjects
{
    public class Dimensoes
    {
        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Validacoes.ValidarSeMenorQue(Altura, 1, "O campo altura não pode ser menor que 1");
            Validacoes.ValidarSeMenorQue(Largura, 1, "O campo largura não pode ser menor que 1");
            Validacoes.ValidarSeMenorQue(Profundidade, 1, "O campo profundidade não pode ser menor que 1");

            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
        }

        public decimal Altura { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Profundidade { get; private set; }

        public string DescricaoFormatada() => $"LxAxP: {Largura} x {Altura} x {Profundidade}";

        public override string ToString() => DescricaoFormatada();
    }
}