using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]public Rigidbody2D body;
    [SerializeField]public Animator anim;
    private bool grounded;
    public float speed;
    private float horizontalInput;
    public float gravity;
    public float maxJump = 2;
    public float minJump = 1f;
    //timetojumpapex not used yet
    public float timeToJumpApex = 0.4f;

    public Vector3 velocity;

    //Flipping
    Vector2 directionalInput;
    
    void Start()
    {
        //Grab references
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gravity = -(2 * maxJump)/ Mathf.Pow(timeToJumpApex, 2);
        maxJump = Mathf.Abs(gravity) * timeToJumpApex;
        minJump = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJump);

        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("grounded", grounded);

        //Go back and check if there is ground under player

        //Short Jump
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (velocity.y > minJump)
            {
                velocity.y = minJump;
            }

        }
        

        //Switching out horizontalInput with directionalInput
        float horizontalInput = Input.GetAxis("Horizontal");
        
        //Move
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip
        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        //Jump
        if(Input.GetKeyUp(KeyCode.Space) && grounded)
        {
            Jump();     
        }
            
            

        //set anim
        anim.SetBool("move", horizontalInput != 0);
        
    }


    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground")
            grounded = true;
    }

    public bool canAttack(){
        return (horizontalInput == 0);
    }
    
}
