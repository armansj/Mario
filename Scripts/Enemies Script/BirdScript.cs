using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{

    Animator anim;

    Rigidbody2D myBody;

    Vector3 moveDirection = Vector3.left;

    private Vector3 originalPosition;

    private Vector3 movePosition;

    public GameObject birdEgg;

    public LayerMask playerLayer;

    private bool attacked;

    private bool canMove;

    private float speed=2.5f;



    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalPosition.x += 6f;


        movePosition = transform.position;
        movePosition.x -= 6f;


        canMove = true;



    }

    // Update is called once per frame
    void Update()
    {
        MoveTheBird();
        DropEgg();
    }


    void MoveTheBird()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * speed*Time.smoothDeltaTime);

            if (transform.position.x >= originalPosition.x)

            {
                moveDirection = Vector3.left;
                ChangeDirection(0.5f);

            }else if(transform.position.x <= movePosition.x)
            {
                moveDirection = Vector3.right;
                ChangeDirection(-0.5f);
            }
        }
    }


    void ChangeDirection(float direction)
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x = direction;
        transform.localScale = currentScale;


    }


    void DropEgg()
    {

        if (!attacked)
        {
            if (Physics2D.Raycast(transform.position,Vector2.down,Mathf.Infinity,playerLayer))
            {
                Instantiate(birdEgg, new Vector3(transform.position.x, transform.position.y - 1f,transform.position.z),Quaternion.identity);

                attacked = true;
                anim.Play("BirdFly");

            }



        }

    }



    IEnumerator BirdDead(float timer)
    {

        yield return new WaitForSeconds(timer);

        gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D target)
    {

        if (target.tag == MyTags.BULLET_TAG)
        {

            anim.Play("BirdDead");
            GetComponent<BoxCollider2D>().isTrigger = true;
            myBody.bodyType = RigidbodyType2D.Dynamic;
            canMove = false;
            StartCoroutine(BirdDead(2.0f));


        }
        
    }



}//class
