using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public GameObject healthUIPrefab;

    [HideInInspector]
    public HealthUI HealthUI;

	void Awake()
	{
	    The.GameUI = this;
	}

    void Start()
    {
        GameObject health = Instantiate(healthUIPrefab, this.transform, false);
        HealthUI = health.GetComponent<HealthUI>();
    }

}
