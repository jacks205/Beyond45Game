using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public int scoreValue = 10;


	Animator anim;
	bool isDead;


	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log (other.gameObject.name);
		if (other.gameObject.name == "bullet(Clone)") {
			Destroy (other.gameObject);
			TakeDamage(GunController.BULLET_DAMAGE);
		}
	}

	public void TakeDamage (int amount)
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

		anim.SetTrigger ("Dead");

		Destroy (gameObject, 2f);
	}
}
