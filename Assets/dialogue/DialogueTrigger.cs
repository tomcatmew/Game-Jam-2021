using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

	public Dialogue dialogue;
    public DialogueManager diamanager;
    public GameObject prefabdialog;
    public void TriggerDialogue()
	{
        diamanager.StartDialogue(dialogue);
	}

    private void Start()
    {
        prefabdialog.SetActive(true);
    }
}