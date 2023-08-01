using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordlierController : MonoBehaviour
{
    // Enemy health
    [SerializeField] float health, maxHealth = 2f;

    public GameObject player;
    public float speed;
    public float distanceBetween = 3f; // distance to move towards the player and melee attack
    public float shootingRange = 7f; // distance for the sworldiers to start projectile attack.
    public GameObject projectile;
    public GameObject projectileParent;
    private GameObject spawnedProjectile;
    public float firerate = 1f;
    private float nextFireTime;
    public float attackRange;

    //private float distance;
    private Animator animator;

    private EnemyCombat eCombat;
    // New variable to track if the enemy is following the player
    private bool isFollowingPlayer = true;

    private void Start()
    {
        // Health system
        health = maxHealth;

        animator = GetComponent<Animator>();
        eCombat = GetComponent<EnemyCombat>();
    }

    private void Update()
    {


        if (isFollowingPlayer)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();


            eCombat.SetAttackPointPosition(direction);

            // Calculate the movement vector
            Vector2 moveDirection = direction * speed * Time.deltaTime;

            //Idle
            if (distance <= shootingRange && distance > distanceBetween)
            {
                if (Time.time >= nextFireTime)
                {
                    animator.SetBool("isProjectile", true);
                    SpawnProjectile();
                    nextFireTime = Time.time + firerate;
                }
            }
            else if (distance > shootingRange && distance <= distanceBetween)
            {

                animator.SetBool("isProjectile", false);
               
            }
            else
            {
                animator.SetBool("isProjectile", false);
            }




            //Move towards the player and will melee attack
            if (distance < distanceBetween)
            {
                // Move the goblin towards the player
                transform.Translate(moveDirection);
                animator.SetBool("isMoving", true);
                animator.SetBool("isProjectile", false);

                // Check if the distance is close enough to attack
                if (distance < attackRange)
                {
                    animator.SetBool("isAttacking", true);
                }
                else
                {
                    animator.SetBool("isAttacking", false);
                }
            }
            else
            {
                animator.SetBool("isMoving", false);
                animator.SetBool("isAttacking", false);
            }




            // Update the blend tree parameters
            animator.SetFloat("horizontalMovement", moveDirection.x);
            animator.SetFloat("verticalMovement", moveDirection.y);
        }
    }

    private void UpdateAnimator(Vector2 moveDirection)
    {
        // Update the blend tree parameters
        animator.SetFloat("horizontalMovement", moveDirection.x);
        animator.SetFloat("verticalMovement", moveDirection.y);

        // Determine if the enemy is moving or idle
        bool isMoving = moveDirection.magnitude > 0.1f;

        animator.SetBool("isMoving", true);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount; // 3 -> 2 -> 1 -> 0 = Enemy has died

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Stop following the player when the enemy dies
        isFollowingPlayer = false;

        // Debug
        Debug.Log("Enemy Swordlier died!");

        // Disable the Collider component to prevent further collisions
        GetComponent<PolygonCollider2D>().enabled = false;

        // Play the death animation by setting the "isDead" parameter to true
        animator.SetBool("isDead", true);

        // Destroy the enemy GameObject after some time (adjust the delay as needed)
        float deathAnimationDuration = 2f; // Replace with the actual duration of the death animation
        Destroy(gameObject, deathAnimationDuration);
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(transform.position, distanceBetween);
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }


    public void SpawnProjectile()
    {
        // Instantiate the projectile and store the reference to the spawned object
        spawnedProjectile = Instantiate(projectile, projectileParent.transform.position, Quaternion.identity);
    }

    public void ForceStopProjectile()
    {
        // Check if the spawnedProjectile reference is valid and destroy it
        if (spawnedProjectile != null)
        {
            Destroy(spawnedProjectile);
        }
    }
}