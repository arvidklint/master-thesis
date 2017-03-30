using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torus : MonoBehaviour {

    public Manager manager;

    public bool onTriggerEnter = false;

    public List<SteamController> steamControllers;

    private bool firstClick = false;
    private float diffAngle = 0f;

    public float height = 3f;

    // Use this for initialization
    void Start () {
        
    }

    void Update() {
        transform.localPosition = new Vector3(0f, manager.height + height, 0f);
        transform.localScale = new Vector3(manager.radius, manager.radius, manager.radius);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (onTriggerEnter) {
            foreach(SteamController sc in steamControllers) {
                if(sc.triggerButtonPress)
                {
                    firstClick = true;
                }
                if(sc.triggerButton)
                {
                    Vector3 diff = sc.transform.position - transform.position;
                    float angle = Vector3.Angle(diff, -transform.forward);

                    if (firstClick)
                    {
                        diffAngle = angle - manager.angle;
                        //Debug.Log("manager.angle: " + manager.angle + " Angle: " + angle + " Diff: "+ diffAngle);
                    }
                    firstClick = false;

                    manager.angle = angle - diffAngle;

                    manager.SetHeight(sc.transform.position.y - height);
                }
            }
        }
	}

    void OnTriggerEnter(Collider other) {
        onTriggerEnter = true;
        firstClick = true;
    }

    void OnTriggerExit(Collider other) {
        onTriggerEnter = false;
        firstClick = false;
    }
}
