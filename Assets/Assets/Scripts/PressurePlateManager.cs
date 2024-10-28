using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateManager : MonoBehaviour
{
    public UnityEvent puzzleSolved;
    public int PressedBlocks = 0;
    public int PadsNeeded;
    public TMP_Text CorrText;
    public bool DoorOpened = false;
    public void UpdateText(string Text)
    {
        CorrText.text = Text;
    }

    public void Addpressed() { PressedBlocks++; }
    public void Removepressed() { PressedBlocks--;}
    // Update is called once per frame
    void Update()
    {
        if(PressedBlocks == PadsNeeded && DoorOpened == false)
        {
            Debug.Log("Invoke event");
            UpdateText("Door Unlocked");
            DoorOpened = true;
            //puzzleSolved.Invoke();
        }
        else if (PressedBlocks != PadsNeeded)
        {
            //UpdateText("Door Locked");
        }
    }
}
