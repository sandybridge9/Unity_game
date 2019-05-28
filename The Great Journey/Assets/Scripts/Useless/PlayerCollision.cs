using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OOOOOOOOOOOOOOF");
        if (collision.gameObject.tag == "Breakable_Box")
        {
            //Debug.Log("x22x2x2x2");
            Destroy(collision.gameObject);
        }
    }
}
