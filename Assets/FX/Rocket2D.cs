using UnityEngine;
using System.Collections;

public class Rocket2D : Projectile2D {

    public GameObject explosion;
    public RocketExplosion2D explosionCollider;
    bool exploded = false;
    Animator anim;

	// Use this for initialization
	void Start () {
        base.Start();
        anim = GetComponent<Animator>();
        anim.SetBool("isFlying", true);
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    void FixedUpdate(){
        base.FixedUpdate();
    }

    public void Explode()
    {
        if (!exploded)
        {
            explosionCollider.ExplodeCollider();
            PlayExplosionAnim();
            exploded = true;
        }
    }

    void PlayExplosionAnim()
    {
        GameObject explosionAnim = Instantiate(explosion, this.transform.position, new Quaternion(0,0,0,1)) as GameObject;
        Animator explAnim = explosionAnim.GetComponent<Animator>();
        explAnim.SetTrigger("Explode");
        Destroy(explosionAnim, 3f);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
        if (LayerMask.LayerToName(other.gameObject.layer) == "Ground" || LayerMask.LayerToName(other.gameObject.layer) == "Sandbag")
			Explode ();
	}
}
