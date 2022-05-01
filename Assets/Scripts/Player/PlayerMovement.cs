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

    // Joystick
    public Transform innerCircle;
    public Transform outerCircle;
    private Vector2 pointA;
    private Vector2 pointB;
    private bool joystickPressed = false;
    private bool touchStart = false; // finger is pressed down on the screen

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

        if (Input.GetMouseButtonDown(0)) { // Get pointA
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 origin = new Vector2(pos.x, pos.y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);
            if (hit != null && hit.collider != null && hit.collider.gameObject != null) {
                if (hit.collider.gameObject.tag == "Joystick") {
                    joystickPressed = true;

                    pointA = outerCircle.transform.position;

                    pointB = Camera.main.ScreenToWorldPoint(
                        new Vector3(Input.mousePosition.x, 
                                    Input.mousePosition.y, 
                                    Camera.main.transform.position.z));
                }
            }
        }

        if (joystickPressed && Input.GetMouseButton(0)){ // Get pointB
            touchStart = true;
            pointA = outerCircle.transform.position;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        } else {
            touchStart = false;
            joystickPressed = false;
            innerCircle.transform.position = outerCircle.transform.position;
        }

        //Jump
        if(Input.GetKeyUp(KeyCode.Space) && grounded)
        {
            Jump();     
        }
    }

    private void FixedUpdate() {
        if (touchStart) {
            // Calculate the offset of two points
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            body.velocity = new Vector2(direction.x * speed, body.velocity.y);

            // Set the inner circle to stay within the outer circle
            innerCircle.transform.position =
                new Vector3(
                        (pointB.x > pointA.x + 2 ? pointA.x + 2 : (pointB.x < pointA.x - 2 ? pointA.x - 2 : pointB.x)), 
                        innerCircle.transform.position.y, 
                        innerCircle.transform.position.z);

            //Flip
            if(direction.x > 0.01f)
                transform.localScale = Vector3.one;
            else if(direction.x < -0.01f)
                transform.localScale = new Vector3(-1,1,1);
            
            //set anim
            anim.SetBool("move", direction.x != 0);
        }
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
