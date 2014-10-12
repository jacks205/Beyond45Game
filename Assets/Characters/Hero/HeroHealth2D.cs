using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroHealth2D : MonoBehaviour {

    public float startingHealth = 100f;
    public float currentHealth;
    public Slider healthSlider;  
    public bool isDead = false;
    public bool godMode;
    public Animator anim;

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
//        Debug.Log ("Hero: " + other.gameObject.name);
        ShotScript shot = other.GetComponent<ShotScript>();
        if(shot != null){
            if(shot.isEnemyShot && !isDead){
                Destroy (other.gameObject);
                TakeDamage(shot.damage);
            }
        }
    }

    void TakeDamage(float damage){
        if(isDead)
            return;
        if (!godMode)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
        }
        if(currentHealth <= 0)
        {
            Death ();
        }
    }

    void Death(){
        isDead = true;
        Debug.Log("DEAD");
        anim.SetBool("isDead", true);
        anim.SetTrigger("Dead");
        
        Destroy (this.gameObject, 5f);
    }
}
