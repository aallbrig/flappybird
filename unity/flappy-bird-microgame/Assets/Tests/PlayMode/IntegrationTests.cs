using System.Collections;
using MonoBehaviours;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class Obstacles
    {
        [Test]
        public void AssetLoadable()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Obstacle");
            var obstacle = Object.Instantiate(prefab);
            Assert.NotNull(obstacle);
        }
    }

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
    }
}