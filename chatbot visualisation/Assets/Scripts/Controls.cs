using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	public GameObject torus;

	public Manager manager;
	
    void Start() {
        torus.transform.localPosition = new Vector3(0f, manager.height + 4f, 0f);
    }

	// Update is called once per frame
	void Update () {
		torus.transform.localScale = new Vector3 (manager.radius, manager.radius, manager.radius);
	}
}
