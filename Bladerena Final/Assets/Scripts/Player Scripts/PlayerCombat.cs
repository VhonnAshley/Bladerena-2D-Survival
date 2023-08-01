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

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public const float attackDamage = 1f;

    private void Start()
    {
        anim = GetComponent<Animator>();

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

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);

    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }


    void Attack()
    {
        //Attack Animation
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
