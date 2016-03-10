using UnityEngine;
using System.Collections;

public class MouseOver : MonoBehaviour {

        public Renderer rend;
    private Color originalColour;
        void Start()
        {
            rend = GetComponent<Renderer>();
            originalColour = rend.material.color;
        }
        void OnMouseEnter()
        {
            rend.material.color = Color.red;
        }
        void OnMouseOver()
        {
            rend.material.color -= new Color(1F, 0, 0) * Time.deltaTime;
        }
        void OnMouseExit()
        {
            rend.material.color = originalColour;
        }

	// Update is called once per frame
	void Update () {
	
	}
}
