using UnityEngine;
using System.Collections;

public class MenuCamera : MonoBehaviour {

    public Transform[] cameraPositions;
    public Vector2[] moveSpeeds;
    public float[] cameraZoomSpeeds;
    public float[] initalZoom;
    public float timeAtEachPosition = 5f;
    float timer = 0;
    int index = 0;
    int length;
    Camera camera;
	// Use this for initialization
	void Start () {
        length = cameraPositions.Length;
        camera = GetComponent<Camera>();

	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= timeAtEachPosition)
        {
            if(index == length - 1)
                index = -1;
            ++index;
            Vector3 newPosition = new Vector3(cameraPositions [index].position.x, cameraPositions [index].position.y, -10);
            transform.position = newPosition;
            camera.orthographicSize = initalZoom[index];
            timer = 0;
        }
        transform.Translate(moveSpeeds[index]);
        camera.orthographicSize += cameraZoomSpeeds [index];
//        Debug.Log(camera.orthographicSize);
	}
}
