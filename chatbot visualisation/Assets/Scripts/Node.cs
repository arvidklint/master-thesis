using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public Turn turn;

	public List<Node> connectedNodes;
	public Node jumpNode;

	public List<Link> links;

}
