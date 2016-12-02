using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required when using event data
using UnityEngine.UI;

public class handleEvent : MonoBehaviour, IEndDragHandler
{
    public ScrollRect scroll;
    private int index = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //Do this when the user stops dragging this UI Element.
    public void OnEndDrag(PointerEventData data)
    {
        // If reached the end of list
        if( scroll.normalizedPosition.y == 0.0f){
            var names = GameObject.Find("Camera").GetComponent<main>().names;
            if (names.Count >= index + 10)
            {
                for (var i = index; i < index + 10; i++)
                {
                    Transform txt = (Transform)Instantiate(GameObject.Find("Camera").GetComponent<main>().listElement, 
                                                            GameObject.Find("Camera").GetComponent<main>().parrent);
                    txt.GetComponent<Text>().text = names[i];
                }
                index = index + 10;
            }
            else
            {
                for (var i = index; i < names.Count; i++)
                {
                    Transform txt = (Transform)Instantiate(GameObject.Find("Camera").GetComponent<main>().listElement,
                                                            GameObject.Find("Camera").GetComponent<main>().parrent);
                    txt.GetComponent<Text>().text = names[i];
                }
                index = names.Count;
            }
        }
    }
}
