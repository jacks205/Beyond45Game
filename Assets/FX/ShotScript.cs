using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

    public float damage = 25f;

    public bool isEnemyShot = false;
    
    void Start()
    {
        // 2 - Limited time to live to avoid any leak
        Destroy(gameObject, 7); // 20sec
    }
}
