using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public bool selected;
	public bool deselect;
	public Node node;

	// Use this for initialization
	void Start () {
		node = GetComponent<Node> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (selected) {
			node.Selected = true;
			selected = false;
		}
		if (deselect) {
			node.Selected = false;
			deselect = false;
		}
	}
}
