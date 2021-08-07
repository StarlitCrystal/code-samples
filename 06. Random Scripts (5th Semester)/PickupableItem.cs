using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickupableItem : MonoBehaviour
{
    //NOT OWNED BY ME. SOURCE: https://www.patrykgalach.com/2020/03/16/pick-up-items-in-unity/

    private Rigidbody rigid;
    public Rigidbody rigidBody => rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
}
