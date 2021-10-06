using Core;
using UnityEngine;


namespace MonoBehaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class BirdBehaviour : MonoBehaviour, IFly
    {
        public Bird Bird { get; set; }
        private Rigidbody _rigidBody;
        [ContextMenu("Fly")]
        public void Fly()
        {
            _rigidBody.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }

        private void Start()
        {
            Bird = Bird.Factory(this);
            _rigidBody = GetComponent<Rigidbody>();
        }
    }
}