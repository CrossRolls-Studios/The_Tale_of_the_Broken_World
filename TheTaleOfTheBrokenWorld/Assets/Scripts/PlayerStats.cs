using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public int currentLevel;
    public int currentExp;
    public int []toLevelUp;

    public int power;
    public int agility;
    public int intelligence;
    public int stamina;
    public int luck;
    public int block;



    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        if (currentExp >= toLevelUp[currentLevel])
        {
            currentLevel++;
        }
	}

    public void AddExperience(int experienceToAdd)
    {
        currentExp += experienceToAdd;
    }
}
