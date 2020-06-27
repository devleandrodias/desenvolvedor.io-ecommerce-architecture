using System;
using System.Threading.Tasks;
using NerdStore.Catalogo.Domain.Interface;

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

        // Pensar em açōes de negócio com Debitar Estoque

        public EstoqueService(IProdutoRepository repository)
        {
            _produtoRepository = repository;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            Produto produto = await _produtoRepository.ObterPorId(produtoId);

            if (!produto.PossuiEstoque(quantidade) || produto == null) return false;

            produto.DebitarEstoque(quantidade);

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