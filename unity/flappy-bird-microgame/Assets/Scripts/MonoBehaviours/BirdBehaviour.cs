using System;
using UnityEngine;
using core;

namespace MonoBehaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class BirdBehaviour : MonoBehaviour
    {
        public int flapStrength = 5;
        public Bird Bird { get; private set; }
        private Rigidbody _rigidbody;

        public void NewBird()
        {
            Bird = Bird.Factory();
        }

        public void Start()
        {
            NewBird();
            SetupListeners();
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void SetupListeners()
        {
            Bird.FlappedWings += OnFlappedWings;
        }

        private void OnFlappedWings(object sender, EventArgs eventArgs)
        {
            _rigidbody.AddForce(Vector3.up * flapStrength, ForceMode.Impulse);
        }

        [ContextMenu("Fly")]
        public void Fly() => Bird.FlapWings();
    }
}