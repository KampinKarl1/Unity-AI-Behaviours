using UnityEngine;

namespace GamePlay
{
    public static class Sounds 
    {
        /// <summary>
        /// 
        /// </summary>
        public static void MakeSound(Sound sound) 
        {
            Collider[] col = Physics.OverlapSphere(sound.pos, sound.range);

            for (int i = 0; i < col.Length; i++)
                if (col[i].TryGetComponent(out IHear hearer))
                    hearer.RespondToSound(sound);
        }
    }
}
