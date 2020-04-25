using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        FindObjectOfType<Game>().Damage();
    }
}
