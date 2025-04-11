using UnityEngine;
using UnityEngine.AI;
public static class Vec3_Utils
{
    public static Vector3 RandomizeAll(float min, float max)
    {
        float x, y, z;
        x = Random.Range(min, max);
        y = Random.Range(min, max);
        z = Random.Range(min, max);
        return new Vector3(x, y, z);
    }

    private static bool PointIsNavigable(Vector3 pos, out float y)
    {
        bool navigable = NavMesh.SamplePosition(pos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas);
        y = hit.position.y;
        return navigable;
    }

    /// <summary>
    /// Create a randomized Vector3 using floats. 
    /// </summary>
    /// <returns>New Vector3 with randomized x, y, and z coordinates.</returns>
    public static Vector3 RandomizedVector3(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax) 
    {
        return new Vector3(
            Random.Range(xMin, xMax),
            Random.Range(yMin, yMax),
            Random.Range(zMin, zMax));
    }

    /// <summary>
    /// Create a randomized Vector3 using floats. 
    /// </summary>
    /// <returns>New Vector3 with randomized x, y, and z coordinates.</returns>
    public static Vector3 RandomizedVector3(ParticleSystem.MinMaxCurve x, ParticleSystem.MinMaxCurve y, ParticleSystem.MinMaxCurve z)
    {
        return RandomizedVector3(x.constantMin, x.constantMax, y.constantMin, y.constantMax, z.constantMin, z.constantMax);
    }

    /// <summary>
    /// Enter a positive float or 0 in each coordinate. Min is the negative of the coordinate entered.
    /// </summary>
    /// <param name="limits">An x of 10.0f could be anything from -10.0f to 10.0f.</param>
    /// <returns></returns>
    public static Vector3 RandomizedVector3(Vector3 limits) 
    {
        return new Vector3(
            Random.Range(-limits.x, limits.x),
            Random.Range(-limits.y, limits.y),
            Random.Range(-limits.z, limits.z));
    }

    public static Quaternion RandomizedRotation(Vector3 randomizedEuler) 
    {
        return Quaternion.Euler(RandomizedVector3(randomizedEuler));
    }

    /// <summary>
    /// Will return a random position within radius of passed position. Will place the object on the ground or at the passed position's Y.
    /// </summary>
    /// <param name="maxDist"></param>
    /// <param name="pos"></param>
    /// <param name="ensureNavigable">Optionally can check that the position is navigable on the NavMesh.</param>
    /// <returns></returns>
    public static Vector3 RandomPositionNearPosition(float maxDist, Vector3 pos, bool ensureNavigable = false)
    {
        float prevY = pos.y;
        pos += (Random.insideUnitSphere * maxDist);

        //Place the object on the ground.
        if (ensureNavigable)
            PointIsNavigable(pos, out pos.y);
        else
            pos.y = prevY;

        return pos;
    }
    public static Vector3 WithinRadiusOfOrigin(float radius)
    {
        Vector3 pos = Random.insideUnitSphere * radius;
        pos.y = 0;

        return pos;
    }

    public static Vector3 RandPosNearTransform(Transform me, float maxDist)
    {
        float dist = maxDist / 2f;

        return new Vector3(me.position.x + Random.Range(-dist, dist),
                            me.position.y,
                            me.position.z + Random.Range(-dist, dist));
    }
}
