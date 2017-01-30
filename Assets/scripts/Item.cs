using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Player>())
		{
			Debug.Log("Player Picked up an item");
			Destroy(gameObject);
		}
	}


}
