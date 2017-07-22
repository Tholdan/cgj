using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithEnviroment : MonoBehaviour {

    //PUBLIC
    public float interactionDistance;

    //PRIVATE
    private RaycastHit rayHitInfo;
    private GameObject interactiveObject;
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(transform.position, transform.rotation * Vector3.forward);
        Debug.DrawRay(transform.position, transform.rotation * Vector3.forward);
        if (Physics.Raycast(ray, out rayHitInfo, interactionDistance) && rayHitInfo.transform.CompareTag("Npc"))
        {
            Debug.Log(rayHitInfo.transform.name);
            interactiveObject = rayHitInfo.transform.gameObject.GetComponent<InteractiveGameObject>().interactiveVisualAlert;
            interactiveObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                rayHitInfo.transform.gameObject.GetComponent<InteractiveGameObject>().ActivateDialogue(gameObject);
            }
        }
        else
        {
            if(interactiveObject != null)
                interactiveObject.SetActive(false);
        }
	}
}
