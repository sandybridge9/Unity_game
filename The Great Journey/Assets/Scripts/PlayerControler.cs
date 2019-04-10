using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    Camera cam;
    public float Speed = 10f;
    public float Gravity = -5f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        //transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime*speed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);
        //Rigidbody rigidBody = GetComponent<Rigidbody>();

        //if (Input.GetKeyDown(KeyCode.A))
        //{

        //}  
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(horizontal, 0f, vertical) * Time.deltaTime * Speed;
        transform.Translate(playerMovement, Space.Self);
    }
}
