using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour {

    //PUBLIC
    //Moving speed.
    public float speed;
    //Path that the platform will do.
    public Vector3[] steps;

    //PRIVATE
    private int currentStep;
    private Vector3 currentDirection;
    private Rigidbody fatherRb;
    

	// Use this for initialization
	void Start () {
		if(steps.Length == 0)
        {
            Debug.LogError("Movable Object with an empty list!");
        }

        currentStep = 0;
        transform.position = steps[currentStep];
        currentDirection = getVectorBetweenSteps(currentStep, NextStep());
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //Movement function
        transform.position += currentDirection * speed * Time.fixedDeltaTime;

        //Checks if the platform has arrived to the destination.
        if (Vector3.Distance(transform.position, steps[currentStep]) >= Vector3.Distance(steps[NextStep()], steps[currentStep]))
        {
            currentStep = NextStep();
            currentDirection = getVectorBetweenSteps(currentStep, NextStep());
        }
	}

    /// <summary>
    /// Calculates and returns the next step.
    /// </summary>
    /// <returns>int specifying the next step.</returns>
    int NextStep()
    {
        return (currentStep + 1) % steps.Length;
    }

    /// <summary>
    /// Simple vector function between two 3d space points.
    /// </summary>
    /// <param name="initStep"></param>
    /// <param name="finalStep"></param>
    /// <returns>Vector3 between two points.</returns>
    Vector3 getVectorBetweenSteps(int initStep, int finalStep)
    {
        return (steps[finalStep] - steps[initStep]).normalized;
    }


    //These two functions just make the player stay on the platform.
    void OnCollisionEnter(Collision other)
    {
        if (other.rigidbody != null)
        {
            //The other object movement now is linked to the platform.
            other.transform.parent = transform;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.rigidbody != null)
        {
            //Adding velocity to simulate the fact of jumping with a certain horizontal velocity.
            other.rigidbody.velocity = new Vector3(
                other.rigidbody.velocity.x + currentDirection.x * speed,
                other.rigidbody.velocity.y,
                other.rigidbody.velocity.z + currentDirection.z * speed);
            //The other object parent is set to null to be free movement.
            other.transform.parent = null;
        } 
    }
}
