using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Pad : MonoBehaviour
{
    public string Mykey;
    public bool PressedDown;
    public PressurePlateManager pm;
    public MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        mr.material.color = Color.green;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Block")
        {
            if(collision.gameObject.name == Mykey)
            {
                Debug.Log("I am pressed");
                mr.material.color = Color.blue;
                pm.Addpressed();
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.name == Mykey)
            {
                Debug.Log("I am pressed");
                mr.material.color = Color.blue;
                pm.Addpressed();
            }
        }
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Block") && collision.gameObject.name == Mykey)
        {
            Debug.Log("Block Upressed");
            pm.Removepressed();
            mr.material.color = Color.green;
        }
        else if (collision.gameObject.CompareTag("Player") && collision.gameObject.name == Mykey)
        {
            Debug.Log("Player Upressed");
            pm.Removepressed();
            mr.material.color = Color.green;
        }
    }
}
