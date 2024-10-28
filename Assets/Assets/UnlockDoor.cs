using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{ 
    public List<GameObject> VaultDoors;
    public List<GameObject> VaultDoorsPos;
    public int KeyType;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Find the player GameObject and get the PlayerController component
            Player playerController = collision.gameObject.GetComponent<Player>();
            if (playerController != null)
            {
                if (KeyType == 0)
                {
                    if (playerController.HasKey[0] == true)
                    {
                        OnUnlocked(0);
                        playerController.LoseKey(0);
                    }
                }
                else if (KeyType == 1)
                {
                    if (playerController.HasKey[1] == true)
                    {
                        OnUnlocked(1);
                        playerController.LoseKey(1);

                    }
                }
                else if (KeyType == 2)
                {
                    if (playerController.HasKey[2] == true)
                    {
                        OnUnlocked(2);
                        playerController.LoseKey(2);

                    }
                }
                else if (KeyType == 3)
                {
                    if (playerController.HasKey[3] == true)
                    {
                        OnUnlocked(3);
                        playerController.LoseKey(3);

                    }
                }
            }
        }
    }

    public void OnUnlocked(int index)
    {
            VaultDoors[index].transform.position = VaultDoorsPos[index].transform.position;
    }

}
