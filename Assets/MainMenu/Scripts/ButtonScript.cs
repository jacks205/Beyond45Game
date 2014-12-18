using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGameOnClick()
    {
        Debug.Log("Start");
        Application.LoadLevel("Level1");
    }

    public void ContinueOnClick()
    {
        Debug.Log("Continue");
    }

    public void QuitOnClick()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
