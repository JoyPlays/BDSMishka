using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTest : MonoBehaviour
{


    private bool startRegen = false;
    private float prevHealth = 0;

	// Use this for initialization
	void Start () {

        Debug.Log("Initial HP: " + The.playerHealth.Health + "/" + The.playerHealth.GetTotalHealth());

	    The.playerHealth.SkillBonus = 10;
        Debug.Log("Add Passive Skill LVL1 +10% Bonus = HP: " + The.playerHealth.Health + "/" + The.playerHealth.GetTotalHealth());

	    The.playerHealth.ArtifactBonus = 10;
        Debug.Log("Add Artifact +10% bonus = HP: " + The.playerHealth.Health + "/" + The.playerHealth.GetTotalHealth());

        The.playerHealth.TakeDamage(22);
        Debug.Log("Player Take -22HP of Damage = HP: " + The.playerHealth.Health + "/" + The.playerHealth.GetTotalHealth());

	    The.playerHealth.SkillBonus = 20;
        Debug.Log("Increase Passive Skill to LVL2 +20% Bonus = HP: " + The.playerHealth.Health + "/" + The.playerHealth.GetTotalHealth());

        The.playerHealth.AddHealth(15);
        Debug.Log("Loot MedKit +15HP = HP: " + The.playerHealth.Health + "/" + The.playerHealth.GetTotalHealth());

        Debug.Log("Start regeneration each 10 sec by 3HP" );
        The.playerHealth.StartRegeneration();
	    startRegen = true;
	    prevHealth = The.playerHealth.Health;
	}
	
	// Update is called once per frame
	void Update () {
	    if (startRegen)
	    {
	        if (prevHealth < The.playerHealth.Health)
	        {
	            Debug.Log("Restored HP:  " + The.playerHealth.Health + "/" + The.playerHealth.GetTotalHealth());
	            prevHealth = The.playerHealth.Health;
	            if (prevHealth == The.playerHealth.GetTotalHealth())
	            {
	                startRegen = false;
                    Debug.Log("Stop Regeneration, HP Restored!");
	            }
	        }
	    }
	}
}
