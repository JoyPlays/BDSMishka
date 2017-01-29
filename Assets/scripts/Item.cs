using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<PlayerControler>())
		{
			PlayerControler.Lives++;
			//GameUI.UpdateUI();
			Destroy(gameObject);
		}
	}


}
