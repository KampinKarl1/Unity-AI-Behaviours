using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour, IInstincts
{
    [SerializeField] private NavAgentMover mover = null;
    [SerializeField] private Transform target = null;

    [SerializeField] private float searchRadius = 8f;
    [SerializeField] private float killDistance = 3f;
    [SerializeField] private float lookThreshold = .8f; //How much the fox has to rotate to target for kill 

    private void Start()
    {
        mover.MoveToPos(transform.position + Vec3_Utils.WithinRadiusOfOrigin(searchRadius));
    }

    void Update()
    {
        //chase target
        if (target)
        {
            mover.MoveTo(target);

            if (Vector3.Dot(transform.forward.normalized, target.position.normalized) <= lookThreshold 
                && Vector3.Distance(transform.position, target.position) <= killDistance)

                KillTarget();
            
            return;
        }
        //search for targets
         if (mover.HasArrived)
            mover.MoveToPos(transform.position + Vec3_Utils.WithinRadiusOfOrigin(searchRadius));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == transform || !other.CompareTag("Animal"))
            return;

        //If an equal or better predator
        if (other.TryGetComponent(out IInstincts instincts) && instincts.PredatorLevel() >= predatorLevel)
            return;

        //If dont have target or if the new poss. target is slower than current
        if (target == null || SlowerThanCurTarget(other.transform) || CloserThanCurTarget(other.transform))
            target = other.transform;
    }

    private void KillTarget() 
    {
        Destroy(target.gameObject);
        target = null;

        CheckForNewTarget();
    }

    private void CheckForNewTarget() 
    {
        var cols = Physics.OverlapSphere(transform.position, 10f);

        Transform closest = null;
        float close = float.MaxValue;

        for (int i = 0; i < cols.Length; i++)
        {
            if (!cols[i].CompareTag("Animal") || cols[i].transform == transform ||
                    (cols[i].TryGetComponent(out IInstincts instincts) && instincts.PredatorLevel() >= predatorLevel))
                continue;

            float dist = Vector3.Distance(transform.position, cols[i].transform.position);
            if (dist < close) 
            {
                close = dist;
                closest = cols[i].transform;
            }
        }

        if (closest)
            target = closest;
    }

    private bool CloserThanCurTarget(Transform other)
    {
        return Vector3.Distance(other.position, transform.position) < Vector3.Distance(target.position, transform.position);
    }

    private bool SlowerThanCurTarget(Transform other) 
    {
        if (other.TryGetComponent(out ISpeed spd1) && target.TryGetComponent(out ISpeed spd2))
            return spd1.GetMaxSpeed() < spd2.GetMaxSpeed();
        return false;
    }

    public void BecomePreyOf(Transform predator)
    {
       
    }

    [SerializeField] private int predatorLevel = 5;
    public int PredatorLevel()
    {
        return predatorLevel;
    }
}
