using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Catalogo.Domain.Interface;

namespace NerdStore.Catalogo.Domain.Events
{
    public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoEventHandler(IProdutoRepository produtorRepository)
        {
            _produtoRepository = produtorRepository;
        }

        public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken)
        {
            Produto produto = await _produtoRepository.ObterPorId(mensagem.AggregateId);

            // Enviar email para aquisição de mais produtos
        }
    }
}