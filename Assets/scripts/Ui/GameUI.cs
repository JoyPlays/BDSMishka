using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public GameObject healthUIPrefab;
    public HealthUI HealthUI;

	void Awake()
	{
	    The.GameUI = this;
	    GameObject health = Instantiate(healthUIPrefab, this.transform, false);
	    HealthUI = health.GetComponent<HealthUI>();
	}

}
