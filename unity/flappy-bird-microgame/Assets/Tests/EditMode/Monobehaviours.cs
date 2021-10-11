using MonoBehaviours;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class BirdBehaviourComponent
    {
        [Test]
        public void BirdsCanFly()
        {
            var gameObject = new GameObject();
            var sut = gameObject.AddComponent<BirdBehaviour>();
            var flappedWings = false;
            sut.Bird.FlappedWings += (sender, args) => flappedWings = true;

            sut.Fly();

            Assert.IsTrue(flappedWings);
        }
    }

    public class ObstacleComponent
    {
        [Test]
        public void ObstaclesCanKillThings()
        {
            var sut = new GameObject().AddComponent<BirdKiller>();
            var bird = new GameObject().AddComponent<BirdBehaviour>();
            var killed = false;
            bird.Bird.Died += (sender, args) => killed = true;

            sut.KillBird(bird);

            Assert.IsTrue(killed);
        }
    }

    public class RewarderComponent
    {
        [Test]
        public void BirdsCanBeRewarded()
        {
            var bird = new GameObject().AddComponent<BirdBehaviour>();
            var sut = new GameObject().AddComponent<RewardBehaviour>();
            var rewarded = false;
            bird.Bird.NavigatedObstacleSuccessfully += (sender, obstacle) => rewarded = true;

            sut.Reward(bird.Bird);

            Assert.True(rewarded);
        }
    }
}