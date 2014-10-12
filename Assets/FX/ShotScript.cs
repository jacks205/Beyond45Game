using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

    public int damage = 1;

    public bool isEnemyShot = false;
    
    void Start()
    {
        // 2 - Limited time to live to avoid any leak
        Destroy(gameObject, 7); // 20sec
    }
}
