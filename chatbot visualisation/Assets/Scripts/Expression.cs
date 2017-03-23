using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity {
	public string entity;
	public string value;
	public int start;
	public int end;
}

[System.Serializable]
public class Expression {
	public string text;
	public List<Entity> entities;
}

[System.Serializable]
public class ExpressionCollection {
	public List<Expression> data;

	public static List<Expression> CreateExpressionList(string json) {
		ExpressionCollection ec = JsonUtility.FromJson<ExpressionCollection> (json);
		return ec.data;
	}
}
