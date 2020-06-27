using System;
using System.Threading.Tasks;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Interface;
using NerdStore.Core.Bus;

namespace NerdStore.Catalogo.Domain.Services
{
    /*
        Serviço de domínio
        O serviço de estoque pertence ao Catalogo
        O serviço do domínio expressa uma necessidade do negócio
        E também é um item da linguagem ubíqua
        Serviço cross-agragete
        Quando sua regra não cabe nem na camada de aplicação nem na entidade
        Utilize serviços de dominio para representar açōes conhecidas pela sua linguagem ubíqua, e tem que fazer parte da sua regra de negócio
    */
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatrHandler _bus;

        // Pensar em açōes de negócio com Debitar Estoque

        public EstoqueService(IProdutoRepository repository, IMediatrHandler bus)
        {
            _produtoRepository = repository;
            _bus = bus;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            Produto produto = await _produtoRepository.ObterPorId(produtoId);

            if (!produto.PossuiEstoque(quantidade) || produto == null) return false;

            produto.DebitarEstoque(quantidade);

            // Isso é trabalhar com eventos
            if (produto.QuantidadeEstoque < 10)
                await _bus.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));

            _produtoRepository.Atualizar(produto);

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            Produto produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose() => _produtoRepository.Dispose();
    }
}