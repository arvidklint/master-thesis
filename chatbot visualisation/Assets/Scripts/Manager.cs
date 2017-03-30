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

	public Color linkColor;
	public Color jumpLinkColor;

    public float height = 10f;
    public float angle = 0f;

    public bool deselectAll = false;

	// Use this for initialization
	void Start () {
		if (dm.stories.Count == 0) {
			Debug.LogError ("Stories are not loaded");
			return;
		}

		Vector3 rootPosition = Vector3.zero;
		// Create the nodes and links (stories)
		foreach (Story story in dm.stories) {
			// Create root node
			Node node = CreateNodes (story.turns);
			rootNodes.Add (node);
			node.transform.SetParent (this.transform);
		}

		// Create the links between stories
		 CreateLinkBetweenStories(rootNodes);
	}

	void Update() {
		float x = 0f;
		float z = 0f;
		for (int i = 0; i < rootNodes.Count; i++) {
			x = radius * Mathf.Cos ((i * Mathf.PI * 2f + angle) / rootNodes.Count);
			z = radius * Mathf.Sin ((i * Mathf.PI * 2f + angle) / rootNodes.Count);
			rootNodes [i].transform.position = new Vector3 (x, height, z);
			rootNodes [i].transform.LookAt (new Vector3(0f, height, 0f));
			PlaceNodes (rootNodes [i]);
		}

        if(deselectAll) {
            DeselectAll();
            deselectAll = false;
        }
	}

    public void SelectAll()
    {
        foreach(Node node in rootNodes)
        {
            node.SelectAll();
        }
    }

    public void SetHeight(float h)
    {
        height = h;
    }

    void DeselectAll() {
        foreach(Node node in rootNodes) {
            node.DeselectColor();
        }
    }

	Node FindBookmark(List<Node> nodes, string bookmark) {
		Node foundNode = null;
		foreach (Node node in nodes) {
			foreach (Operation operation in node.turn.operations) {
				if (operation.bookmark == bookmark) {
					return node;
				}
			}
			Node temp = FindBookmark (node.connectedNodes, bookmark);
			if (temp != null) {
				foundNode = temp;
			}
		}
		return foundNode;
	}

	void PlaceNodes(Node root) {
		for (int i = 0; i < root.connectedNodes.Count; i++) {
			Vector3 position = Vector3.zero;
			Vector3 middle = Vector3.zero;

			position.y = -nodeDistance;

			float angle = i * 2.5f * Mathf.PI * 2f - (root.connectedNodes.Count - 1) * Mathf.PI * 3f;


			middle.y = position.y + root.transform.position.y;
			root.connectedNodes[i].transform.localPosition = position;
			root.connectedNodes[i].transform.LookAt (middle);
			root.connectedNodes[i].transform.RotateAround(middle, Vector3.up, angle);

			PlaceNodes (root.connectedNodes[i]);
		}
	}

	void CreateLinkBetweenStories(List<Node> nodes) {
		foreach (Node node in nodes) {
			foreach (Operation operation in node.turn.operations) {
				if (operation.jump != null) {
					Node other = FindBookmark (rootNodes, operation.jump);
					if (other != null) {
						Link link = CreateLink(node, other, jumpLinkColor);
						node.jumpNode = other;
						node.links.Add(link);
					}

				}
			}
			if (node.connectedNodes.Count > 0) {
				CreateLinkBetweenStories (node.connectedNodes);
			}
		}
	}

	Link CreateLink(Node start, Node end, Color color) {
		GameObject go = (GameObject)Instantiate (linkPrefab, Vector3.zero, Quaternion.identity);
		Link link = go.GetComponent<Link> ();
		link.Set (start, end, color);
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
					currentNode.links.Add (CreateLink (currentNode, n, linkColor));
				}
			} else {
				currentNode = CreateNode (turn);
				if (prevNode == null) {
					rootNode = currentNode;
				} else {
					prevNode.connectedNodes.Add (currentNode);
					currentNode.transform.SetParent (prevNode.transform);
					prevNode.links.Add (CreateLink (prevNode, currentNode, linkColor));
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