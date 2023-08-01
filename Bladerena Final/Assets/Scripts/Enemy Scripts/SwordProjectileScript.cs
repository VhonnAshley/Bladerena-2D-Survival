using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectileScript : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;

    

    // Start is called before the first frame update
    void Start()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemies");
        int projectilesLayer = LayerMask.NameToLayer("projectiles");
        Physics2D.IgnoreLayerCollision(enemyLayer, projectilesLayer, true);
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
        

    }

}




