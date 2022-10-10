using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;

    private int endRollingFrame = -1;

    private GameObject character;
    private CharacterMovement characterMovement;

    [SerializeField]
    private int rollingFrameDuration = 5000009;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        character = GameObject.FindGameObjectWithTag("Character");
        characterMovement = character.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
            }

            



            animator.SetFloat("dirX", direction.x);
            animator.SetFloat("dirY", Mathf.Abs(direction.z));
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);

        }



        if (Input.GetKeyDown(KeyCode.Space))
        {

            if(endRollingFrame == -1 && characterMovement.rollingCoolDown <= 0)
            {
            animator.SetBool("isRolling", true);
            endRollingFrame = Time.frameCount + rollingFrameDuration;

            }
        }



        if (Time.frameCount > endRollingFrame && endRollingFrame!=-1)
        {
            endRollingFrame = -1;
            animator.SetBool("isRolling", false);
        }
    }
}
