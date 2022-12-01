using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{

    private Animator anim;
    private bool animation_Started;
    private bool animation_finished;


    private int jumpTime;

    private bool jumpLeft = true;

    private Rigidbody2D myBody;


    private string coroutine_Name = "FrogJump";




    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutine_Name);   
    }

    // Update is called once per frame
    void Update()
    {
     

    }

    private void LateUpdate()
    {
        if(animation_finished && animation_Started)
        {
            animation_Started = false;
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        animation_Started = true;
        animation_finished = false;

        jumpTime++;

        if (jumpLeft)
        {


            anim.Play("FrogJumpLeft");
        }
        else
        {

            anim.Play("FrogJumpRight");
        }

        StartCoroutine(coroutine_Name);

    }


    void AnimationFinished()
    {
        animation_finished = true;

        if (jumpLeft)
        {
            anim.Play("FrogIdleLeft");
        }
        else
        {
            anim.Play("FrogIdleRight");
        }


       


        if (jumpTime == 3)
        {
            jumpTime = 0;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1f;
            transform.localScale = tempScale;
            jumpLeft = !jumpLeft;
        }

       

    }



    IEnumerator  FrogDead(float timer)
    {

        yield return new WaitForSeconds(timer);

        gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.BULLET_TAG)
        {

            anim.Play("FrogDead");
            GetComponent<BoxCollider2D>().isTrigger = true;
            StartCoroutine(FrogDead(0.2f));


        }
    }

}//class























