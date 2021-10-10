using System;
using System.Collections;
using UnityEngine;
using Core;

namespace MonoBehaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class BirdBehaviour : MonoBehaviour
    {
        public int flapStrength = 3;
        public float flapsPerSecond = 2.0f;
        public Bird Bird { get; private set; }
        private Rigidbody _rigidbody;
        private bool _canFlap = true;

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
            _canFlap = false;
            _rigidbody.AddForce(Vector3.up * flapStrength, ForceMode.Impulse);
            StartCoroutine(ResetFlap());
        }

        [ContextMenu("Fly")]
        public void Fly()
        {
            if (_canFlap)
            {
                Bird.FlapWings();
            }
        }

        private IEnumerator ResetFlap()
        {
            yield return new WaitForSeconds(1 / flapsPerSecond);
            _canFlap = true;
        }
    }
}