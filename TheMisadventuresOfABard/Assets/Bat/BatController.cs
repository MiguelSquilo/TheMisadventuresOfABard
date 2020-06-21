using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{

    private Animator myAnim;
    private Transform target;
    [SerializeField]
    private float speed = 0f;



    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    public void FollowPlayer()
    {
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("MoveX", (target.position.x - transform.position.x));
        myAnim.SetFloat("MoveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Debug.Log("ENTROU NA CENA");
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
        */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("SAIU DA CENA");
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("SAIU DA CENA");
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("ENTROU NA CENA");
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }else
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    
}