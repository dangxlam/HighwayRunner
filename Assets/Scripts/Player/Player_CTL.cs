using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CTL : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1; //0:left 1:middle 2:right
    public float laneDistance = 4;//distance betwwen 2 lanes

    public float jumpForce;
    public float gravity = -20;

    public bool isJumping = false;
    public bool isFalling = false;
    //public GameObject playerObject;
    public Animator animator;

    private bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private bool isSliding = false;

   
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

   
    void Update()
    {
        if (!GameManager.isGameStarted)
            return;
        if(forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;

        animator.SetBool("isGameStarted", true);
        direction.z = forwardSpeed;

        isGrounded = Physics.CheckSphere(groundCheck.position, 1.1f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        //Debug.Log(isGrounded);
        if(isGrounded)
        {
            
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
            
        } else
        {
            direction.y += gravity * Time.deltaTime;
        }
        
        if(SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }

        //Check input to get lane
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane--;
        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane++;
        }

        //calculate new position
        Vector3 targetPosition = transform.position.z * transform.forward 
            + transform.position.y * transform.up;
         

        if (desiredLane == 0 )
        {
            targetPosition += Vector3.left * laneDistance;
        } else if(desiredLane == 2 )
        {
            targetPosition += Vector3.right * laneDistance;
        }
        //Debug.Log(transform.position);
        //transform.position = targetPosition;
        //transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);
        //controller.center = controller.center;

        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.fixedDeltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }
          
        //Move Player
        controller.Move(direction * Time.deltaTime);

    }

  

    private void Jump()
    {
        isJumping = true;
        direction.y = jumpForce;
        //animator.Play("Jump");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            GameManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

   

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(0.8f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
        isSliding = false;
    }
}
