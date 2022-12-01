using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    private bool moveLeft;
    private float speed = 1.0f;
    

    Rigidbody2D myBody;
    Animator anim;


    private bool canMove;
    private bool stunned;
    


    public LayerMask playerLayer;


    public Transform down_Collision,left_Collision,right_Collision,top_Collision;

    private Vector3 left_Collision_Position;
    private Vector3 right_Collision_Position;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        left_Collision_Position = left_Collision.position;
        right_Collision_Position = right_Collision.position;

    }

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(speed, myBody.velocity.y);

            }

        }

        CheckCollision();



    }
    
   

 
    void CheckCollision()
    {
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f,playerLayer);



        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);


        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);

                    anim.Play("Stunned");
                    stunned = true;


                    //Beetle Code!
                    if (tag == MyTags.BEETLE_TAG)
                    {
                        anim.Play("Stunned");
                        StartCoroutine(Dead(0.5f));


                    }





                }
            }

        }


            if (leftHit)
            {
                if (leftHit.collider.gameObject.tag == "Player")
                {

                     if (!stunned)
                    {
                        //Player Killed
                        print("Hit Left");
                    }
                    else
                    {

                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(15.0f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }

                      
                    }

                }
            }
                
            



            if (rightHit)
            {
                if (rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
                {

                    if (!stunned)
                    {
                    print("Hit Right");
                    //Player Killed
                }
                if (tag != MyTags.BEETLE_TAG)
                {
                    myBody.velocity = new Vector2(15.0f, myBody.velocity.y);
                    StartCoroutine(Dead(3f));
                }

            }



        }



        




        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f))
        {

            ChangeDirection(); 
        }

    }



    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 tempSclae = transform.localScale;

        if (moveLeft)
        {
            tempSclae.x = Mathf.Abs(tempSclae.x);
            left_Collision.position = left_Collision_Position;
            right_Collision.position = right_Collision_Position;

        }
        else
        {
            tempSclae.x =- Mathf.Abs(tempSclae.x);
            left_Collision.position = right_Collision_Position;
            right_Collision.position = left_Collision_Position;

        }
        transform.localScale = tempSclae;

    }

    IEnumerator Dead(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);

    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.BULLET_TAG)
        {
            if (tag == MyTags.BEETLE_TAG)
            {
                anim.Play("Stunned");

                canMove = false;
                myBody.velocity = new Vector2(0,0);

                StartCoroutine(Dead(0.4f));


            }

             if (tag == MyTags.SNAIL_TAG)
            {
                if (!stunned)
                {
                    anim.Play("Stunned");
                    stunned = true;
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                }
                else
                {
                    StartCoroutine(Dead(0.1f));
                }



            }
            
        }

    }


}//class
