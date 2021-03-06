using System;

namespace Core
{
    public interface IFly
    {
        public void Fly();
    }

    public interface IContainBird
    {
        public Bird Bird { get; }
    }

    public class NoopWings : IFly
    {
        public void Fly() {}
    }

    public class Bird : IKillable, IRewardable
    {

        private readonly IFly _wings;
        private Bird(IFly wings) => _wings = wings ?? throw new ArgumentNullException(nameof(wings));

        public bool IsAlive { get; private set; } = true;

        public void Kill() => Die();
        public void Reward() => NavigateSuccessful(new Obstacle());

        public static Bird Of(IFly wings = null) => new Bird(wings ?? new NoopWings());

        public event EventHandler Died;

        public event EventHandler FlappedWings;

        public event EventHandler<Obstacle> NavigatedObstacleSuccessfully;

        private void Die()
        {
            IsAlive = false;
            Died?.Invoke(this, EventArgs.Empty);
        }

        public void FlapWings()
        {
            _wings.Fly();
            FlappedWings?.Invoke(this, EventArgs.Empty);
        }

        public void NavigateSuccessful(Obstacle obstacle) => NavigatedObstacleSuccessfully?.Invoke(this, obstacle);
    }
}