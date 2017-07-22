using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObject : MonoBehaviour
{

    //PUBLIC
    public GameObject interactiveVisualAlert;
    public GameObject dialogueCanvas;
    public GameObject player;

    void Update()
    {
        if (dialogueCanvas.GetComponent<Dialogue>().DialogueFinished())
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    public void ActivateDialogue(GameObject player)
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        dialogueCanvas.SetActive(true);
    }
}
