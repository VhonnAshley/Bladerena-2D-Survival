using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{

    private Animator anim;
    [SerializeField]private const float ATTACK_ANIMATION_DURATION = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public float attackOffsetX = 0.1f;
    public float attackOffsetY = 0.1f;


    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public const float attackDamage = 1f;

    private Vector2 lastDirection = Vector2.zero;



    private void Start()
    {
        anim = GetComponent<Animator>();
        attackPoint = transform.Find("AttackPoint");
    }


    // Update is called once per frame
    void Update()
    {
     
        if (Time.time >= nextAttackTime) {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
              
               
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;

            }


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

    //Reset the attack point's position when going back to idle.
    public void ResetAttackPointPosition()
    {
        float attackOffsetX = 0.5f; // Adjust this value based on the offset of the attack point
        float attackOffsetY = 0.5f;   // Adjust this value based on the offset of the attack point

        attackPoint.localPosition = new Vector3(lastDirection.x * attackOffsetX, lastDirection.y * attackOffsetY, 0f);
    }



    void Attack()
    {
        //Attack Animation
        // Play SFX
        AudioManager.Instance.PlaySFX("swoosh");
        anim.SetTrigger("Attack");

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);

        //damage the enemy within enemy layers
        foreach (Collider2D enemy in hitEnemies)
        {
            
            // Check if the enemy has a FireballController component and call TakeDamage if it does
            FireballController fireballController = enemy.GetComponent<FireballController>();
            if (fireballController != null)
            {
                Debug.Log("We hit" + enemy.name);
                fireballController.TakeDamage(attackDamage);
            }

            // Check if the enemy has a GoblinController component and call TakeDamage if it does
            GoblinController goblinController = enemy.GetComponent<GoblinController>();
            if (goblinController != null)
            {
                Debug.Log("We hit" + enemy.name);
                goblinController.TakeDamage(attackDamage);
            }

            // Check if the enemy has a SwordlierController component and call TakeDamage if it does
            SwordlierController swordlierController = enemy.GetComponent<SwordlierController>();
            if (swordlierController != null)
            {
                Debug.Log("We hit" + enemy.name);
                swordlierController.TakeDamage(attackDamage);
            }
            
        }

      

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
