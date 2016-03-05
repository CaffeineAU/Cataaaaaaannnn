using UnityEngine;
using System.Collections;

namespace Completed

{

    public class CatanTile : MonoBehaviour
    {

        public BoardManager.TileType Type;
        public Quaternion Rotation = Quaternion.AngleAxis(30, Vector3.up);
        public Vector3 Centre;
        public float Width;
        public float Height;
        public float ViewingAngle;



        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}