using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    private float speed = 5.0f;
    private Animator anim;
    private Rigidbody2D myBody;



    //jump

    public Transform groundChecker;
    private float jumpPower=8.0f;

    private bool isGrounded;
    private bool jumped;

    public LayerMask groundLayer;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        CheckIfGrounded();
        PlayerJump();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChnageDirection(1);
        }
        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChnageDirection(-1);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));

    }

    void ChnageDirection(int direction)
    {
        Vector2 currentScale = transform.localScale;
        currentScale.x = direction;
        transform.localScale = currentScale;

    }

     void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                anim.SetBool("Jump", true);

            }
        }
    }



     void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundChecker.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded)
        { 
            if (jumped)
            {

                jumped = false;
                anim.SetBool("Jump", false);
            }

        }

    }


}//class
