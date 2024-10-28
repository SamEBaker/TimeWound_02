using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDoorBehavior : MonoBehaviour
{
    //public GameObject UIPopUp;
    public GameManager gm;
    public int PlayerIDoor;
    public GameObject door;
    public GameObject openDoor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Find the player GameObject and get the PlayerController component
            Player playerController = collision.gameObject.GetComponent<Player>();
            //check if that deathroom's player is dead 
                playerController.DeathUI.SetActive(true);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerController = collision.gameObject.GetComponent<Player>();
            playerController.DeathUI.SetActive(false);
        }
    }
    public void Unlock()
    {
        door.transform.position = openDoor.transform.position;
    }

}
