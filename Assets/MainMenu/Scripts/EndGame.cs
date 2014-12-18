using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other){
        if (other != null)
        {
            if(other.transform.parent != null){
            if (other.transform.parent.name.StartsWith("Hero"))
                switch(Application.loadedLevelName)
                {
                    case "Level1":
                        Application.LoadLevel("Level2");
                        break;
                    case "Level2":
                        Application.LoadLevel("MainMenu2");
                        break;
                }
            }
        }
     }
}
