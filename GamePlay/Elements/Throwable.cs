using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GamePlay
{
    public class Throwable : MonoBehaviour
    {
        [SerializeField] private UnityEvent onCollideWith = new UnityEvent();
        [SerializeField] private LayerMask collisionLayerMask = ~0;
        [SerializeField] private bool destroyOnCollide = false;

        private void OnCollisionEnter(Collision collision)
        {
            if (collisionLayerMask == (collisionLayerMask | (1 << collision.gameObject.layer)))
            {
                onCollideWith?.Invoke();

                if (destroyOnCollide)
                    Destroy(this); //destroys only the script
            }
        }


        public void MakeASound(float range)
        {
            var sound = new Sound(transform.position, range, Sound.SoundType.Interesting);
            
            Sounds.MakeSound(sound);
        }
    }
}
