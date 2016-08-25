using UnityEngine;
using System.Collections;
using System;


//add the prize for the quests


public class QuestManager : MonoBehaviour {
    public int numberOfQuests;
    public GameObject[] quests;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void GetNewQuest(GameObject newQuest)
    {
        newQuest.transform.SetParent(transform);
        numberOfQuests++;
        Array.Resize(ref quests, numberOfQuests);
        quests[numberOfQuests - 1] = newQuest;
        
    }
}
