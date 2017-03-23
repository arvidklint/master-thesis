using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

	public List<Action> actions;
	public List<Expression> expressions;
	public List<Story> stories;

	// Use this for initialization
	void Awake () {
		string json;

		// Load actions
		json = Loader.Load(Application.dataPath + "/Data/actions.json");
		ActionCollection actionCollection = JsonUtility.FromJson<ActionCollection>(json);
		actions = actionCollection.data;

		// Load expressions
		json = Loader.Load(Application.dataPath + "/Data/expressions.json");
		expressions = ExpressionCollection.CreateExpressionList (json);

		// Load stories
		json = Loader.Load(Application.dataPath + "/Data/stories.json");
		stories = StoryCollection.CreateStoryList (json);
	}
}
