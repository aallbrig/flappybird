using Core;
using UnityEngine;

namespace MonoBehaviours
{
    public class BirdKiller : MonoBehaviour
    {
        private readonly Obstacle _obstacle = new Obstacle();
        private void OnTriggerEnter(Collider other)
        {
            var bird = other.gameObject.GetComponent<BirdBehaviour>();
            if (bird != null && bird.Bird != null && bird.IsAlive)
                KillBird(bird);
        }

        public void KillBird(BirdBehaviour bird) => _obstacle.Kill(bird.Bird);
    }
}