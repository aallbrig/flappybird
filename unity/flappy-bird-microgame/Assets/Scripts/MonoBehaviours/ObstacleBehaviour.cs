using Core;
using UnityEngine;

namespace MonoBehaviours
{
    public class ObstacleBehaviour : MonoBehaviour
    {
        private readonly Core.Obstacle _obstacle = new Core.Obstacle();
        private void OnTriggerEnter(Collider other)
        {
            var bird = other.gameObject.GetComponent<BirdBehaviour>();
            if (bird != null && bird.Bird != null && bird.IsAlive)
                KillBird(bird);
        }

        public void KillBird(BirdBehaviour bird) => _obstacle.Kill(bird.Bird);
    }
}