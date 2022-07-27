using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private GameObject thingToThrow = null;
    [SerializeField] private float throwForce = 100f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            ThrowThing();
    }

    private void ThrowThing() 
    {
        if (thingToThrow == null)
            return;

        GameObject o = Instantiate(thingToThrow, transform.position, transform.rotation);

        if (o.TryGetComponent(out Rigidbody rb))
            rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);

        Destroy(o, 5f);
    }
}
