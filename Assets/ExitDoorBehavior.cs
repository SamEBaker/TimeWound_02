using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorBehavior : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Find the player GameObject and get the PlayerController component
            Player playerController = collision.gameObject.GetComponent<Player>();
            //check if that deathroom's player is dead 
            playerController.ExitUI.SetActive(true);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerController = collision.gameObject.GetComponent<Player>();
            playerController.ExitUI.SetActive(false);
        }
    }

}
