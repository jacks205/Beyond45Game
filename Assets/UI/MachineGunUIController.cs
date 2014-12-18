using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MachineGunUIController : MonoBehaviour {

    public Sprite mgSprite;
    Image image;
    public RectTransform rectTransform;
    float timer;
    public float offsetY = 0f;
    public float offsetX = 380f;
    bool usingMachineGun;
    public Vector3 imageScaling;
    GameObject mgObject;
    public float blinkInterval;
    public float endBlinkTimePercentage = 0;
    float endBlinkInterval = 0;
    float blinkTimer = 0;
    bool iconIsOn;
	// Use this for initialization
	void Start () {
        this.mgObject = new GameObject();
        this.mgObject.transform.parent = this.transform;
        this.mgObject.AddComponent<Image>(); // add image 
        this.image = this.mgObject.GetComponent<Image>(); 
        RemoveIcon();
        RectTransform imgRect = this.mgObject.GetComponent<RectTransform>();//get rectTransform to set anchor
        imgRect.anchoredPosition = this.rectTransform.anchoredPosition; //set anchor of image to the AmmoUI's anchor
        Debug.Log( imgRect.position);
        imgRect.position = new Vector3(imgRect.position.x + offsetX, imgRect.position.y + offsetY,0);
        this.usingMachineGun = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.usingMachineGun && timer > 0)
        {
            if (timer <= this.endBlinkInterval)
            {
                if (this.blinkTimer >= this.blinkInterval)
                {
                    if (this.iconIsOn)
                        RemoveIcon();
                    else
                        ShowIcon();
                    this.blinkTimer = 0;
                }
                this.blinkTimer += Time.deltaTime;
            }
            this.timer -= Time.deltaTime;
        } else if (timer <= 0)
            RemoveIcon();
	}

    void ShowIcon(){
        this.image.sprite = this.mgSprite;
        this.image.gameObject.transform.localScale = imageScaling; //set scaling of grenade
        this.iconIsOn = true;
    }

    void RemoveIcon(){
        this.image.sprite = null;
        this.image.gameObject.transform.localScale = new Vector3(0f, 0f);
        this.iconIsOn = false;
    }

    public void ShowMachineGunIcon(float seconds){
        this.usingMachineGun = true;
        this.timer = seconds;
        endBlinkInterval = timer * endBlinkTimePercentage;
        ShowIcon();
    }
}
