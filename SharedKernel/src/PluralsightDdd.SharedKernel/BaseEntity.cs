namespace PublisherSystem.SharedKernel
{
    /// <summary>
    /// Base types for all Entities which track state using a given Id.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public abstract class BaseEntity<TId>
    {
        /// <summary>
        /// Ensure that Entities have an Id.
        /// </summary>
        public TId Id { get; protected set; }

        /// <summary>
        /// Entities must be able to maintain Domain events.
        /// </summary>
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
