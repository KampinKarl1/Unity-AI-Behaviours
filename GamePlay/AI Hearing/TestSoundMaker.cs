using UnityEngine;

namespace GamePlay
{
    public class TestSoundMaker : MonoBehaviour
    {
        [SerializeField] private AudioSource source = null;

        [SerializeField] private float soundRange = 25f;
        
        [SerializeField] private Sound.SoundType soundType = Sound.SoundType.Danger;

        private void OnMouseDown()
        {
            if (source.isPlaying) //If already playing a sound, don't allow overlapping sounds 
                return;

            source.Play();

            var sound = new Sound(transform.position, soundRange, soundType);

            Sounds.MakeSound(sound);
        }
    }
}
