using UnityEngine;
using System.Collections;

public class DamageEmitter : MonoBehaviour {


    public float floatDuration = 0.5f;
    public float speed = 0.3f;
    public Vector3 offset  = new Vector3(0.35f, 0.3f, 0f);
    public Vector2 xSpread = new Vector2(-0.085f, 0.085f);
    public void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
    }

	public void EmitDamage(Transform obj, float damage){
        float xSwayOffset = Random.Range(xSpread.x, xSpread.y);
        Vector3 newPos = new Vector3(obj.transform.position.x + offset.x + xSwayOffset, 
                                     obj.transform.position.y + offset.y, 
                                     obj.transform.position.z + offset.z);
        GameObject damageObj = Instantiate(this.gameObject, newPos, obj.rotation) as GameObject;
        damageObj.GetComponentInChildren<TextMesh>().text = damage.ToString();
        Destroy(damageObj, floatDuration);
    }
     

}
