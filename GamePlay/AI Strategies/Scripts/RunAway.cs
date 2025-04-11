using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : MonoBehaviour, IInstincts
{
    [SerializeField] private NavAgentMover mover = null;

    private Transform predator = null;

    [SerializeField] private float displacementDist = 5f;

    #region Indicate being chased
    private Color origColor;
    private void SetOriginalColor()
    {
        var rend = GetComponentInChildren<Renderer>();
        origColor = rend.material.color;
    }

    private void ChangeColor(Color color)
    {
        var rend = GetComponentInChildren<Renderer>();
        rend.material.color = color;
    }
    #endregion

    #region IInsticts impl.
    public void BecomePreyOf(Transform predator)
    {
        print(predator + " started chasing " + name);
        this.predator = predator;

        ChangeColor(Color.yellow);
    }

    private void StopBeingChased() 
    {
        predator = null;

        ChangeColor(origColor);
    }

    public int PredatorLevel() => 0;
    #endregion

    void Start() 
    {
        SetOriginalColor();
    }

    private void Update()
    {
        if (predator)
            EvadePredator();
        else
            Idle();
    }

    #region Become Prey and Stop being prey logic

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Animal") ||
            (other.TryGetComponent(out IInstincts instincts) && instincts.PredatorLevel() <= PredatorLevel())) //if is a lesser predator than me
            return;

        //If it looks like it's coming toward me
        if (Vector3.Dot(other.transform.forward.normalized, transform.position.normalized) > .5f)
            BecomePreyOf(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        //if the exiting object is the animal that was chasing me
        if (other.CompareTag("Animal") && (predator != null && other.transform == predator))
            CoroutineRunner.DelayedAction(() => StopBeingChased(), .5f);
    }
    #endregion

    #region Evasion
    private void EvadePredator() 
    {
        Vector3 normDir = (predator.position - transform.position).normalized;

        normDir = Quaternion.AngleAxis(Random.Range(0, 179), Vector3.up) * normDir;

        mover.MoveToPos(transform.position - (normDir * displacementDist));
    }
    #endregion

    #region Idle 
    private float nextCluckTime = 0f;
    private float nextMoveTime = 0f;
    const float MIN_BTWN_CLUCKS = 2.0f;
    const float MAX_BTWN_CLUCKS = 12.0f;
    const float MIN_BTWN_MOVES = 2.0f;
    private void Idle() 
    {
        if (mover.HasArrived && PassedTime(nextMoveTime))
        {
            nextMoveTime = SetFutureRandomTime(MIN_BTWN_MOVES, 5f);
            mover.MoveToPos(Vec3_Utils.RandPosNearTransform(transform, 12f));
        }

        if (PassedTime(nextCluckTime)) 
        {
            nextCluckTime = SetFutureRandomTime(MIN_BTWN_CLUCKS, MAX_BTWN_CLUCKS);
            print( 
                ProbablityReached(.05f) ? 
                name + ": Cluck" : 
                name + ": Just clucking around");
        }
    }
    //Making stuff easier to read and reproduce
    private static bool ProbablityReached(float lowestForSuccess) => Random.Range(0f, 1f) >= lowestForSuccess;
    private static bool PassedTime(float time) => Time.time >= time;
    private static float SetFutureRandomTime(float min, float max) => Time.time + Random.Range(min, max);
    
    #endregion
}
