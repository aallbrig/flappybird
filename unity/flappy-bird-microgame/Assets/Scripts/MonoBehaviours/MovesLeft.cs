using System;
using UnityEngine;

namespace MonoBehaviours
{
    public class MovesLeft : MonoBehaviour
    {
        public float speed = 10f;

        private void Update()
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}