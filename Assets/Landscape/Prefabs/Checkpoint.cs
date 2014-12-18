using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public Sprite lightSignSprite;
    public GameObject light;
    public int numberOfCheckpoint;
    bool enabled = false;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled)
        {
            Debug.Log(LayerMask.LayerToName(other.gameObject.layer));
            if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
            {
                Debug.Log(light.GetComponent<SpriteRenderer>().gameObject.name);
                light.GetComponent<SpriteRenderer>().sprite = lightSignSprite;
                enabled = true;
            }
        }
    }

    public bool isEnabled(){
        return this.enabled;
    }
}
