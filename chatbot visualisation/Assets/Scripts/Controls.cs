using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	public GameObject torus;

	public Manager manager;
	
	// Update is called once per frame
	void Update () {
		torus.transform.localScale = new Vector3 (manager.radius, manager.radius, manager.radius);
	}
}
