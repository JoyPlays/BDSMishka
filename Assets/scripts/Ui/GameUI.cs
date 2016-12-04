using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

	private static GameUI instance = null;
	public static GameUI Instance
	{
		get { return instance; }
	}

	public Text livesText;

	internal static void UpdateUI()
	{
		if (!instance) return;
	
		if (instance.livesText) instance.livesText.text = string.Format("Lives: {0}", PlayerControler.Lives);

	}

	void Awake()
	{
		instance = this;
	}

}
