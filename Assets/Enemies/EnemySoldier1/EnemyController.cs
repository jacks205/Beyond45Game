using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public static float MaxSpeed = 2.5f;
    public bool FacingRight = false;

    public Transform player;
    public Transform bullet;
    public Vector2 bulletSpeed;
    public float maxX = 1f;
    public float minX = 1f;
    public float speed = 2.5f;

    private float velocity = 1f;
    public float vAngleFactor = 1f;


    public float shootingRate = 0.25f;
    float shootCooldown;
    Vector2 startPoint;
    float startTime;
    float duration = 3f;

    public float lookingRange = 15f;
    public float shootingRange = 5f;

    public float playerDistance;
    EnemyHealth enemyHealth;
	// Use this for initialization
	void Start () {
        startPoint = transform.position;
        startTime = Time.time;
        shootCooldown = 0f;
        enemyHealth = GetComponent<EnemyHealth>();

	}

	// Update is called once per frame
	void Update () {
//        Debug.Log(EnemyHealth.IsDead);
        if (!enemyHealth.isDead)
        {
            if (shootCooldown > 0)
                shootCooldown -= Time.deltaTime;
            playerDistance = Vector2.Distance(player.localPosition, transform.position);
            if(playerDistance < lookingRange){
                LookAtPlayer();
//                Debug.Log(playerDistance < shootingRange);
                if(playerDistance < shootingRange && CanAttack)
                    Shoot();
            }
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

        float playerVertical = player.localPosition.y;
        float enemyVerticalUp = transform.position.y + vAngleFactor;
        float enemyVerticalDown = transform.position.y - vAngleFactor;
        float gunRotationDegrees = 0f;
        if (playerVertical > enemyVerticalUp)
        {
            gunRotationDegrees = 45f;
        } else if (playerVertical < enemyVerticalDown)
        {
            gunRotationDegrees = -45f;
        }
        if (move != null)
        {
            move.speed = bulletSpeed;
            if(FacingRight){
                Vector2 direction = Vector2.right.Rotate(gunRotationDegrees);
                SetBulletAngleAndVelocity(shotTransform, move, direction, gunRotationDegrees);
            }else{
                //                    SetBulletAngleAndVelocity(shotTransform, move, oppositeDirection, gunRotationDegreesOpposite);
                float gunRotationDegreesOpposite = 180 - gunRotationDegrees;
                Vector2 oppositeDirection = Vector2.right.Rotate(gunRotationDegreesOpposite);
                SetBulletAngleAndVelocity(shotTransform, move, oppositeDirection, gunRotationDegreesOpposite);
            }
            
        }
    }

    void LookAtPlayer(){
        if (!FacingRight && (player.localPosition.x > transform.position.x))
            Flip();
        else if (FacingRight && (player.localPosition.x < transform.position.x))
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
