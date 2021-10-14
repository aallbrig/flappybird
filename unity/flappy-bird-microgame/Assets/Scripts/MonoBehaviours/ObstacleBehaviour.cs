using Core;
using UnityEngine;

namespace MonoBehaviours
{
    public class ObstacleBehaviour : MonoBehaviour
    {
        private readonly Obstacle _obstacle = Obstacle.Of();

        private void OnTriggerEnter(Collider other)
        {
            var container = other.gameObject.GetComponent<IContainBird>();
            if (container?.Bird != null && container.Bird.IsAlive)
                KillBird(container);
        }

        public void KillBird(IContainBird bird) => _obstacle.Kill(bird.Bird);
    }
}