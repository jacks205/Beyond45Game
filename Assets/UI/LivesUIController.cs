using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesUIController : MonoBehaviour {

    public string text =  "Lives: ";
    public Text screenText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetTextLives(int lives){
        screenText.text = text + lives;
    }

}
