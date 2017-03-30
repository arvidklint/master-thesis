using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    public Node parentNode;
    public Manager manager;

    public SteamController[] steamControllers;

    private bool triggerEnter = false;
    private bool keepSelected = false;

	// Use this for initialization
	void Start () {
        parentNode = GetComponentInParent<Node>();
        manager = FindObjectOfType<Manager>();
        Debug.Log(manager);
	}

    void Update() {
        if(steamControllers.Length == 0)
        {
            steamControllers = (SteamController[])FindObjectsOfType(typeof(SteamController));
        }

        if(triggerEnter)
        {
            foreach(SteamController sc in steamControllers)
            {
                if(sc.triggerButtonPress)
                {
                    keepSelected = !keepSelected;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        parentNode.Selected = true;
        triggerEnter = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(!keepSelected)
        {
            parentNode.Selected = false;
            manager.SelectAll();
        }
        triggerEnter = false;
    }
}
