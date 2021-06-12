using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialog_new : MonoBehaviour
{
    public TextMeshProUGUI textdisplay;
    [TextArea(3, 10)]
    public string[] sentences;
    private int index;
    public float typingInterval;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

        animator.SetBool("IsOpen", true);
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textdisplay.text += letter;
            yield return new WaitForSeconds(typingInterval);
        }
    }

    public void nextsentence()
    {
        Debug.Log(index);
        Debug.Log(sentences.Length);
        if (index < sentences.Length - 1)
        {
            index++;
            textdisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            EndDialogue();
        }
    }
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
