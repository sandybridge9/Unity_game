using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    Camera cam;
    public float speed;
    private Rigidbody rb;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Debug.Log("x: " + rb.velocity.x + " y: " + rb.velocity.y + " z: " + rb.velocity.z);
        Vector3 v = new Vector3(0.0f, rb.velocity.y, 0.0f);
        rb.velocity = v;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        //rb.velocity = movement * speed;
        rb.AddRelativeForce(movement * speed);
    }
    //Camera cam;
    //public float Speed = 10f;
    //Vector3 movement = new Vector3();
    //Rigidbody rb3D;

    //private void Start()
    //{
    //    cam = Camera.main;
    //    rb3D = GetComponent<Rigidbody>();
    //}
    //void FixedUpdate()
    //{
    //    GetInput();
    //    MoveCharacter(movement);
    //}
    //private void GetInput()
    //{
    //    movement.x = Input.GetAxisRaw("Horizontal");
    //    movement.y = 0f;
    //    movement.z = Input.GetAxisRaw("Vertical");
    //    Debug.Log("x: "+movement.x +" y: "+movement.y +" z: " +movement.z);
    //}
    //public void MoveCharacter(Vector3 movementVector)
    //{
    //    movementVector.Normalize();
    //    // move the RigidBody instead of moving the Transform
    //    rb3D.velocity = movementVector * Speed * Time.deltaTime;
    //}

    //Camera cam;
    //public float Speed = 10f;
    //public float Gravity = -5f;

    //Start is called before the first frame update
    //void Start()
    //{
    //    cam = Camera.main;
    //}

    //Update is called once per frame
    //void FixedUpdate()
    //{
    //    MovePlayer();
    //    transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * Speed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * Speed);
    //    Rigidbody rigidBody = GetComponent<Rigidbody>();

    //    if (Input.GetKeyDown(KeyCode.A))
    //    {

    //    }
    //}

    //void MovePlayer()
    //{
    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");
    //    Vector3 playerMovement = new Vector3(horizontal, 0f, vertical) * Time.deltaTime * Speed;
    //    transform.Translate(playerMovement, Space.Self);
    //}
}
