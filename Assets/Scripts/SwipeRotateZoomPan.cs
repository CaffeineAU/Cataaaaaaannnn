using UnityEngine;
using System.Collections;

public class SwipeRotateZoomPan : MonoBehaviour
{

    public float RotateSpeed = 0.1f;
    public Vector3 RotateAxis = Vector3.up;
    public int PinchSpeed = 2;
    public Camera selectedCamera;
    public float MinZoom = 90f;
    public float MaxZoom = 25f;
    public float DefaultZoom = 30f;
    public float DeadZone = 0.75f;
    public float varianceInDistances = 5.0F;
#if UNITY_ANDROID
    private float touchDelta = 0.0F;
    private Vector2 prevDist = new Vector2(0, 0);
    private Vector2 curDist = new Vector2(0, 0);
#endif
    //private float startAngleBetweenTouches = 0.0F;
    //private int vertOrHorzOrientation = 0; //this tells if the two fingers to each other are oriented horizontally or vertically, 1 for vertical and -1 for horizontal
    //private Vector2 midPoint = new Vector2(0, 0); //store and use midpoint to check if fingers exceed a limit defined by midpoint for oriantation of fingers
    Vector3 ClickPos = Vector3.zero;

    string debugtext = "";

    // Use this for initialization
    void Start()
    {

    }

    //float xpos = 0;
    //float deltax = 0;
    //[System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 300, 50), debugtext);

    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_ANDROID
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float ypos = Input.GetTouch(0).position.y;
            float deltax = Input.GetTouch(0).deltaPosition.x;
            if (deltax > DeadZone || deltax < -DeadZone)
            {
                transform.RotateAround(Vector3.zero, RotateAxis, RotateSpeed * (ypos > Screen.height / 2 ? -deltax : deltax));
            }
        }


        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {


            //midPoint = new Vector2(((Input.GetTouch(0).position.x + Input.GetTouch(1).position.x) / 2), ((Input.GetTouch(0).position.y - Input.GetTouch(1).position.y) / 2)); //store midpoint from first touches
            curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; //current distance between finger touches
            prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)); //difference in previous locations using delta positions
            touchDelta = curDist.magnitude - prevDist.magnitude;

            //if ((Input.GetTouch(0).position.x - Input.GetTouch(1).position.x) > (Input.GetTouch(0).position.y - Input.GetTouch(1).position.y))
            //{
            //    vertOrHorzOrientation = -1;
            //}
            //if ((Input.GetTouch(0).position.x - Input.GetTouch(1).position.x) < (Input.GetTouch(0).position.y - Input.GetTouch(1).position.y))
            //{
            //    vertOrHorzOrientation = 1;
            //}

            if ((touchDelta < -DeadZone)) //
            {
                selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView + (1 * PinchSpeed), MaxZoom, MinZoom);
            }


            if ((touchDelta > DeadZone))

            {
                selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView - (1 * PinchSpeed), MaxZoom, MinZoom);
            }

        }
#endif

#if UNITY_STANDALONE || UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            ClickPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && (Input.GetAxis("Mouse X") != 0))
        {
            transform.RotateAround(Vector3.zero, RotateAxis, RotateSpeed * 5 * (ClickPos.y > Screen.height / 2 ? -Input.GetAxis("Mouse X") : Input.GetAxis("Mouse X")));
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView - 20*Input.GetAxis("Mouse ScrollWheel"), MaxZoom, MinZoom);
        }
#endif



#if UNITY_ANDROID
        if (Input.touchCount == 1 && ((Input.touches[0].tapCount == 2)))
#endif
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
#endif
        {
            selectedCamera.fieldOfView = DefaultZoom;
        }
    }
}


