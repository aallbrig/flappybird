using Core;
using NUnit.Framework;

namespace Tests.EditMode
{
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
            var bird = Bird.Factory();
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
            var bird = Bird.Factory(testWings);
            var birdHasFlappedWings = false;
            bird.FlappedWings += (sender, args) => birdHasFlappedWings = true;

            bird.FlapWings();

            Assert.IsTrue(testWings.Flapped);
            Assert.IsTrue(birdHasFlappedWings);
        }

        [Test]
        public void BirdCanSuccessfullyNavigateObstacles()
        {
            var bird = Bird.Factory();
            var obstacle = new Obstacle();
            var successfulNavigation = false;
            bird.NavigatedObstacleSuccessfully += (sender, args) => successfulNavigation = args == obstacle;

            bird.NavigateSuccessful(obstacle);

            Assert.IsTrue(successfulNavigation);
        }
    }
}