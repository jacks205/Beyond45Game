using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroHealth2D : MonoBehaviour {
    
    public float startingHealth = 100f;
    public float currentHealth;
    public Slider healthSlider;  
    public LivesUIController livesController;
    bool dead = false;
    public bool godMode;
    public Animator anim;
    
    public DamageEmitter damageEmitter;
    
    public float backToMenuTimer = 3f;
    
    public int totalLives = 1;
    public float respawnTime = 3f;
    bool respawn = false;
    public Checkpoint[] checkpoints;
    // Use this for initialization
    void Start () {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        livesController.SetTextLives(totalLives);
        Debug.Log(checkpoints.Length);
    }
    
    // Update is called once per frame
    void Update () {
        if (dead)
        {
            backToMenuTimer -= Time.deltaTime;
            if(backToMenuTimer <= 0)
                Application.LoadLevel("MainMenu2");
        }
        if(respawn){
            respawnTime -= Time.deltaTime;
            if(respawnTime <= 0){
                RespawnToLastCheckpoint();
                this.currentHealth = startingHealth;
                healthSlider.value = currentHealth;
                respawn = false;
                anim.SetBool("isDead", false);
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //        Debug.Log ("Hero: " + other.gameObject.name);
        Bullet2D shot = other.GetComponent<Bullet2D>();
        if(shot != null){
            if(shot.isEnemyShot && !dead){
                if (!respawn)
                {
                    Destroy (other.gameObject);
                    TakeDamage(shot.damage);
                    damageEmitter.EmitDamage(transform, shot.damage);
                }
            }
        }
    }
    
    void TakeDamage(float damage){
        if (dead)
            return;
        if (!godMode)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            Death();
        }
        
    }
    
    void RespawnToLastCheckpoint(){
        int recentCheckpointIndex = 0;
        for (int i = 0; i < checkpoints.Length; ++i)
        {
            if(checkpoints[i].isEnabled())
                recentCheckpointIndex = i;
            
        }
        this.transform.position = checkpoints[recentCheckpointIndex].transform.position;
        livesController.SetTextLives(totalLives);
    }
    
    void Death(){
        Debug.Log("DEAD");
        anim.SetBool("isDead", true);
        --totalLives;
        if(totalLives <= 0){
            dead = true;
            Destroy (this.gameObject, 5f);
        }else{
            respawn = true;
        }
        
    }
    
    void ShowExplosion(Transform explodeAnimTransform){
        Transform boom = Instantiate(explodeAnimTransform, this.transform.position, this.transform.rotation) as Transform;
        Animator explodeAnim = boom.GetComponent<Animator>();
        explodeAnim.SetTrigger("Explode");
        Destroy(boom.gameObject, 3f);
    }

    public bool isDead{
        get{ return dead || respawn; }
    }
}
