namespace Catalog.Products.EventHandlers
{
    public class ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger)
        : INotificationHandler<ProductPriceChangedEvent>
    {
        public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            // todo : publish product price changed integration event for update basket prices
            logger.LogInformation("Domain Event {DomainEvent} handled", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
