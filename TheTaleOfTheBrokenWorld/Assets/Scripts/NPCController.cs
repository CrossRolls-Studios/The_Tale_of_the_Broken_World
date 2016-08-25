using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPCController : MonoBehaviour {

    public string nameNPC;
    public string profession;
    public Text nameText;
    public Text professionText;

    public string[] phrasesMeet;
    public string phraseExit; 

    public string[] playerPhrases;

    public string NPCType;

    public Text firstPhrase;
    public Text exit;

    public GameObject greenEdges;
    public Transform quest;
    public NPCQuestController questController;
	// Use this for initialization
	void Start () {
        greenEdges.SetActive(false);
        nameText.text = nameNPC;
        professionText.text = profession;
        quest = transform.FindChild("Quests");
        if (quest != null)
        {
            questController = quest.GetComponent<NPCQuestController>();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MeetNPC()
    {

        int phraseNumber = Random.Range(0, phrasesMeet.Length);
        firstPhrase = GameObject.Find("FirstPhrase").GetComponent<Text>();
        firstPhrase.transform.position = GameObject.Find("NoDialogPlace").transform.position;
        if (quest != null)
        {
            if (questController.readyToGiveQuest == true)
            {
                firstPhrase.transform.position = GameObject.Find("DialogPlace").transform.position;
                questController.WritingTheQuest();
            }
            else if (questController.questGiven == true)
            {
                firstPhrase.transform.position = GameObject.Find("DialogPlace").transform.position;
                questController.WritingTheQuest();
            }
        }
        firstPhrase.text = nameNPC + ": " + phrasesMeet[phraseNumber];
        exit = GameObject.Find("Exit").GetComponent<Text>();
        exit.text = phraseExit;
    }

    

}
