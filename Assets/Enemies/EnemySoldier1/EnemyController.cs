using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public static float MaxSpeed = 2.5f;
    public static bool FacingRight = false;

    public Transform player;
    public Transform bullet;
    public Vector2 bulletSpeed;
    public float maxX = 1f;
    public float minX = 1f;
    public float speed = 2.5f;

    private float velocity = 1f;



    public float shootingRate = 0.25f;
    float shootCooldown;
    Vector2 startPoint;
    float startTime;
    float duration = 3f;

    float playerDistance;

	// Use this for initialization
	void Start () {
        startPoint = transform.position;
        startTime = Time.time;
        shootCooldown = 0f;
	}

	// Update is called once per frame
	void Update () {
//        Debug.Log(EnemyHealth.IsDead);
        if (!EnemyHealth.IsDead)
        {
            if (shootCooldown > 0)
                shootCooldown -= Time.deltaTime;
            playerDistance = Vector2.Distance(player.position, transform.position);
            if(playerDistance < 15f){
                LookAtPlayer();
                if(playerDistance < 5f && CanAttack)
                    Shoot();
            }
//            rigidBody.velocity = ( player.position - transform.position )*speed;
//            transform.Translate(-speed * Time.deltaTime, 0, 0);
//            transform.position = Vector2.Lerp(startPoint, player.position, (Time.time - startTime) / duration);
            //if enemy moves off left side of screen...
//        Debug.Log(transform.position.x);
//        if(transform.position.x <= -25)
//        {
//            //then have randomize its position and move it to the right side of the screen.
//            transform.position = new Vector3(25* Time.deltaTime,0,0);
//        }
        }
	}

    void Shoot(){
        shootCooldown = shootingRate;
        
        // Create a new shot
        Transform shotTransform = Instantiate(bullet) as Transform;
        // Assign position
        shotTransform.position = transform.position;
        
        // The is enemy property
        ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            shot.isEnemyShot = true;
        }
        
        // Make the weapon shot always towards it
        BulletMove move = shotTransform.gameObject.GetComponent<BulletMove>();
//        Vector2 direction = Vector2.right.Rotate(gunRotationDegrees);
        //            float gunRotationDegreesOpposite = 180 - gunRotationDegrees;
        //            Vector2 oppositeDirection = Vector2.right.Rotate(gunRotationDegreesOpposite);
        if (move != null)
        {
            move.speed = bulletSpeed;
            if(FacingRight){
                Vector2 angle0 = Vector2.right.Rotate(0f);
                SetBulletAngleAndVelocity(shotTransform, move, angle0, 0f);
            }else{
                //                    SetBulletAngleAndVelocity(shotTransform, move, oppositeDirection, gunRotationDegreesOpposite);
                Vector2 angle180 = Vector2.right.Rotate(180f);
                SetBulletAngleAndVelocity(shotTransform, move, angle180, 180f);
            }
            
        }
    }

    void LookAtPlayer(){
        if (!FacingRight && (player.position.x > transform.position.x))
            Flip();
        else if (FacingRight && (player.position.x < transform.position.x))
            Flip();
    }

    void SetBulletAngleAndVelocity(Transform obj, BulletMove move, Vector2 direction, float rotation){
        obj.Rotate(new Vector3(0,0,rotation));
        move.direction = direction; // towards in 2D space is the right of the sprite
    }

    void Flip(){
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
