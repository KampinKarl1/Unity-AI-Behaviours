using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;

using UnityEngine.AI; //For use of Navmesh agent

public class FunctionalAdult : MonoBehaviour, IHear
{
    [SerializeField] private NavMeshAgent agent = null;

    [SerializeField, Tooltip("How far away, in meters, the agent will run from danger.")] 
    private float displacementFromDanger = 10f;

    void Awake() 
    {
        if (agent == null && !TryGetComponent(out agent))        
            Debug.LogWarning(name + " doesn't have an agent!");        
    }

    public void RespondToSound(Sound sound)
    {
        /* 
        *   Put fun things here
        *   Examples:
        *   Animate the NPC, Play a sound clip ("What was that?!"), Throw some UI up, Check if the sound is more important than current task
        */
    
        if (sound.soundType == Sound.SoundType.Interesting)
            MoveTo(sound.pos);
        else if (sound.soundType == Sound.SoundType.Dangerous) //Must have this case so that it doesn't run away from the default sound type
        {
            Vector3 dir = (sound.pos - transform.position).normalized;
            MoveTo(transform.position - (dir * displacementFromDanger));
        }
        //else will do nothing in the case of Sounds with Default sound type
    }

    private void MoveTo(Vector3 pos) 
    {
        agent.SetDestination(pos);
        agent.isStopped = false;
    }
}
