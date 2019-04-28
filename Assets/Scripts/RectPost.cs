using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RectPost : MonoBehaviour {

    public Vector3 localPos;
    public Rect rect;
	// Use this for initialization
	void Start () {
        localPos = transform.localPosition;
        RawImage img = transform.GetComponent<RawImage>();
        if(img)
        {
            rect = img.GetPixelAdjustedRect();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
