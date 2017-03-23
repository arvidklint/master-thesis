using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public DataManager dm;

	public GameObject nodePrefab;

	public GameObject linkPrefab;

	public List<Node> rootNodes;

	public float radius = 5f;
	public float nodeDistance = 3f;

	// Use this for initialization
	void Start () {
		if (dm.stories.Count == 0) {
			Debug.LogError ("Stories are not loaded");
			return;
		}

		Vector3 rootPosition = Vector3.zero;
		// Create the stories (nodes)
		foreach (Story story in dm.stories) {
			// Create root node for the story
			Node node = CreateNodes (story.turns);
			rootNodes.Add (node);
		}
	}

	void Update() {
		float x = 0f;
		float z = 0f;
		for (int i = 0; i < rootNodes.Count; i++) {
			x = radius * Mathf.Cos ((i * Mathf.PI * 2f) / rootNodes.Count);
			z = radius * Mathf.Sin ((i * Mathf.PI * 2f) / rootNodes.Count);
			rootNodes [i].transform.position = new Vector3 (x, 0f, z);
			rootNodes [i].transform.LookAt (Vector3.zero);
			PlaceNodes (rootNodes [i]);
		}
	}

	void PlaceNodes(Node root) {
		for (int i = 0; i < root.connectedNodes.Count; i++) {
			Vector3 position = Vector3.zero;
			Vector3 middle = Vector3.zero;
			position.x += nodeDistance * Mathf.Sin (((i * Mathf.PI / 2f) / root.connectedNodes.Count) - Mathf.PI / 8f);
			position.y += nodeDistance * Mathf.Cos (((i * Mathf.PI / 2f) / root.connectedNodes.Count) - Mathf.PI / 8f);
			middle.y = position.y + root.transform.position.y;
			root.connectedNodes [i].transform.localPosition = position;
			root.connectedNodes [i].transform.LookAt (middle);

			PlaceNodes (root.connectedNodes[i]);
		}
	}

	Link CreateLink(Node start, Node end) {
		GameObject go = (GameObject)Instantiate (linkPrefab, Vector3.zero, Quaternion.identity);
		Link link = go.GetComponent<Link> ();
		link.Set (start, end);
		return link;
	}

	Node CreateNode(Turn turn) {
		GameObject go = (GameObject)Instantiate(nodePrefab, Vector3.zero, Quaternion.identity);
		Node node = go.GetComponent<Node> ();
		node.turn = turn;
		return node;
	}

	Node CreateNodes(List<Turn> turns) {
		Node rootNode = null;
		Node prevNode = null;
		Node currentNode = null;
		foreach (Turn turn in turns) {
			if (turn.branches.Count != 0) {
				if (currentNode == null)
					continue;
				currentNode.connectedNodes.AddRange (CreateBranchNodes (turn.branches));
				foreach (Node n in currentNode.connectedNodes) {
					n.transform.SetParent (currentNode.transform);
					currentNode.link = CreateLink (currentNode, n);
				}
			} else {
				currentNode = CreateNode (turn);
				if (prevNode == null) {
					rootNode = currentNode;
				} else {
					prevNode.connectedNodes.Add (currentNode);
					currentNode.transform.SetParent (prevNode.transform);
					prevNode.link = CreateLink (prevNode, currentNode);
				}
			}

			prevNode = currentNode;
		}
		return rootNode;
	}

	List<Node> CreateBranchNodes(List<Branch> branches) {
		List<Node> nodes = new List<Node>();
		foreach(Branch branch in branches) {
			nodes.Add (CreateNodes (branch.turns));
		}
		return nodes;
	}
}