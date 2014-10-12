using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoUIController : MonoBehaviour {

    public Image grenadeImage;
    public Sprite grenadeImg;
    RectTransform rectTransform;
	// Use this for initialization
    public Vector2 fuck;
    RectTransform fuckRect;

    int ammoCount = 0;
    public int ammoCapacity = 5;
    GameObject[] ammoList;
    public float grenadeSpacingX = 40f;
    public Vector2 firstGrenadePosition = new Vector2(37.506f, 347.311f);

	void Start () {
        rectTransform = GetComponent<RectTransform>();
        ammoList = new GameObject[ammoCapacity];
	}
	
	// Update is called once per frame
	void Update () {
        fuck = fuckRect.transform.position;
	}

    public void AddGrenade(){
        if (!AmmoFull)
        {
            GameObject ammoObj = new GameObject();
            ammoObj.AddComponent<Image>();
            Image image = ammoObj.GetComponent<Image>();
            image.sprite = grenadeImg;
            image.gameObject.transform.localScale = new Vector3(.3f, .3f, 0);
//            ammoObj.AddComponent<RectTransform>();
            RectTransform imgRect = ammoObj.GetComponent<RectTransform>();
            imgRect.anchoredPosition = rectTransform.anchoredPosition;
//            Debug.Log(ammoCount);
            if(ammoCount > 0){
                Vector2 lastPosition = ammoList[ammoCount-1].GetComponent<RectTransform>().transform.position;
                ammoObj.transform.position = new Vector2(lastPosition.x + grenadeSpacingX, lastPosition.y);
                ammoList[ammoCount] = ammoObj;
                ++ammoCount;
            }else{
                //First position
                ammoObj.transform.position = firstGrenadePosition;
                ammoList[ammoCount] = ammoObj;
                fuckRect = imgRect;
                ++ammoCount;
            }

            ammoObj.transform.parent = this.transform;
        }
    }

    public void RemoveGrenade(){
        if(!AmmoEmpty){
            GameObject grenade = ammoList[ammoCount-1];
            Destroy(grenade);
            ammoList[ammoCount-1] = null;
            --ammoCount;
        }
    }


    public bool AmmoFull {
        get{
            return ammoCount == 4;
        }
    }

    public bool AmmoEmpty {
        get{
            return ammoCount == 0;
        }
    }
}
