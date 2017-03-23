using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {

	public Node startNode;
	public Node endNode;

	public Color color;

	LineRenderer lineRenderer;

	public void Set(Node start, Node end) {
		startNode = start;
		endNode = end;

		lineRenderer = gameObject.AddComponent<LineRenderer> ();

		lineRenderer.material = new Material (Shader.Find("Self-Illumin/Diffuse"));
		lineRenderer.SetColors(color, color);
		lineRenderer.SetWidth(0.1f, 0.1f);
	}

	public static void DrawLine(Vector3 start, Vector3 end, Color color) {
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(color, color);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
	}

	void Update() {
		if (!startNode || !endNode)
			return;

		lineRenderer.SetPosition(0, startNode.transform.position);
		lineRenderer.SetPosition(1, endNode.transform.position);
	}
}
