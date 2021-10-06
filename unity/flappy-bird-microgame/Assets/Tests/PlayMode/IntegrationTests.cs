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

    public class Bird
    {
        [SetUp]
        public void Setup()
        {
            Time.timeScale = 10F;
        }
        
        [UnityTest]
        public IEnumerator BirdFalls()
        {
            var bird = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Bird"));
            var birdOriginalPosition = bird.transform.position;

            yield return new WaitForSeconds(1);

            Assert.AreNotEqual(birdOriginalPosition, bird.transform.position);
        }

        [UnityTest]
        public IEnumerator BirdCanFly()
        {
            var bird = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Bird"));
            yield return null; // Allow unity lifecycle methods to be called
            var script = bird.GetComponent<BirdBehaviour>();
            var rigidBody = bird.GetComponent<Rigidbody>();
            var originalVelocity = rigidBody.velocity.normalized;
            
            script.Fly();
            yield return new WaitForSeconds(0.2f);

            Assert.IsTrue(originalVelocity.y < rigidBody.velocity.normalized.y);
        }
    }
}