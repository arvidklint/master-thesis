using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {

	public Node startNode;
	public Node endNode;

	LineRenderer lineRenderer;

	void Awake() {
		lineRenderer = GetComponent<LineRenderer> ();
	}

	public void Set(Node start, Node end, Color color) {
		startNode = start;
		endNode = end;

		//lineRenderer = gameObject.AddComponent<LineRenderer> ();

//		lineRenderer.material = new Material (Shader.Find("Standard"));
		lineRenderer.SetColors(color, color);
		lineRenderer.SetWidth(0.1f, 0.1f);
	}

	void Update() {
		if (!startNode || !endNode)
			return;

		lineRenderer.SetPosition(0, startNode.transform.position);
		lineRenderer.SetPosition(1, endNode.transform.position);
	}
}
