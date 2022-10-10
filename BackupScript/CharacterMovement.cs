using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        groundDistance = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 moveDirection = new Vector3();

     
        if (direction.magnitude >= 0.1f)
        {
        
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;

        
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, maxRotateSpeed);

            moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            
            moveDirection = moveDirection.normalized;

            lastMoveDirX = moveDirection.x;
            lastMoveDirZ = moveDirection.z;

            transform.rotation = Quaternion.Euler(0, angle, 0);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;

                
            }
            else
            {
                isRunning = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(rollingCoolDown <= 0 && endFrame == -1 && direction.magnitude > 0.1f)
            {
                endFrame = Time.frameCount + rollDuration;
                isRolling = true;
            }
            
        }

        if(Time.frameCount >= endFrame && endFrame != -1)
        {
            endFrame = -1;
            isRolling = false;
            rollingCoolDown = rollingCoolDownDuration;
        }

        if(rollingCoolDown > 0)
        {
            rollingCoolDown -= 1;
        }
        

        if (!isRunning)
        {
        
            if (isRolling && direction.magnitude < 0.1f)
            {
                moveDirection.x = lastMoveDirX * normalSpeed*2;
                moveDirection.z = lastMoveDirZ * normalSpeed*2;
            }
            else
            {
                moveDirection.x *= normalSpeed ;
                moveDirection.z *= normalSpeed;
            }
        }
        else
        {
            
            moveDirection.x *= runningSpeed ;
            moveDirection.z *= runningSpeed ;
        }

        if (Physics.Raycast(transform.position, -Vector3.up, groundDistance + 0.1f) == false) 
        { 
            gravity += 1f; 

        } else 
        { 
            gravity = 9.8f; 
        }
        moveDirection.y += (gravity * -1);
        controller.Move(moveDirection * Time.deltaTime);


    }
}
