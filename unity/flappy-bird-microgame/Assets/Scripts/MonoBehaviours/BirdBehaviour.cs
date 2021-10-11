using System;
using System.Collections;
using Core;
using UnityEngine;

namespace MonoBehaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class BirdBehaviour : MonoBehaviour
    {
        public int flapStrength = 3;
        public float flapsPerSecond = 2.0f;
        private bool _canFlap = true;

        private Rigidbody _rigidbody;

        public Bird Bird { get; } = Bird.Factory();

        public void Start()
        {
            SetupListeners();
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void SetupListeners() => Bird.FlappedWings += OnFlappedWings;

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
                Bird.FlapWings();
        }

        private IEnumerator ResetFlap()
        {
            yield return new WaitForSeconds(1 / flapsPerSecond);
            _canFlap = true;
        }
    }
}