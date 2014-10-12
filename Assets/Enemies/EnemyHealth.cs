using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public float startingHealth = 100;
	public float currentHealth;
	public int scoreValue = 10;



	Animator anim;
	public bool isDead;


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
        ShotScript shot = other.GetComponent<ShotScript>();
        if(shot != null){
            if(!shot.isEnemyShot && !isDead){
    			Destroy (other.gameObject);
    			TakeDamage(shot.damage);
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

		Destroy (this.gameObject, 4f);
	}
}
