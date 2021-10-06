using core;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class NoopWings : IFly
    {
        public void Fly() {}
    }

    public class TestWings : IFly
    {
        public bool Flapped;
        public void Fly() => Flapped = true;
    }

    public class GameplayCore
    {
        [Test]
        public void BirdCanBeKilledByObstacle()
        {
            var bird = new Bird(new NoopWings());
            var birdHasDied = false;
            bird.Died += (sender, args) => birdHasDied = true;
            var obstacle = new Obstacle();

            obstacle.Kill(bird);

            Assert.IsTrue(birdHasDied);
        }

        [Test]
        public void BirdCanFlap()
        {
            var testWings = new TestWings();
            var bird = new Bird(testWings);
            var birdHasFlappedWings = false;
            bird.FlappedWings += (sender, args) => birdHasFlappedWings = true;

            bird.FlapWings();

            Assert.IsTrue(birdHasFlappedWings);
            Assert.IsTrue(testWings.Flapped);
        }

        [Test]
        public void BirdCanSuccessfullyNavigateObstacles()
        {
            var bird = new Bird(new NoopWings());
            var obstacle = new Obstacle();
            var successfulNavigation = false;
            bird.NavigatedObstacleSuccessfully += (sender, args) => successfulNavigation = args == obstacle;

            bird.NavigateSuccessful(obstacle);

            Assert.IsTrue(successfulNavigation);
        }
    }
}