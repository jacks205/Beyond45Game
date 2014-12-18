using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public static float MaxSpeed = 2.5f;
    public bool FacingRight = false;

    public Transform player;
    public Transform bullet;

    public Vector2 bulletSpeed = new Vector2(5f,5f);

    Animator anim;
	public bool isTank;
	public Vector3 tankBulletScale = new Vector3(4f,4f);

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
        anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
//        Debug.Log(GetAngleToPlayer(this.player.transform));
        if (!enemyHealth.isDead)
        {
            if (shootCooldown > 0)
                shootCooldown -= Time.deltaTime;
            playerDistance = Vector2.Distance(player.localPosition, transform.position);
            if(playerDistance < lookingRange){
                LookAtPlayer();
//                Debug.Log(playerDistance < shootingRange);
                if(playerDistance < shootingRange && CanAttack){
                    Shoot();
                    anim.SetBool("isShooting",true);
                }else
                    anim.SetBool("isShooting",false);
            }
        }else
            anim.SetBool("isShooting",false);
	}

    void Shoot(){
        shootCooldown = shootingRate;
        
        // Create a new shot
        Transform shotTransform = Instantiate(bullet) as Transform;
        // Assign position
        shotTransform.position = transform.position;
		if (isTank)
			shotTransform.localScale = tankBulletScale;
        // The is enemy property
		Bullet2D shot = shotTransform.gameObject.GetComponent<Bullet2D>();
        if (shot != null)
        {
            shot.isEnemyShot = true;
            if(!isTank)shot.damage = (float)Random.Range(5, 12);
            else shot.damage = (float)Random.Range(20, 30);
        }

        
        // Make the weapon shot always towards it

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
//        gunRotationDegrees = GetAngleToPlayer(player);
		if (shot != null)
        {
			shot.speed = bulletSpeed;
            if(FacingRight){
                Vector2 direction = Vector2.right.Rotate(gunRotationDegrees);
				SetBulletAngleAndVelocity(shotTransform, shot, direction, gunRotationDegrees);
            }else{
                //                    SetBulletAngleAndVelocity(shotTransform, move, oppositeDirection, gunRotationDegreesOpposite);
                float gunRotationDegreesOpposite = 180 - gunRotationDegrees;
                Vector2 oppositeDirection = Vector2.right.Rotate(gunRotationDegreesOpposite);
				SetBulletAngleAndVelocity(shotTransform, shot, oppositeDirection, gunRotationDegreesOpposite);
            }
            
        }
    }

    float GetAngleToPlayer(Transform heroTransform)
    {
        float ang = Vector2.Angle(player.position, transform.position);
//        if (Vector3.Cross(transform.position, player.position).z > 0)
//            ang = 360 - ang;
        return ang;
    }

    void LookAtPlayer(){
        if (!FacingRight && (player.localPosition.x > transform.position.x))
            Flip();
        else if (FacingRight && (player.localPosition.x < transform.position.x))
            Flip();
    }

	void SetBulletAngleAndVelocity(Transform obj, Bullet2D move, Vector2 direction, float rotation){
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
