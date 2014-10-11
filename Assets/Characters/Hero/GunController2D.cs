using UnityEngine;
using System.Collections;

public class GunController2D : MonoBehaviour {
    public static int BULLET_DAMAGE = 100;
    
    public bool usingController = true;

    public Transform shootingSpot9;

    public Transform bullet;
    public Transform grenade;

    public float grenadeRange = 20f;
    
    public float bulletSpeed = 2f;
    public float shootingRate = 0.25f;
    public float throwingRate = 0.5f;
    Transform currentSelectedFiringPosition;
    SpriteRenderer spriteRend;
    Animator gunAngle;
    float shootCooldown;
    float throwCooldown;
    public float flyTime= 2.0f;
    
    // Use this for initialization
    void Start () {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Grenade"));
        spriteRend = GetComponent<SpriteRenderer> ();
        gunAngle = GetComponent<Animator> ();
        shootCooldown = 0f;
        throwCooldown = 0f;
    }
    
    // Update is called once per frame
    void Update () {
        if (shootCooldown > 0)
            shootCooldown -= Time.deltaTime;
        if (throwCooldown > 0)
            throwCooldown -= Time.deltaTime;
        CheckToShoot(); 
        CheckToThrow();
    }

    void CheckToShoot(){

        if (CanAttack && isFiring())
        {
            shootCooldown = shootingRate;
            
            // Create a new shot
            Transform shotTransform = Instantiate(bullet) as Transform;
            
            // Assign position
            shotTransform.position = transform.position;
            
            // The is enemy property
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
//                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot always towards it
            BulletMove move = shotTransform.gameObject.GetComponent<BulletMove>();
            if (move != null)
            {
                if(HeroController2D.FacingRight){
                    move.direction = this.transform.right; // towards in 2D space is the right of the sprite
                }else{
                    move.direction = -this.transform.right;
                    FlipBullet(shotTransform);
                }
                    
            }
        }
    }


    void CheckToThrow(){
        if (CanThrow && isThrowingGrenade())
        {
            throwCooldown = throwingRate;
            Transform grenadeTransform = Instantiate(grenade) as Transform;

            grenadeTransform.position = this.transform.position;
            grenadeTransform.rigidbody2D.velocity = -ThrowGrenadeVel(transform.position, HeroController2D.FacingRight);
            Destroy(grenadeTransform.gameObject, 5f);
        }
    }


    bool isFiring(){
        float fire1 = Input.GetAxis ("Fire1");
        if (fire1 == 1)
        {
            gunAngle.SetBool("isShooting", true);//Show Animation
            return true;
        } else
        {
            gunAngle.SetBool("isShooting", false);//Show Animation
            return false;
        }
    }

    bool isThrowingGrenade(){
        float fire2 = Input.GetAxis ("Fire2");
        return fire2 == 1;
    }

    void FlipBullet(Transform shotTransform){
        Vector3 theScale = shotTransform.localScale;
        theScale.x *= -1;
        shotTransform.localScale = theScale;
    }

    Vector2 ThrowGrenadeVel( Vector2 throwPos, bool direction)
    {
        Vector2 grenadeVel = new Vector2();
        int facingFactor = direction ? -1 : 1;
        grenadeVel.x = facingFactor * grenadeRange / flyTime; // we don't factor in gravity for X
        // Handles different heights nicely
        grenadeVel.y = (5 - 0.5f * 9.8f * flyTime) / flyTime;
        return grenadeVel;     
    }

    
    
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }

    public bool CanThrow
    {
        get
        {
            return throwCooldown <= 0f;
        }
    }
    
}
