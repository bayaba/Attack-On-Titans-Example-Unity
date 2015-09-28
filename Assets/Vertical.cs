using UnityEngine;
using System.Collections;

public class Vertical : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            if (col.rigidbody.velocity.y >= 0) col.gameObject.SendMessage("RotatePlayer");
        }
    }
}
