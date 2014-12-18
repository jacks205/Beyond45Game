using UnityEngine;
using System.Collections;

public class Bullet2D : Projectile2D {

    public AudioSource bulletSound;
    public AudioSource tankSound;
    Vector3 tankShotSize = new Vector3(4,4,0);

	// Use this for initialization
	void Start () {
		base.Start ();
        if (this.transform.localScale.Equals(tankShotSize))
        {
            tankSound.Play();
        } else
        {
            bulletSound.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	void FixedUpdate()
    {
		base.FixedUpdate ();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
//        Debug.Log(other.name);
        if((other.gameObject.name.StartsWith("Sandbag") || other.gameObject.name.StartsWith("Box")) && !isEnemyShot){
            Destroy(this.gameObject);
        }
    }
}

