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


        /*
        *
        *       The following methods should be called by adding events to the onCollideWith event.
        *       (Plug this component into the event's field and in the drop down, select Throwable/Make_______Sound(float range))
        *
        */
        public void MakeAnInterestingSound(float range)
        {
            var sound = new Sound(transform.position, range, Sound.SoundType.Interesting); 
            
            Sounds.MakeSound(sound);
        }

        public void MakeADangerousSound(float range)
        {
            var sound = new Sound(transform.position, range, Sound.SoundType.Dangerous);
            
            Sounds.MakeSound(sound);
        }
    }
}
