using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    public static UnlockDoor Instance;
    public int numberOfKeysPlaced;

    private Transform openDoor;
    private static bool doorUnlocked;

    private void Awake()
    {
        numberOfKeysPlaced = 0;
        openDoor = GetComponent<Transform>();
        doorUnlocked = false;
    }

    private void Update()
    {
        if (numberOfKeysPlaced == 3 && !doorUnlocked)
        {
            openDoor.Translate(0f, 9.75f, 0f);
            doorUnlocked = true;
        }
    }
}
