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
            var script = gameObject.AddComponent<BirdBehaviour>();
            script.NewBird();
            var flappedWings = false;
            script.Bird.FlappedWings += (sender, args) => flappedWings = true;

            script.Fly();

            Assert.IsTrue(flappedWings);
        }
    }
}