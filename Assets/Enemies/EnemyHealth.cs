using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public int scoreValue = 10;



	Animator anim;
	public static bool IsDead;


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
		Debug.Log (other.gameObject.name);
        ShotScript shot = other.GetComponent<ShotScript>();
        if(shot != null){
            if(!shot.isEnemyShot && !IsDead){
    			Destroy (other.gameObject);
    			TakeDamage(GunController.BULLET_DAMAGE);
            }
		}
	}

	public void TakeDamage (int amount)
	{
        if(IsDead)
			return;

		currentHealth -= amount;
		if(currentHealth <= 0)
		{
			Death ();
		}
	}

	void Death ()
	{
        IsDead = true;
        anim.SetBool("isDead", true);
        anim.SetTrigger("Dead");

		Destroy (this.gameObject, 4f);
	}
}
