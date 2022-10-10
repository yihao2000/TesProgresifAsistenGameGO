using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator animator;



    //Movement
    public float lookRadius = 30f;
    Transform target;
    NavMeshAgent agent;
    public float speed = 5.0f;

    //Attack
    public int damage = 0;
    public int currentHealth;
    public int maxHealth = 1000;
    public int attackDelay = 60;
    int currentAttackDelay = 0;
    public int stoppingDistance = 12;
    bool attack1 = true;
    bool attack2, attack3 = false;
    bool isDeath;

    //Bar
    public EnemyHealthBar healthBar;

    //Camp this animal belongs to
    EnemyCamp camp;



    public void UpdateAction(string type)
    {
        if (currentAttackDelay <= 0)
        {
            if (type == "attack")
            {
                if (attack1 == true)
                {

                    attack1 = false;
                    attack2 = true;
                    attack3 = false;
                }
                else if (attack2 == true)
                {
                  
                    attack1 = false;
                    attack2 = false;
                    attack3 = true;
                }
                else if (attack3 == true)
                {
                 
                    attack3 = false;
                    attack2 = false;
                    attack1 = true;
                }
                Player player = target.gameObject.GetComponent<Player>();
                player.TakeDamage(damage);
            }
        }
        UpdateAnimation("setAttacking");
    }

    public void UpdateAnimation(string type)
    {

        if (type == "setIdle")
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", false);
        }
        else if (type == "setMoving")
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
        }
        else if (type == "setAttacking")
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);

            if (attack1 == true)
            {

                animator.SetBool("attack1", true);
                animator.SetBool("attack2", false);
                animator.SetBool("attack3", false);
            }
            else if (attack2 == true)
            {

                animator.SetBool("attack1", false);
                animator.SetBool("attack2", true);
                animator.SetBool("attack3", false);
            }
            else if (attack3 == true)
            {
                animator.SetBool("attack1", false);
                animator.SetBool("attack2", false);
                animator.SetBool("attack3", true);
            }
        }
        else if (type == "setDeath")
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("attack1", false);
            animator.SetBool("attack2", false);
            animator.SetBool("attack3", false);
            animator.SetBool("isDeath", true);

        }
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        target = GameObject.Find("Character").transform;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        agent.stoppingDistance = stoppingDistance;

        camp = gameObject.GetComponentInParent<EnemyCamp>();


    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            if(isDeath == false)
            {
                isDeath = true;
                Die();
            }
            
        }
        else
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius && target.GetComponent<Player>().GetCurrentHealth() > 0)
            {
                healthBar.ShowHealthBar();
                agent.SetDestination(target.position);
                UpdateAnimation("setMoving");



                if (distance <= agent.stoppingDistance)
                {
                   
                        UpdateAction("attack");
                        FaceTarget();
                    
                    
                }
                else
                {
                    attack1 = true;
                    attack2 = false;
                    attack3 = false;
                    
                    
                }

            }
            else
            {
                UpdateAnimation("setIdle");
                agent.ResetPath();
                healthBar.HideHealthBar();


            }

            if (currentAttackDelay <= 0)
            {
                currentAttackDelay = attackDelay;
            }
            currentAttackDelay--;
        }



    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }


    void Die()
    {

        UpdateAnimation("setDeath");
        agent.ResetPath();
        healthBar.HideHealthBar();

        Player player = target.gameObject.GetComponent<Player>();
       
        player.AddExp(Random.Range(20, 40));

        StartCoroutine(DeathWaitCoroutine());

       

        //Remove animal from the camp
        camp.RemoveAnimal(gameObject);
        

        //Remove animal from the player enemy list
        target.gameObject.GetComponent<Player>().RemoveEnemy(gameObject);

    }

    IEnumerator DeathWaitCoroutine()
    {
        Debug.Log("Masuk Coroutine");
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);


    }
}
