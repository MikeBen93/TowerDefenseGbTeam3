using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMenu : MonoBehaviour
{
    [SerializeField] private DialogueElements[] dialogueElements;

    public bool showDialogueMenu = false;

    [SerializeField] private GameObject completeLevelUI;
    [SerializeField] private GameObject dialogueMenuUI;
    [SerializeField] private GameObject witchImage;
    [SerializeField] private GameObject engineerImage;
    [SerializeField] private GameController gameController;

    private Sprite witchSprite;
    private Sprite engineerSprite;
    [SerializeField] private Text characterText;

    private int currentDialogueElement;

    private void OnEnable()
    {
        witchSprite = witchImage.GetComponent<Image>().sprite;
        engineerSprite = engineerImage.GetComponent<Image>().sprite;
        currentDialogueElement = 0;

        SetDialogueElement(currentDialogueElement);
    }

    public void ActivateDialogue()
    {
        dialogueMenuUI.SetActive(true);
    }

    public void Next()
    {
        currentDialogueElement++;

        if(currentDialogueElement == dialogueElements.Length)
        {
            dialogueMenuUI.SetActive(false);
            gameController.ShowWinWindow();
            return;
        } 

        SetDialogueElement(currentDialogueElement);
    }

    private void SetDialogueElement(int dialogueNumber)
    {
        if(dialogueElements[dialogueNumber].characterImage == witchSprite)
        {
            witchImage.SetActive(true);
            engineerImage.SetActive(false);
        } else
        {
            witchImage.SetActive(false);
            engineerImage.SetActive(true);
        }
        characterText.text = dialogueElements[dialogueNumber].characterSpeech;
    }


}
