using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public int scoreValue = 10;


	public Animator anim;
	bool isDead;


	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
//        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log (other.gameObject.name);
		if ((other.gameObject.name == "mgBullet(Clone)") && !isDead) {
			Destroy (other.gameObject);
			TakeDamage(GunController.BULLET_DAMAGE);
		}
	}

	public void TakeDamage (int amount)
	{
		if(isDead)
			return;

		currentHealth -= amount;
        Debug.Log(currentHealth);
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

		Destroy (this.gameObject, 4f);
	}
}
