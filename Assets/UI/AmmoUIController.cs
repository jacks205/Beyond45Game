using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoUIController : MonoBehaviour {
    
    public Sprite grenadeSprite;
    public RectTransform rectTransform;
    
    
    int ammoCount = 0; //Total grenades current in inventory
    public int ammoCapacity = 5; //Total amount of grenades a player can have
    GameObject[] ammoList = null; //Array of grenade GameObjects for keeping track of on screen grenades and inventory grenades + 
    public float grenadeSpacingX = 40f; //Spacing between UI grenades
    public float offsetY = -50f;
    public float offsetX = 200f;
    public Vector2 firstGrenadePosition = new Vector2(37.506f, 347.311f); //Guesstimate of where the first grenade image should go (Needs to be dynamic) 
    
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }
    
    public void AddGrenade(){
        //Something that should be in Start(), but this fixed a null pointer issue for some reason
        if(ammoList == null)
            ammoList = new GameObject[ammoCapacity];
        
        //If ammo is not full
        if (!AmmoFull)
        {
            //Create ammo ui object and place on screen with grenade list
            GameObject ammoObj = new GameObject();
            ammoObj.transform.parent = this.transform;
            ammoObj.AddComponent<Image>(); // add image 
            Image image = ammoObj.GetComponent<Image>(); 
            image.sprite = grenadeSprite; //set sprite to grenade
            image.gameObject.transform.localScale = new Vector3(.3f, .3f, 0); //set scaling of grenade
            RectTransform imgRect = ammoObj.GetComponent<RectTransform>();//get rectTransform to set anchor
            imgRect.anchoredPosition = rectTransform.anchoredPosition; //set anchor of image to the AmmoUI's anchor
            Debug.Log( imgRect.position);
            imgRect.position = new Vector3(imgRect.position.x + offsetX, imgRect.position.y + offsetY,0);
            //Logic for grenade 1 and 2-5
            if(ammoCount > 0){
                Vector2 lastPosition = ammoList[ammoCount-1].GetComponent<RectTransform>().transform.position;
                ammoObj.transform.position = new Vector2(lastPosition.x + grenadeSpacingX, lastPosition.y); //add spacing from left grenade
                ammoList[ammoCount] = ammoObj;
                ++ammoCount;
            }else{
                //First position
                ammoList[ammoCount] = ammoObj;
                ++ammoCount;
            }
            
            //            ammoObj.transform.parent = this.transform;
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
