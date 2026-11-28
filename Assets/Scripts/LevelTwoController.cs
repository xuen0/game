using System;
using UnityEngine;

public class LevelTwoController : MonoBehaviour
{

    [System.Serializable] //show that custom class inside the Inspector
    public class LanternConnections
    {
        public Lantern lantern;              // The main lantern (e.g., Lantern 1)
        public Lantern[] connectedLanterns;  // The 2 lanterns linked to it (e.g., Lantern 3 & 4)
        public bool lastState;
    }

    // Pass 0 for Lantern 1, 1 for Lantern 2 and other.
    public LanternConnections[] puzzleLanterns = new LanternConnections[5]; //Set have how many item
    private bool isPuzzleSolved = false;

    //public void InteractWithLantern(int lanternIndex)
    //{
    //    if (lanternIndex < 0 || lanternIndex >= puzzleLanterns.Length) return; //Find have how much lantern

    //    // 1. Flip the clicked lantern
    //    Lantern mainLantern = puzzleLanterns[lanternIndex].lantern;
    //    if (mainLantern != null)
    //    {
    //        mainLantern.SetLight(!mainLantern.isLightOn);
    //    }

    //    // 2. Flip the 2 connected lanterns
    //    foreach (Lantern linkedLantern in puzzleLanterns[lanternIndex].connectedLanterns)
    //    {
    //        if (linkedLantern != null)
    //        {
    //            linkedLantern.SetLight(!linkedLantern.isLightOn);
    //        }
    //    }
    //}

    void CheckAllLanternsLit()
    {
        for (int i = 0; i < puzzleLanterns.Length; i++)
        {
            if (puzzleLanterns[i].lantern == null || !puzzleLanterns[i].lantern.isLightOn)
            {
                return;
            }
        }

        isPuzzleSolved = true;
        Debug.Log("Puzzle Solved! All 5 lanterns are lit.");
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Find the element in array and remember
        for (int i = 0; i < puzzleLanterns.Length; i++)
        {
            if (puzzleLanterns[i].lantern != null)
            {
                puzzleLanterns[i].lastState = puzzleLanterns[i].lantern.isLightOn;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPuzzleSolved) return;

        // Watch for any lantern changing its isLightOn state (via On/Off button or E key)
        for (int i = 0; i < puzzleLanterns.Length; i++)
        {
            Lantern current = puzzleLanterns[i].lantern;
            if (current != null && current.isLightOn != puzzleLanterns[i].lastState)
            {
                puzzleLanterns[i].lastState = current.isLightOn;

                // Flip its 2 connected lanterns
                foreach (Lantern linkedLantern in puzzleLanterns[i].connectedLanterns)
                {
                    if (linkedLantern != null)
                    {
                        linkedLantern.SetLight(!linkedLantern.isLightOn);
                    }
                }

                // Sync all recorded states so connected lanterns don't trigger another chain reaction
                for (int j = 0; j < puzzleLanterns.Length; j++)
                {
                    if (puzzleLanterns[j].lantern != null)
                    {
                        puzzleLanterns[j].lastState = puzzleLanterns[j].lantern.isLightOn;
                    }
                }

                CheckAllLanternsLit();
                break;
            }

        }
    }



}
