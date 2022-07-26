using UnityEngine;

namespace GamePlay
{
    public class Sound
    {

        public Sound(Vector3 _pos, float _range)
        {
            pos = _pos;

            range = _range;
        }

        public readonly Vector3 pos;

        /// <summary>
        /// This the intensity of the sound.
        /// </summary>
        public readonly float range;
    }
}
