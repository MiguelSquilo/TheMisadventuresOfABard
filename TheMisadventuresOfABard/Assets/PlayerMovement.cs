using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    public float delay = 1f;
    public GameObject shootingObject;
    public float FIRE_BASE_SPEED;
    private Vector2 shootingDir;
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1|| Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            shootingDir = movement;
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }
        if (Input.GetKeyDown(KeyCode.Space))
         {
            shoot();
         }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * movement * Time.fixedDeltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exit")
        {
            Debug.Log("Trigger Exit!!");
            Invoke("restart", delay);
        }
    }

    private void restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void shoot()
    {
        // Vector2 shootingDirection;
        /*if (movement.Equals(Vector2.zero))
        {
            shootingDirection = new Vector2(0,-1);
        }
        else
        {
            shootingDirection = movement;
        }*/
        shootingDir.Normalize();

        GameObject note = Instantiate(shootingObject, transform.position, Quaternion.identity);
        Attack attackScript = note.GetComponent<Attack>();
        attackScript.velocity = shootingDir * FIRE_BASE_SPEED;
        attackScript.bard = gameObject;
        note.transform.Rotate(0, 0, Mathf.Atan2(shootingDir.y, shootingDir.x) * Mathf.Rad2Deg);
        Destroy(note, 2.0f);
    }
}




/* IEnumerator Attack()
    {
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(2);
        Debug.Log("done casting");
    }
}*/
