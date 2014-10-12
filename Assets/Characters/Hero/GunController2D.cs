using UnityEngine;
using System.Collections;

public class GunController2D : MonoBehaviour {
    public static int BULLET_DAMAGE = 100;
    
    public bool usingController = true;

    public Transform bullet;
    public float bulletUpperAngle = 45f;
    public float bulletLowerAngle = 45f;

    public Transform grenade;

    public float grenadeRange = 20f;
    
    public float bulletSpeed = 2f;
    public float shootingRate = 0.25f;
    public float throwingRate = 0.5f;
    public Animator anim;
    float shootCooldown;
    float throwCooldown;
    public float flyTime= 2.0f;
    public HeroHealth2D health;
    HeroController2D heroController2D;
    // Use this for initialization
    void Start () {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Grenade"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
        anim = GetComponent<Animator> ();
        heroController2D = GetComponent<HeroController2D>();
        shootCooldown = 0f;
        throwCooldown = 0f;
//        health = GetComponent<HeroHealth2D>();
//        anim.SetBool("hasRocket", true);
    }
    
    // Update is called once per frame
    void Update () {
        if (!health.isDead)
        {
            if (shootCooldown > 0)
                shootCooldown -= Time.deltaTime;
            if (throwCooldown > 0)
                throwCooldown -= Time.deltaTime;
            float controllerDegrees = GetControllerAngle();
            CheckToShoot(controllerDegrees); 
            CheckToThrow();
        }
    }

    void CheckToShoot(float gunRotationDegrees){

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
                shot.damage = 50f;
            }

            // Make the weapon shot always towards it
            BulletMove move = shotTransform.gameObject.GetComponent<BulletMove>();
            Vector2 direction = Vector2.right.Rotate(gunRotationDegrees);
//            float gunRotationDegreesOpposite = 180 - gunRotationDegrees;
//            Vector2 oppositeDirection = Vector2.right.Rotate(gunRotationDegreesOpposite);
            if (move != null)
            {
                if(heroController2D.FacingRight){
                    SetBulletAngleAndVelocity(shotTransform, move, direction, gunRotationDegrees);
                }else{
//                    SetBulletAngleAndVelocity(shotTransform, move, oppositeDirection, gunRotationDegreesOpposite);
                    Vector2 angle180 = Vector2.right.Rotate(180f);
                    SetBulletAngleAndVelocity(shotTransform, move, angle180, 180f);
                }
                    
            }
        }
    }

    void SetBulletAngleAndVelocity(Transform obj, BulletMove move, Vector2 direction, float rotation){
        obj.Rotate(new Vector3(0,0,rotation));
        move.direction = direction; // towards in 2D space is the right of the sprite
    }
     
    float GetControllerAngle(){
        float joystick4thAxis = Input.GetAxis ("Mouse X") * 10;
        float joystick5thAxis = Input.GetAxis ("Mouse Y") * 10;
//        Debug.Log("X: " + joystick4thAxis);
//        Debug.Log("Y: " + joystick5thAxis);
        if (joystick4thAxis <= 0.09 && joystick5thAxis <= 0.09) {
            return 0;
        } else if ((joystick4thAxis <= 0.8 && joystick4thAxis >= -.3) && (joystick5thAxis <= -0.5 && joystick5thAxis >= -1)) {
            return bulletUpperAngle;
        } else if ((joystick4thAxis <= 0.8 && joystick4thAxis >= -.3) && (joystick5thAxis >= 0.5 && joystick5thAxis <= 1)) {
            return -bulletLowerAngle;
        } 
        return 0;
    }


    void CheckToThrow(){
        if (CanThrow && isThrowingGrenade())
        {
            throwCooldown = throwingRate;
            Transform grenadeTransform = Instantiate(grenade) as Transform;

            grenadeTransform.position = this.transform.position;
            grenadeTransform.rigidbody2D.velocity = -ThrowGrenadeVel(transform.position, heroController2D.FacingRight);
            Destroy(grenadeTransform.gameObject, 5f);
        }
    }


    bool isFiring(){
        float fire1 = Input.GetAxis ("Fire1");
        if (fire1 == 1)
        {
            anim.SetBool("isShooting", true);//Show Animation
            return true;
        } else
        {
            anim.SetBool("isShooting", false);//Show Animation
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
        grenadeVel.y = (2 - 0.5f * 9.8f * flyTime) / flyTime;
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

public static class Vector2Extension {
    
    public static Vector2 Rotate(this Vector2 v, float degrees) {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
        
        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}