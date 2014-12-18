using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public float startingHealth = 100;
	public float currentHealth;
	public int scoreValue = 10;
    public DamageEmitter damageEmitter;

	Animator anim;
	public bool isDead;

    public GameObject hitBox;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if (!isDead)
        {
            if (other.name.StartsWith("bullet"))
            {
                Bullet2D shot = other.GetComponent<Bullet2D>();
                if (shot != null)
                {
                    if (!shot.isEnemyShot && !isDead)
                    {
                        Destroy(other.gameObject);
                        TakeDamage(shot.damage);
                        damageEmitter.EmitDamage(this.transform, shot.damage);
                    }
                }
            } else if (other.name.StartsWith("rocket"))
            {
                Rocket2D shot = other.GetComponent<Rocket2D>();
                if (shot != null)
                {
                    if (!shot.isEnemyShot && !isDead)
                    {
                        shot.Explode();
                        TakeDamage(shot.damage);
                        damageEmitter.EmitDamage(this.transform, shot.damage);
                    }
                }
            } else if (other.name.StartsWith("explosion"))
            {
                GrenadeScript g = other.GetComponent<GrenadeScript>();
                if (g != null)
                    damageEmitter.EmitDamage(this.transform, g.explosionDamage);
                else
                {
                    RocketExplosion2D r = other.GetComponent<RocketExplosion2D>();
                    if (r != null)
                        damageEmitter.EmitDamage(this.transform, r.explosionDamage);
                }
            }
        }
	}

    public void TakeDamage (float amount)
	{
        if(isDead)
			return;

		currentHealth -= amount;
		if(currentHealth <= 0)
		{
			Death ();
		}
	}

	void Death ()
	{
        isDead = true;
        anim.SetBool("isDead", true);
        anim.SetTrigger("Dead");
        rigidbody2D.fixedAngle = false;
        if (GetComponent<EnemyController>().isTank)
        {
            Destroy(hitBox);
        }
//        rigidbody2D.isKinematic = true;

		Destroy (this.gameObject, 4f);
	}
}
