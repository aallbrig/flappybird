using UnityEngine;

namespace MonoBehaviours
{
    public class ObstacleBehaviour : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var bird = other.gameObject.GetComponent<BirdBehaviour>();
            bird?.Bird?.Kill();
        }
    }
}