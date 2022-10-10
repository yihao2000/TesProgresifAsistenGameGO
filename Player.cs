using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Panel
    UpgradeOption upgradeOption;
    EndMenu endMenu;

    //Texture Detection
    int texture;

    //EnemyList
    List<GameObject> enemyList = new List<GameObject>();

    //PlayerAttack
    public int damage = 20;
    public int attackDuration = 150;
    int currAttackDuration = 0;
    bool attack1 = true;
    bool attack2 = false;

    //Weapon
    bool holdingWeapon = false;
    bool weaponAttack1 = true;
    bool weaponAttack2 = false;

    //Player Attributes

    int currentHealth;
 
    int maxHealth = 1000;

    public HealthBar healthBar;

    int currentExp = 0;
    int maxExp = 100;
    
    int currLevel = 1;

    public ExperienceBar expBar;

    int upgradePoint, agilityPoint, strengthPoint, powerPoint;

    
   
    


    //Movement Related
    private CharacterController controller;
    [SerializeField]
    private float normalSpeed = 10f;
    [SerializeField]
    private float runningSpeed = 15f;
    private float currentVelocity;
    private float gravity = 9.8f;
    private float lastMoveDirX;
    private float lastMoveDirZ;
    private float groundDistance;
    private bool isRunning = false;
    [SerializeField]
    private float maxRotateSpeed = 0.1f;
    private int endFrame = -1;
    private int rollDuration = 90;
    private bool isRolling = false;
    public float rollingCoolDown;
    [SerializeField]
    private float rollingCoolDownDuration = 180;
    [SerializeField]
    private Transform camera;
    Vector3 moveDirection = new Vector3();
    private bool isIdling = false;


    //Animation Related Attributes
    private Animator animator;
    private int endRollingFrame = -1;
    private GameObject character;
    private CharacterMovement characterMovement;
    [SerializeField]
    private int rollingFrameDuration = 5000009;


    

    // Start is called before the first frame update
    void Start()
    {

        upgradeOption = GameObject.Find("UpgradeOption").GetComponent<UpgradeOption>();
        endMenu = GameObject.Find("EndMenu").GetComponent<EndMenu>();
      

        controller = GetComponent<CharacterController>();
        groundDistance = GetComponent<Collider>().bounds.extents.y;

        animator = GetComponentInChildren<Animator>();
        character = GameObject.FindGameObjectWithTag("Character");
        characterMovement = character.GetComponent<CharacterMovement>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealthText(currentHealth, maxHealth);

        currentExp = 0;
        expBar.SetMaxExp(maxExp);
        expBar.SetCurrLevel(currLevel);

        upgradePoint = 0;
        agilityPoint = 0;
        strengthPoint = 0;
        powerPoint = 0;
        animator.SetBool("isAlive", true);

        texture = 0;
    }

    public int GetCurrentHealth()
    {
        return this.currentHealth;
    }

    public void AddExp(int exp)
    {
        currentExp += exp;

        expBar.SetExp(currentExp);

        while (currentExp >= maxExp)
        {
            upgradePoint++;
            LevelUp();
        }
    }


    public void LevelUp()
    {

        //Exp & LevelUp
        currentExp -= maxExp;
        expBar.SetExp(currentExp);
        IncreaseMaxExp();
        currLevel += 1;
        expBar.SetCurrLevel(currLevel);

        // HP
        ResetHealth();
        healthBar.SetHealthText(currentHealth, maxHealth);

        ShowPanel();

        AudioSource audio = GameObject.Find("LevelUp").GetComponent<AudioSource>();
        audio.Play();
    }

    public void IncreaseMaxExp()
    {
        expBar.SetMaxExp(maxExp);
        maxExp = 1000 + 100*currLevel;
    }

    public void ShowPanel()
    {
        ModifyStatsPanel();
        upgradeOption.ShowPanel();
        
    }

    public void ModifyStatsPanel()
    {
        upgradeOption.ModifyUpgradePoint(upgradePoint);
        upgradeOption.ModifyStrengthPoint(strengthPoint);
        upgradeOption.ModifyPowerPoint(powerPoint);
        upgradeOption.ModifyAgilityPoint(agilityPoint);
    }

    public void TakeDamage(int damage)
    {
      
            currentHealth -= damage;
            healthBar.SetHealthText(currentHealth, maxHealth);
            healthBar.SetHealth(currentHealth);
        
        
    }

    public void DetectSand()
    {
        this.texture = 1;
        animator.SetInteger("texture", this.texture);
    }

    public void DetectGrass()
    {
        this.texture = 0;
        animator.SetInteger("texture", this.texture);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }


    public int CalculateDamage()
    {
        return damage + strengthPoint * 100;
    }
    public void UpdateAction(string type)
    {
        if(type == "setRolling")
        {
            if (rollingCoolDown <= 0 && endFrame == -1 && GetDirection().magnitude > 0.1f)
            {
                endFrame = Time.frameCount + rollDuration;
                isRolling = true;
            }

            if (endRollingFrame == -1 && rollingCoolDown <= 0)
            {
                UpdateAnimation("startRolling");
                endRollingFrame = Time.frameCount + rollingFrameDuration;
            }
        }



        else if (type == "setRunning")
        {

            moveDirection.x *= runningSpeed;
            moveDirection.z *= runningSpeed;

            UpdateAnimation("setRunning");
        }
        else if(type =="setWalking")
        {
            if (isRolling && GetDirection().magnitude < 0.1f)
            {
                moveDirection.x = lastMoveDirX * normalSpeed * 2;
                moveDirection.z = lastMoveDirZ * normalSpeed * 2;
            }
            else
            {
             
                moveDirection.x *= normalSpeed;
                moveDirection.z *= normalSpeed;
            }

            UpdateAnimation("setWalking");
        }
        else if(type == "startAttacking")
        {
            if(currAttackDuration <= 0)
            {
                if (isIdling == true && isRolling == false && isRunning == false)
                {
                    animator.SetBool("isAttacking", true);
                    if (attack1 == true && holdingWeapon == false)
                    {
                        attack1 = false;
                        attack2 = true;
                    }
                    else if (attack2 == true && holdingWeapon == false)
                    {
                        attack1 = true;
                        attack2 = false;
                    }
                    else if (attack1 == false && attack2 == false && holdingWeapon == false)
                    {
                        attack1 = true;
                    }else if(holdingWeapon == true && weaponAttack2 == false && weaponAttack1 == false)
                    {
                        weaponAttack1 = true;
                    }else if(weaponAttack1 == true)
                    {
                        weaponAttack1 = false;
                        weaponAttack2 = true;
                    }else if(weaponAttack2 == true)
                    {
                        weaponAttack1 = true;
                        weaponAttack2 = false;
                    }


                    if (enemyList.Count > 0)
                    {
                        foreach (GameObject o in enemyList)
                        {
                            o.GetComponentInChildren<Enemy>().TakeDamage(CalculateDamage());
                        }
                    }
                    UpdateAnimation("startAttacking");

                }
                currAttackDuration = attackDuration-(5*agilityPoint);
            } 
           
        }
        else if(type == "stopAttacking")
        {
            attack1 = false;
            attack2 = false;
            weaponAttack1 = false;
            weaponAttack2 = false;

            UpdateAnimation("stopAttacking");
        }
    }

 

    public void UpdateGravity()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, groundDistance + 0.1f) == false)
        {
            gravity += 1f;

        }
        else
        {
            gravity = 9.8f;
        }
        moveDirection.y += (gravity * -1);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public void IncreaseStrength()
    {
        maxHealth = 1000 + 100 * strengthPoint;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        ResetHealth();
        healthBar.SetHealthText(currentHealth, maxHealth);
    }

    public void UpgradeStats(string type)
    {
        if (type.Equals("Strength"))
        {
            strengthPoint++;

            IncreaseStrength();
        }
        else if (type.Equals("Agility"))
        {
            agilityPoint++;

        }else if (type.Equals("Power"))
        {
            powerPoint++;

        }
        

        upgradePoint--;

        ModifyStatsPanel();
    }

    public void UpdateAnimation(string type)
    {  
        if (type == "setRunning")
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
        }
        else if(type == "setWalking")
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);
        }
        else if(type == "setIdle")
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }else if(type == "endRolling")
        {
            animator.SetBool("isRolling", false);
        }
        else if(type == "startRolling")
        {
            animator.SetBool("isRolling", true);
        }else if(type == "startAttacking")
        {
            if(attack1 == true)
            {
                animator.SetBool("attack1", true);
                animator.SetBool("attack2", false);
            }else if(attack2 == true)
            {
                animator.SetBool("attack2", true);
                animator.SetBool("attack1", false);
            }else if(holdingWeapon == true)
            {
                if(weaponAttack1 == true)
                {
                    animator.SetBool("weaponAttack1", true);
                    animator.SetBool("weaponAttack2", false);
                }else if(weaponAttack2 == true)
                {
                    animator.SetBool("weaponAttack1", false);
                    animator.SetBool("weaponAttack2", true);
                }
            }
        }else if(type == "stopAttacking")
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("attack1", false);
            animator.SetBool("attack2", false);
            animator.SetBool("weaponAttack1", false);
            animator.SetBool("weaponAttack2", false);
        }



        animator.SetFloat("dirX", GetDirection().x);
        animator.SetFloat("dirY", Mathf.Abs(GetDirection().z));
    }

    public Vector3 GetDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        return direction;
    }


    public float GetTargetAngle()
    {
        return  Mathf.Atan2(GetDirection().x, GetDirection().z) * Mathf.Rad2Deg + camera.eulerAngles.y;
    }


    public float GetAngle()
    {
        return Mathf.SmoothDampAngle(transform.eulerAngles.y, GetTargetAngle(), ref currentVelocity, maxRotateSpeed);
    }


    public void UpdateCameraRotation()
    {
        transform.rotation = Quaternion.Euler(0, GetAngle(), 0);
    }

    public bool SetMoveDirection()
    {
        Vector3 direction = GetDirection();


        if (direction.magnitude >= 0.1f)
        {
            isIdling = false;
            

            float angle = GetAngle();

            moveDirection = Quaternion.Euler(0, GetTargetAngle(), 0) * Vector3.forward;

            moveDirection = moveDirection.normalized;

            lastMoveDirX = moveDirection.x;
            lastMoveDirZ = moveDirection.z;

            UpdateCameraRotation();

            return true;
        }
        else
        {
            moveDirection.x = 0;
            moveDirection.z = 0;
            isIdling = true;
            UpdateAnimation("setIdle");
            return false;
        }

    }


    public void CheckRollingEnd()
    {
        if (Time.frameCount > endRollingFrame && endRollingFrame != -1)
        {
            endRollingFrame = -1;
            UpdateAnimation("endRolling");
        }

        if (Time.frameCount >= endFrame && endFrame != -1)
        {
   
            endFrame = -1;
            isRolling = false;
            rollingCoolDown = rollingCoolDownDuration;
        }


        if (rollingCoolDown > 0)
        {
            rollingCoolDown -= 1;
        }


        
    }

    public void CheckRollingMoveDistance()
    {
        if (!isRunning)
        {

            if (isRolling && GetDirection().magnitude < 0.1f)
            {
                moveDirection.x = lastMoveDirX * normalSpeed * 2;
                moveDirection.z = lastMoveDirZ * normalSpeed * 2;
            }
 
        }
    }




    // Update is called once per frame
    void Update()
    {

        if (currentHealth > 0)
        {
            if (upgradePoint <= 0)
            {
                upgradeOption.HidePanel();
            }


            if (SetMoveDirection() == true)
            {
                UpdateAction("stopAttacking");
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    isRunning = true;

                    UpdateAction("setRunning");

                }
                else
                {
                    isRunning = false;
                    UpdateAction("setWalking");
                }


            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

                UpdateAction("startAttacking");

            }


            if (currAttackDuration > 0)
            {
                currAttackDuration--;
            }



            if (Input.GetKeyDown(KeyCode.Space))
            {
                UpdateAction("setRolling");
            }

            CheckRollingEnd();

            CheckRollingMoveDistance();


            UpdateGravity();


            controller.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isAlive", false);
            endMenu.ShowPanel();
        }
       


    }

    public void holdWeapon()
    {
        holdingWeapon = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Enemy>() != null && !enemyList.Contains(other.gameObject))
        {
            enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null )
        {
            enemyList.Remove(other.gameObject);
        }
    }
}
