using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask playerLayers;

    public float attackRange = 0.5f;

    private Vector2 lastDirection = Vector2.zero;

    private PlayerController pc;
    private SwordlierController sc;
    private Animator anim;

    private bool isEnemyattacking;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isEnemyattacking = anim.GetBool("isAttacking");
        
        //anim.SetBool("isAttacking", isEnemyattacking);


        if (isEnemyattacking)
        {
            checkHit();
        }
        else
        {
            Debug.Log("No hit");
        }
            


        
    }

    public void SetAttackPointPosition(Vector2 direction)
    {

        if (direction != Vector2.zero) // Only update the position if the direction is not zero (character is moving)
        {
            float attackOffsetX = 0.5f; // Adjust this value based on the offset of the attack point
            float attackOffsetY = 0.5f;   // Adjust this value based on the offset of the attack point

            attackPoint.localPosition = new Vector3(direction.x * attackOffsetX, direction.y * attackOffsetY, 0f);
            lastDirection = direction; // Remember the last direction
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void checkHit() {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Debug.Log("We hit player");
                playerController.Die();

            }


        }


    }
}
