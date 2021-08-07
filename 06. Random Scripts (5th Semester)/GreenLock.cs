using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLock : MonoBehaviour
{
    private bool keyInLock;
    public GameObject door;
    public static UnlockDoor doorUnlock;

    private static bool lockUnlocked;

    private void Awake()
    {
        keyInLock = false;
        doorUnlock = door.GetComponent<UnlockDoor>();
        lockUnlocked = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (keyInLock && !lockUnlocked)
        {
            doorUnlock.numberOfKeysPlaced++;
            lockUnlocked = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GKey"))
        {
            keyInLock = true;
        }
    }
}
