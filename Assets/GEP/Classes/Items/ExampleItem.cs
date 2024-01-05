using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleItem : MonoBehaviour, IPickupable
{
    public GameObject Player;

    public void Pickup()
    {
        Destroy(gameObject);
    }
}
