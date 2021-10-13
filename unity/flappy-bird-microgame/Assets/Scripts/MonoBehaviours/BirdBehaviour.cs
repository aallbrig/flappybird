using System;
using System.Collections;
using Core;
using UnityEngine;

namespace MonoBehaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class BirdBehaviour : MonoBehaviour
    {
        public float deathFreezeTime = 0.6f;
        public int flapStrength = 3;
        public float flapsPerSecond = 2.0f;
        private bool _canFlap = true;

        private Rigidbody _rigidbody;

        public bool IsAlive { get; private set; } = true;

        public Bird Bird { get; } = Bird.Factory();

        private void Start() => _rigidbody = GetComponent<Rigidbody>();
        private void OnEnable()
        {
            _canFlap = true;
            Bird.FlappedWings += OnFlappedWings;
            Bird.Died += OnDied;
        }
        private void OnDisable()
        {
            Bird.FlappedWings -= OnFlappedWings;
            Bird.Died -= OnDied;
        }

        private void Update()
        {
            MobileDeviceHack();
        }
        private void MobileDeviceHack()
        {
            if (Input.touchCount > 0)
            {
                Fly();
            }
        }

        private void OnFlappedWings(object sender, EventArgs eventArgs)
        {
            _canFlap = false;
            _rigidbody.AddForce(Vector3.up * flapStrength, ForceMode.Impulse);
            StartCoroutine(ResetFlap());
            // SFX: Play flap audio
        }

        private void OnDied(object sender, EventArgs eventArgs)
        {
            IsAlive = false;
            _canFlap = false;
            // SFX: Play died audio
            StartCoroutine(DestroySelfAfterSeconds(deathFreezeTime));
        }

        private IEnumerator DestroySelfAfterSeconds(float freezeTime)
        {
            yield return new WaitForSeconds(freezeTime);
            Destroy(gameObject);
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