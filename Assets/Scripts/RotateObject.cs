using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {

    public int RotateSpeed = 10;
    public Vector3 RotateAxis = Vector3.up;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, RotateAxis, RotateSpeed * Time.deltaTime);
	}
}
