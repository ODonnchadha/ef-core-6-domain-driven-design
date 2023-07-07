namespace PublisherSystem.SharedKernel
{
    using MediatR;
    public abstract class BaseDomainEvent : INotification
    {
        public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
    }
}
