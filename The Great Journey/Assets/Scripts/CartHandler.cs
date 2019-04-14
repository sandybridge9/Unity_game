using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartHandler : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isNear", false);
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetBool("isNear", false);
    }

    void OnTriggerEnter(Collider collider)
    {
        animator.SetBool("isNear", true);
    }
    void OnTriggerExit(Collider collider)
    {
        animator.SetBool("isNear", false);
    }
}
