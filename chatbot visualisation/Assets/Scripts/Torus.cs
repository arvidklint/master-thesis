using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torus : MonoBehaviour {

    public Manager manager;

    public bool onTriggerEnter = false;

    public List<SteamController> steamControllers;

    private bool firstClick = false;
    private float diffAngle = 0f;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (onTriggerEnter) {
            foreach(SteamController sc in steamControllers) {
                if(sc.triggerButton)
                {
                    Vector3 diff = sc.transform.position - transform.position;
                    float angle = Vector3.Angle(diff, -transform.forward);

                    if (firstClick)
                    {
                        diffAngle = angle - manager.angle;
                    }
                    firstClick = false;

                    manager.angle = angle + diffAngle;
                }
            }
        }
	}

    void OnTriggerEnter(Collider other) {
        onTriggerEnter = true;
        firstClick = true;
        Debug.Log("Enter");
    }

    void OnTriggerExit(Collider other) {
        onTriggerEnter = false;
        firstClick = false;
        Debug.Log("Exit");
    }
}
