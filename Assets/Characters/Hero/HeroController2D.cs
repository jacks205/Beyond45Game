using UnityEngine;
using System.Collections;

public class HeroController2D : MonoBehaviour {
    
    public static float MaxSpeed = 2.5f;
    public bool FacingRight = false;

    public Animator anim;

    bool grounded = true;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask  whatIsGround;
    public float jumpForce = 30;
    public HeroHealth2D health;

    public float jumpTimeLimit = 1f;
    float jumpTimer = 0;

    bool isJumping = false;
    // Use this for initialization
    void Start () {

    }
    
    // Update is called once per frame
    void FixedUpdate () {
        if (!health.isDead)
        {
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

            float move = Input.GetAxis("Horizontal");

            anim.SetFloat("Speed", Mathf.Abs(move));

            rigidbody2D.velocity = new Vector2(move * MaxSpeed, rigidbody2D.velocity.y); 
            if (move > 0 && !FacingRight)
            {
                Flip();
            } else if (move < 0 && FacingRight)
            {
                Flip();
            }
        
            //Jumping down platform
        Physics2D.IgnoreLayerCollision( LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"),!grounded || rigidbody2D.velocity.y > 0);
        float vertical = Input.GetAxis ("Vertical");
        if (grounded && vertical < -0.05) {
            Physics2D.IgnoreLayerCollision( LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), vertical < -0.05);
        }
        }
    }
    
    void Update(){
        if (!health.isDead)
        {
            if(isJumping){
                jumpTimer += Time.deltaTime;
            }
            if(jumpTimer >= jumpTimeLimit){
                jumpTimer = 0;
                isJumping = false;
            }

            if(Input.GetAxis("Jump") > 0){
                if(!isJumping && grounded){
                    rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    isJumping = true;
                }
            }

        }
    }
    
    void Flip(){
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
