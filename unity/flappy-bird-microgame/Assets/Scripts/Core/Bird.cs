using System;

namespace Core
{
    public interface IFly
    {
        public void Fly();
    }

    public class NoopWings : IFly
    {
        public void Fly() {}
    }

    public class Bird: IKillable
    {

        private readonly IFly _wings;
        private Bird(IFly wings) => _wings = wings ?? throw new ArgumentNullException(nameof(wings));
        public static Bird Factory(IFly wings = null) => new Bird(wings ?? new NoopWings());

        public event EventHandler Died;

        public event EventHandler FlappedWings;

        public event EventHandler<Obstacle> NavigatedObstacleSuccessfully;

        private void Die() => Died?.Invoke(this, EventArgs.Empty);

        public void FlapWings()
        {
            _wings.Fly();
            FlappedWings?.Invoke(this, EventArgs.Empty);
        }
        public void NavigateSuccessful(Obstacle obstacle) => NavigatedObstacleSuccessfully?.Invoke(this, obstacle);
        public void Kill() => Die();
    }
}