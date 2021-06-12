using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

	public Dialogue dialogue;
    public DialogueManager diamanager;

    public void TriggerDialogue()
	{
        diamanager.StartDialogue(dialogue);
	}

    private void Start()
    {
		TriggerDialogue();
    }
}