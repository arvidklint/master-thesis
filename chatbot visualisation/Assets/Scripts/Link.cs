using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {

	public Node startNode;
	public Node endNode;

	LineRenderer lineRenderer;

    private Color color;
    public Color selectColor;

	void Awake() {
		lineRenderer = GetComponent<LineRenderer> ();
	}

	public void Set(Node start, Node end, Color color) {
        this.color = color;

		startNode = start;
		endNode = end;

		//lineRenderer = gameObject.AddComponent<LineRenderer> ();

//		lineRenderer.material = new Material (Shader.Find("Standard"));
		lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
	}

    public void SelectColor() {
        lineRenderer.startColor = selectColor;
        lineRenderer.endColor = selectColor;
    }

    public void DeselectColor() {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

	void Update() {
		if (!startNode || !endNode)
			return;

		lineRenderer.SetPosition(0, startNode.transform.position);
		lineRenderer.SetPosition(1, endNode.transform.position);
	}
}
