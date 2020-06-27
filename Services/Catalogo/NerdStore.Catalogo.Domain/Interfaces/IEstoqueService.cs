using System;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Services
{
    public interface IEstoqueService : IDisposable // O que é Disposable?
    {
        Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
        Task<bool> ReporEstoque(Guid produtoId, int quantidade);
    }
}