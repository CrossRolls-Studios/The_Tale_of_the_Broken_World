using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPCQuestController : MonoBehaviour {

    public bool readyToGiveQuest;
    public string questToBeReady;
    public GameObject[] questsToGive;
    public int currentQuestNumber;
    public int currentDialogPhrase;
    public bool questGiven;

    public Text dialogPhrase;
    public GameObject dialogButton;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (questToBeReady != "")
        {
            if (GameObject.Find(questToBeReady) != null)
            {
                readyToGiveQuest = true;
            }
        }

	}

    public void WritingTheQuest()
    {
        dialogButton.SetActive(true);
        Text dialog = dialogButton.GetComponent<Text>();
        dialog.text = questsToGive[currentQuestNumber].GetComponent<Quest>().questName;
    }

    public void Dialog(int numberOfPhrase)
    {
        dialogPhrase = GameObject.Find("FirstPhrase").GetComponent<Text>();
        dialogPhrase.transform.position = GameObject.Find("NoDialogPlace").transform.position;
        dialogPhrase.text =  transform.GetComponentInParent<NPCController>().nameNPC + ": " + questsToGive[currentQuestNumber].GetComponent<Quest>().dialogBeforeQuest[numberOfPhrase];
    }

    public void TextAfterGivingQuest()
    {
        dialogPhrase.text = transform.GetComponentInParent<NPCController>().nameNPC + ": " + questsToGive[currentQuestNumber].GetComponent<Quest>().textAfterGiving;
    }

    public void GivingQuest()
    {
        readyToGiveQuest = false;
        questGiven = true;
    }

    public void QuestNotCompleted()
    {
        dialogPhrase = GameObject.Find("FirstPhrase").GetComponent<Text>();
        dialogPhrase.transform.position = GameObject.Find("NoDialogPlace").transform.position;
        dialogPhrase.text = transform.GetComponentInParent<NPCController>().nameNPC + ": " + questsToGive[currentQuestNumber].GetComponent<Quest>().dialogAfterQuest[0];
    }
}
