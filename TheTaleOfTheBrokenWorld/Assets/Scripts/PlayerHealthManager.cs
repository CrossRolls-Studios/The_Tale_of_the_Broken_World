using UnityEngine;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {

    public float playerMaxHealth;
    public float playerCurrentHealth;

	// Use this for initialization
	void Start () {
        SetMaxHealth();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            
        }
	}

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }

    public void HealPlayer(float HPToHeal)
    {
        playerCurrentHealth += HPToHeal;
        if (playerCurrentHealth > playerMaxHealth)
        {
            SetMaxHealth();
        }
    }

    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
}
