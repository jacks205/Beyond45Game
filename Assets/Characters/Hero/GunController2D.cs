using UnityEngine;
using System.Collections;

public class GunController2D : MonoBehaviour {
    public static int BULLET_DAMAGE = 100;
    
    public bool usingController = true;

    public Transform bullet;
    public Transform rocket;
    public float bulletUpperAngle = 45f;
    public float bulletLowerAngle = 45f;

    public Transform grenade;

    public float grenadeRange = 7f;
    
    public float bulletSpeed = 2f;
    public float shootingRate = 0.25f;
    public float rocketRate = 0.12f;
    public float throwingRate = 0.5f;
    public Animator anim;
    float shootCooldown;
    float throwCooldown;
    public float flyTime= 2.0f;
    public HeroHealth2D health;
    HeroController2D heroController2D;

    bool hasRocket = false;
    public float rocketTimer = 7f;
    float rocketCooldown;
    public AmmoUIController ammoUIController;
    public MachineGunUIController machineGunUIController;
    // Use this for initialization
    void Start () {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Grenade"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
        anim = GetComponent<Animator> ();
        heroController2D = GetComponent<HeroController2D>();
        shootCooldown = 0f;
        throwCooldown = 0f;
        rocketCooldown = 0f;
//        health = GetComponent<HeroHealth2D>();


        PickUpGrenade();
        PickUpGrenade();
        PickUpGrenade();
        PickUpGrenade();
        PickUpGrenade();

    }
    
    // Update is called once per frame
    void Update () {
        if (!health.isDead)
        {
            if (shootCooldown > 0)
                shootCooldown -= Time.deltaTime;
            if (throwCooldown > 0)
                throwCooldown -= Time.deltaTime;
            if(hasRocket){
                if(rocketCooldown > 0)
                    rocketCooldown -=Time.deltaTime;
                else
                    SwitchToMG();
            }
            float controllerDegrees = GetControllerAngle();
            CheckToShoot(controllerDegrees); 
            CheckToThrow();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("AmmoBox"))
        {
            if (!ammoUIController.AmmoFull)
            {
                Destroy(other.gameObject);
                PickUpGrenade();
            }
        } else if (other.name.Equals("RocketBox"))
        {
            if (!hasRocket)
            {
                SwitchToRocket();
                machineGunUIController.ShowMachineGunIcon(this.rocketTimer);
            }else{
                rocketCooldown += rocketTimer; //Add additional time with the rocket
            }
            Destroy(other.gameObject);
        }
    }

    void CheckToShoot(float gunRotationDegrees){

        if (CanShoot && isFiring())
        {
            shootCooldown = !hasRocket ? shootingRate : rocketRate;
            // Create a new shot
//            anim.SetBool("isShooting", true);//Show Animation
            Transform shotTransform;
            if(!hasRocket){
                shotTransform = Instantiate(bullet) as Transform;
                anim.Play("shootingMachineGun");
                Bullet2D shot = shotTransform.gameObject.GetComponent<Bullet2D>();
                // Assign position
                shotTransform.position = transform.position;
                // Make the weapon shot always towards it
                Vector2 direction = Vector2.right.Rotate(gunRotationDegrees);
                if (shot != null)
                {
                    shot.damage =  (float)Random.Range(30, 70);
                    if(heroController2D.FacingRight){
                        shotTransform.Rotate(new Vector3(0,0,gunRotationDegrees));
                        shot.direction = direction;
                    }else{
                        //                    SetBulletAngleAndVelocity(shotTransform, move, oppositeDirection, gunRotationDegreesOpposite);
                        Vector2 angle180 = Vector2.right.Rotate(180f);
                        shotTransform.Rotate(new Vector3(0,0,180f));
                        shot.direction = angle180;
                    }
                    
                }
            }
            else{
                shotTransform = Instantiate(rocket) as Transform;
                anim.Play("shootingRocket");
                Rocket2D shot = shotTransform.gameObject.GetComponent<Rocket2D>();
                // Assign position
                shotTransform.position = transform.position;
                // Make the weapon shot always towards it
                Vector2 direction = Vector2.right.Rotate(gunRotationDegrees);
                float gunRotationDegreesOpposite = 180 - gunRotationDegrees;
//                   Vector2 oppositeDirection = Vector2.right.Rotate(gunRotationDegreesOpposite);
                if (shot != null)
                {
                    shot.damage = 50f;
                    if(heroController2D.FacingRight){
                        shotTransform.Rotate(new Vector3(0,0,-gunRotationDegreesOpposite));
                        shot.direction = direction;
                    }else{
                        //                    SetBulletAngleAndVelocity(shotTransform, move, oppositeDirection, gunRotationDegreesOpposite);
                        Vector2 angle180 = Vector2.right.Rotate(180f);
                        shotTransform.Rotate(new Vector3(0,0,0f));
                        shot.direction = angle180;
                    }
                    
                }
            }


        }
    }

//    void SetBulletAngleAndVelocity(Transform obj, Bullet2D move, Vector2 direction, float rotation){
//        obj.Rotate(new Vector3(0,0,rotation));
//        move.direction = direction; // towards in 2D space is the right of the sprite
//    }
     
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
            ammoUIController.RemoveGrenade();
//            Destroy(grenadeTransform.gameObject, 5f);
        }
    }

    void SwitchToRocket()
    {
        anim.SetBool("hasRocket", true);
        hasRocket = true;
        rocketCooldown = rocketTimer;
    }

    void SwitchToMG()
    {
        anim.SetBool("hasRocket", false);
        hasRocket = false;
    }


    bool isFiring(){
        float fire1 = Input.GetAxis ("Fire1");
        if (fire1 == 1)
            return true;
         else
            return false;

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
        grenadeVel.y = (1 - 0.5f * 9.8f * flyTime) / flyTime;
        return grenadeVel;     
    }

    public void PickUpGrenade(){
        ammoUIController.AddGrenade();
    }

    
    
    public bool CanShoot
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
            if(ammoUIController.AmmoEmpty)
                return false;
            else
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