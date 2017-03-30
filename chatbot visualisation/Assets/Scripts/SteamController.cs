using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamController : MonoBehaviour {

    private Valve.VR.EVRButtonId triggerButtonId = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    public bool triggerButton = false;
    public bool triggerButtonPress = false;

	// Use this for initialization
	void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        triggerButton = device.GetPress(triggerButtonId) ? true : false;
        triggerButtonPress = device.GetPressDown(triggerButtonId) ? true : false;
	}
}
