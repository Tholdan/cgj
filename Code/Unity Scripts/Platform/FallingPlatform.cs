using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    //PUBLIC
    public float timeBeforeFalling;

    //PRIVATE
    private bool activateFallingTime;
    private float currentTime;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        currentTime = timeBeforeFalling;
        rb = GetComponent<Rigidbody>();
        if(rb == null)
        {
            Debug.LogError("Falling Platforms must have a rigidbody attached.");
        }
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
	}
	
	// Update is called once per frame
	void Update () {
        if (activateFallingTime)
        {
            currentTime -= Time.deltaTime;

            if(currentTime <= 0)
            {
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
                activateFallingTime = false;
            }
        }
	}

    void OnCollisionEnter(Collision other)
    {
        activateFallingTime = true;
    }
}
