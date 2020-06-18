﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    public float delay = 1f;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed*movement*Time.fixedDeltaTime);

    }

    private void onTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Exit"){
            Debug.Log("Trigger Exit!!");
            Invoke("restart", delay);
        }
    }

    private void restart(){
        Application.LoadLevel(Application.loadedLevel);
    }
}
