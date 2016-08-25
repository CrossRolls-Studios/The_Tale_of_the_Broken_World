using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {

    public int enemyMaxHealth;
    public int enemyCurrentHealth;

    private PlayerStats thePlayerStats;
    public int experienceToAdd;
    // Use this for initialization
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;

        thePlayerStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            Destroy(gameObject);
            thePlayerStats.AddExperience(experienceToAdd);
        }
    }

    public void HurtEnemy(int damageToGive)
    {
        enemyCurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }
}
