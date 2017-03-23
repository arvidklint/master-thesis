using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Operation {
	public string action;
	public string jump;
}

[System.Serializable]
public class Turn {
	public string user;
	public List<Entity> entities;
	public List<Operation> operations;
	public List<Branch> branches;
}

[System.Serializable]
public class Branch {
	public List<Turn> turns;
}


[System.Serializable]
public class Story {
	public string name;
	public List<Turn> turns;
}

[System.Serializable]
public class StoryCollection {
	public List<Story> data;

	public static List<Story> CreateStoryList(string json) {
		StoryCollection sc = JsonUtility.FromJson<StoryCollection> (json);
		return sc.data;
	}
}
