using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour
{
    public GameObject ObjectToMove;
    public enum RotationAxes { MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseX;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    //public float minimumX = -360F;
    //public float maximumX = 360F;
    //public float minimumY = -60F;
    //public float maximumY = 60F;

    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;
    public float MoveSpeed = 0.2f;
    string debugtext = "";

    private GameObject manager;

    //....

    void FixedUpdate()
    {

#if UNITY_STANDALONE || UNITY_EDITOR
            bool moving = false;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                ObjectToMove.transform.position += ObjectToMove.transform.forward * MoveSpeed;
                moving = true;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                ObjectToMove.transform.position -= ObjectToMove.transform.forward * MoveSpeed;
                moving = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                ObjectToMove.transform.position -= ObjectToMove.transform.right * MoveSpeed;
                moving = true;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                ObjectToMove.transform.position += ObjectToMove.transform.right * MoveSpeed;
                moving = true;
            }
            if (moving && Input.GetAxis("Mouse X") != 0)
            {


                if (axes == RotationAxes.MouseX)
                {
                    // Read the mouse input axis
                    rotationX += Input.GetAxis("Mouse X") * sensitivityX;
                    Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                    transform.rotation = originalRotation * xQuaternion;
                }
                else if (axes == RotationAxes.MouseY)
                {
                    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    //rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
                    Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.right);
                    ObjectToMove.transform.rotation = originalRotation * yQuaternion;
                }
            }
#endif

#if UNITY_ANDROID
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            ObjectToMove.transform.position += Input.GetTouch(0).deltaPosition.x * ObjectToMove.transform.right * MoveSpeed;
            ObjectToMove.transform.position += Input.GetTouch(0).deltaPosition.y * ObjectToMove.transform.forward * MoveSpeed;

        }
#endif
    }

    void Start()
    {
        // Make the rigid body not change rotation
        originalRotation = ObjectToMove.transform.localRotation;

    }



    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 300, 50), debugtext);

    }

}
