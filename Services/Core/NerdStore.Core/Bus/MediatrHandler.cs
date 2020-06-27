using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Message;

namespace NerdStore.Core.Bus
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediatr;

        public MediatrHandler(IMediator mediator)
        {
            _mediatr = mediator;
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediatr.Publish(evento);
        }
    }
}