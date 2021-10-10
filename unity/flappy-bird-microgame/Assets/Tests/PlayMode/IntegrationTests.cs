using System.Collections;
using MonoBehaviours;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class Game
    {
        [SetUp] public void Setup() => Time.timeScale = 10F;
        [TearDown] public void Teardown() => Time.timeScale = 0F;

        [UnityTest]
        public IEnumerator PlayersBirdFalls()
        {
            var bird = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Bird"));
            var birdOriginalPosition = bird.transform.position;

            yield return new WaitForSeconds(1);

            Assert.AreNotEqual(birdOriginalPosition, bird.transform.position);
        }

        [UnityTest]
        public IEnumerator PlayersBirdCanFly()
        {
            var bird = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Bird"));
            yield return null; // allow MB lifecycle methods to be called
            var rigidbody = bird.GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.velocity = Vector3.zero;
            var beforeVelocity = rigidbody.velocity;
            var script = bird.GetComponent<BirdBehaviour>();

            script.Fly();
            yield return null;

            Assert.IsTrue(rigidbody.velocity.normalized.y > beforeVelocity.normalized.y);
        }

        [UnityTest]
        public IEnumerator ObstaclesKillBirds()
        {
            var bird = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Bird"));
            yield return null; // allow MB lifecycle methods to be called
            var script = bird.GetComponent<BirdBehaviour>();
            var birdIsDead = false;
            script.Bird.Died += (sender, args) => birdIsDead = true;

            var obstacle = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Obstacle"));
            yield return null;

            bird.transform.position = Vector3.zero;
            obstacle.transform.position = Vector3.zero;
            yield return null;

            Assert.IsTrue(birdIsDead);
        }
    }
}