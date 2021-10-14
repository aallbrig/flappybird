using System;
using System.Collections;
using Core;
using UnityEngine;

namespace MonoBehaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class BirdBehaviour : MonoBehaviour, IContainBird
    {
        public float deathFreezeTime = 0.6f;
        public int flapStrength = 3;
        public float flapsPerSecond = 2.0f;

        private bool _canFlap = true;
        private Rigidbody _rigidbody;

        private void Start() => _rigidbody = GetComponent<Rigidbody>();

        private void Update() => MobileWebGLHack();
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

        public Bird Bird { get; } = Bird.Of();

        // Grr, why doesn't the new unity input system work on mobile webGL?!
        private void MobileWebGLHack()
        {
            if (Input.touchCount > 0)
                Fly();
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