using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    public Image healthIndecator;
    public Image expIndecator;

    public RectTransform LifeHolderShifter;
    public GameObject Expandable;
    public GameObject LifePrefab;
    private List<GameObject> lifes = new List<GameObject>();


    void Awake()
    {
        if (LifePrefab != null)
        {
            for (int i = 0; i < The.settings.PlayerLifes; i++)
            {
                lifes.Add(Instantiate(LifePrefab, Expandable.transform, false));
            }
            LifeHolderShifter.anchoredPosition = new Vector2(The.settings.PlayerLifes * 25, 0);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
