using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;



public class DialogueSystem : MonoBehaviour
{

    GameObject  dialogueBox, dialoguePanel;

    bool dialogueIsActive = false;

    string nameOfCharacter; 
    Dialogue[] dialogues; 
    int nbDialogues; 
    int currentDialogueIndex = 0;
    bool waitingForUserInput = false;



    // Start is called before the first frame update
    void Start()
    {

        nameOfCharacter = gameObject.name; 
        nbDialogues = calculateNbDialogues(); 
        dialogues = new Dialogue[nbDialogues];

        loadDialogue();
        nbDialogues = calculateNbDialogues();
        
        //for (int i = 0; i < nbDialogues; i++)
        //{ 
        // print("Message: " + dialogues[i].message);
        // print("- Answer A: " + dialogues[i].response[0]);  
        // print("- Answer B: " + dialogues[i].response[1]);
        // }
       


        //startDialogue();


        //DEBUG
        
    }

    public void loadDialogue() 
    { 
        TextAsset textAsset = (TextAsset)Resources.Load("dialogues"); 
        XmlDocument doc = new XmlDocument(); 
        doc.LoadXml(textAsset.text); 
        int dialogueIndex = 0;

        foreach (XmlNode character in doc.SelectNodes("dialogues/character")) 
        { 
            if (character.Attributes.GetNamedItem("name").Value == nameOfCharacter) 
            { 
                dialogueIndex = 0;

                foreach (XmlNode dialogueFromXML in doc.SelectNodes("dialogues/character/dialogue"))
                {
                    dialogues[dialogueIndex] = new Dialogue();
                    dialogues[dialogueIndex].message = dialogueFromXML.Attributes.GetNamedItem("content").Value;
                    int choiceIndex = 0;
                    dialogues[dialogueIndex].response = new string[2];
                    dialogues[dialogueIndex].targetForResponse = new int[2];
                    foreach (XmlNode choice in dialogueFromXML)
                    {
                        dialogues[dialogueIndex].response[choiceIndex] = choice.Attributes.GetNamedItem("content").Value;

                        dialogues[dialogueIndex].targetForResponse[choiceIndex] = int.Parse(choice.Attributes.GetNamedItem("target").Value);
                        choiceIndex++;
                    }
                    dialogueIndex++;
                }

            }
        }

    }

    public int calculateNbDialogues()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("dialogues");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);
        int dialogueIndex = 0;
        foreach (XmlNode character in doc.SelectNodes("dialogues/character"))
        {
            if (character.Attributes.GetNamedItem("name").Value == nameOfCharacter)
            {
                foreach (XmlNode dialogueFromXML in doc.SelectNodes("dialogues/character/dialogue"))
                {
                    dialogueIndex++;
                }
            }
        }
        return dialogueIndex;
    }

    public void startDialogue()
    {
        waitingForUserInput = false;
        dialogueIsActive = true;

    }
    public void displayDialogue1()
    {
        print(dialogues[currentDialogueIndex].message);
        print("[A]> " + dialogues[currentDialogueIndex].response[0]);
        print("[B]> " + dialogues[currentDialogueIndex].response[1]);
    }

    public void displayDialogue2()
    {
        string textToDisplay = "[" + gameObject.name + "] " + " " 
            + dialogues[currentDialogueIndex].message + "\n[A]> " + 
            dialogues[currentDialogueIndex].response[0] + "\n[B]> " + 
            dialogues[currentDialogueIndex].response[1]; ;
        GameObject.Find("dialogueBox").GetComponent<Text>().text = textToDisplay;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (dialogueIsActive)
        {
            if (!waitingForUserInput)
            {
                dialoguePanel.SetActive(true);

                if (currentDialogueIndex != -1) displayDialogue2();
                else
                {
                    dialogueIsActive = false;
                    dialoguePanel.SetActive(false);
                    waitingForUserInput = false;
                    GameObject.Find("Player").GetComponent<ControlPlayer>().EndTalking();

                }
                waitingForUserInput = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    currentDialogueIndex = dialogues[currentDialogueIndex].targetForResponse[0];
                    waitingForUserInput = false;
                }
                else if (Input.GetKeyDown(KeyCode.B))
                {
                    currentDialogueIndex = dialogues[currentDialogueIndex].targetForResponse[1];
                    waitingForUserInput = false;
                }
            }
        }
       

    }

    
}
