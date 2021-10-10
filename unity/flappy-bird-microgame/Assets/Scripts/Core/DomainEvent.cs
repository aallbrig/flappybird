namespace Core
{
    public abstract class DomainEvent {}

    public interface IDomainEventHandler<in DomainEvent>
    {
        void Handle(DomainEvent evt);
    }
}