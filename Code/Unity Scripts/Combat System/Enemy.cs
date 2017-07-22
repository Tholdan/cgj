using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public enum Complexity { simple, complex };

    public GameObject player;
    public CombatData combatData;
    public Complexity complexity;
    [Header("Simple:")]
    public float rotationSpeed;
    [Header("Complex:")]
    public Transform focus;
    public NavMeshAgent agent;



	// Use this for initialization
	void Start () {
        if (complexity == Complexity.complex)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        AISystem();
	}

    void AISystem()
    {
        if (complexity == Complexity.simple)
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) < 10)
            {
                Vector3 direction = player.transform.position - this.transform.position;
                direction.y = 0;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

                if (direction.magnitude < 5)
                {
                    this.transform.Translate(0.0f, 0.0f, 4f * Time.deltaTime);
                }
            }
        }
        else
        {
            if (agent.enabled)
                agent.destination = focus.position;

            if (Vector3.Distance(player.transform.position, this.transform.position) < 2)
                agent.enabled = false;
        }
    }
}
