namespace Cart.Api.Messaging
{
    using Application.Service;
    using Common.Messaging;
    using MassTransit;
    using System.Threading.Tasks;
    public class OrderCompletedEventConsumer: IConsumer<OrderCompletedEvent>
    {
        private readonly ICartService _cartService;
        public OrderCompletedEventConsumer(ICartService cartService)
        {
            _cartService = cartService;
        }

        public Task Consume(ConsumeContext<OrderCompletedEvent> context)
        {
            return _cartService.DeleteCartAsync(context.Message.BuyerId);
        }
    }
}
