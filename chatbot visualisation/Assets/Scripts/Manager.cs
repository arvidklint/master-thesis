using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public DataManager dm;


	public GameObject actionPrefab;

	// Use this for initialization
	void Start () {
		// Create actions
		if (dm.actions.Count == 0) {
			Debug.LogError ("Actions are not loaded");
			return;
		}

		Vector3 currentPosition = Vector3.zero;
		foreach (Action a in dm.actions) {
			GameObject go = (GameObject)Instantiate (actionPrefab, currentPosition, Quaternion.identity);
			go.GetComponent<ActionVisualiser> ().action = a;
			Vector3 start = currentPosition;
			currentPosition.x += 2;
			currentPosition.y += 2;
			Vector3 end = currentPosition;
			Line.DrawLine(start, end, new Color(1, 1, 1, 1));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
