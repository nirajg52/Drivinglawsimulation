using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPosition : MonoBehaviour
{
    public CarController CarController;
  
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Car"))
        {

            print("hey");
            CarController.respawnPoint = gameObject.transform.position;
        }

    }
}
