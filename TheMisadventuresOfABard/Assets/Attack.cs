using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Vector2 velocity;
    public GameObject bard;
    public int damageToGive = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;
            if (other != bard)
            {
                Debug.Log(hit.collider.gameObject);
                if (other.CompareTag("Enemy"))
                {
                    Destroy(gameObject);
                    EnemyHealthManager healthMan;
                    healthMan = other.gameObject.GetComponent<EnemyHealthManager>();
                    healthMan.HurtEnemy(damageToGive);
                }
            }
            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
            
        }
        transform.position = newPosition;
    }
}
