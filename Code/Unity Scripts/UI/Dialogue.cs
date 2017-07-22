using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    //PUBLIC
    public Text currentText;
    public GameObject nextDialogueButton;
    public float minSecondsBetweenCharacters = 0.2f;
    public float maxSecondsBetweenCharacters;
    public string[] dialogueParts;

    //PRIVATE
    private int currentPart;
    private bool partFinished;
    private bool dialogueFinished;
    private float currentSecondsBetweenCharacters;

    void Awake()
    {
        dialogueFinished = false;
    }

	// Use this for initialization
	void Start ()
    {
        currentText.text = "";
        currentSecondsBetweenCharacters = minSecondsBetweenCharacters;
        currentPart = 0;
        StartCoroutine(DisplayDialogue(dialogueParts[currentPart]));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentSecondsBetweenCharacters = maxSecondsBetweenCharacters;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            currentSecondsBetweenCharacters = minSecondsBetweenCharacters;
        }

        if (partFinished && Input.GetKeyDown(KeyCode.E))
        {
            nextDialogueButton.SetActive(false);
            currentText.text = "";
            StartCoroutine(DisplayDialogue(dialogueParts[currentPart]));
            partFinished = false;
        }

        if (dialogueFinished && Input.GetKeyDown(KeyCode.E))
        {
            nextDialogueButton.SetActive(false);
            gameObject.SetActive(false);
            currentPart = 0;
        }
    }

    private IEnumerator DisplayDialogue(string currentDialogue)
    {
        foreach(char letter in currentDialogue)
        {
            currentText.text += letter;
            yield return new WaitForSeconds(currentSecondsBetweenCharacters);
        }

        nextDialogueButton.SetActive(true);
        currentPart++;
        if(currentPart >= dialogueParts.Length)
        {
            partFinished = false;
            dialogueFinished = true;
        }
        else
        {
            partFinished = true;
            dialogueFinished = false;
        }
    }

    public bool DialogueFinished()
    {
        return this.dialogueFinished;
    }
}
