using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

	public List<Action> actions;

	// Use this for initialization
	void Awake () {
		string json;
		json = Loader.Load(Application.dataPath + "/Data/actions.json");
		ActionCollection actionCollection = JsonUtility.FromJson<ActionCollection>(json);
		actions = actionCollection.data;
	}
}
