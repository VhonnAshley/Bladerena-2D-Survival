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
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


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
            Debug.Log("We hit" + enemy.name);

        }

      

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
