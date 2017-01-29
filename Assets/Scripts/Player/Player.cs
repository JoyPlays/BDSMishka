using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerHealth Health;


    void Awake()
    {
        The.Player = this;
        Health = new PlayerHealth();
    }

	
}
