using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Node : MonoBehaviour {

	public Turn turn;

	public List<Node> connectedNodes;
	public Node jumpNode;

	public TextMesh textMeshUser;
	public Transform textMeshUserTransform;

	public TextMesh textMeshOperation1;
	public Transform textMeshOperation1Transform;

	public TextMesh textMeshOperation2;
	public Transform textMeshOperation2Transform;

	public Transform textMeshAnswerTransform;

	public List<TextMesh> textMeshOperations;

	public Transform cube;

	public List<Link> links;

	public int userTextMax = 10;
	public int operationTextMax = 20;

    public float transitionTime = 1f;

    public Color color;
    private Renderer cubeRenderer;
    public Color selecColor;

	private bool selected = false;
	private bool selectedFirst = false;
	private bool deselect = false;

	private Vector3 originalPosition;
	private Vector3 textMeshLocalPosition;


	public bool Selected {
		get {
			return this.selected;
		}
		set {
			this.selected = value;
			if (this.selected) {
				this.selectedFirst = true;
			} else {
				this.deselect = true;
			}
		}
	}

	void Start() {
		textMeshOperations = new List<TextMesh> ();
//		textMeshUser = GetComponentInChildren<TextMesh> ();

//		textMeshUser.text = turn.user;

		cube = transform.GetChild (0);
		textMeshUserTransform = transform.GetChild (1);
		textMeshUser = textMeshUserTransform.gameObject.GetComponent<TextMesh> ();
		SetText (textMeshUser, turn.user, userTextMax);
        cubeRenderer = cube.GetComponent<Renderer>();
//		Debug.Log (textMeshUser);

		textMeshOperation1Transform = transform.GetChild (2);
		textMeshOperation1 = textMeshOperation1Transform.gameObject.GetComponent<TextMesh> ();
		textMeshOperations.Add (textMeshOperation1);

		textMeshOperation2Transform = transform.GetChild (3);
		textMeshOperation1 = textMeshOperation2Transform.gameObject.GetComponent<TextMesh> ();
		textMeshOperations.Add (textMeshOperation2);

		textMeshLocalPosition = textMeshUserTransform.localPosition;

		textMeshAnswerTransform = transform.GetChild (4);

		int counter = 0;
		foreach (Operation operation in turn.operations) {
			if (operation.action != null && textMeshOperations[counter] != null) {
				SetText(textMeshOperations[counter], operation.action.Substring(9), operationTextMax);
				counter++;
			}

			if (counter >= textMeshOperations.Count) {
				break;
			}
		}
	}

	void Update() {
		originalPosition = transform.localPosition;
		if (selectedFirst) {
			cube.DOScale (new Vector3 (4f, 3f, 1.5f), transitionTime);
			DOTween.To (()=> textMeshUserTransform.localPosition, x=> textMeshUserTransform.localPosition = x, new Vector3 (0f, 1f, -0.76f), transitionTime);
			if (textMeshOperation1Transform != null) {
				DOTween.To (()=> textMeshOperation1Transform.localPosition, x=> textMeshOperation1Transform.localPosition = x, new Vector3 (0f, 0f, -0.76f), transitionTime);
				DOTween.To (()=> textMeshOperation1Transform.localScale, x=> textMeshOperation1Transform.localScale = x, new Vector3 (0.05f, 0.05f, 0.05f), transitionTime);
			}

			if (textMeshOperation2Transform != null) {
				DOTween.To (()=> textMeshOperation2Transform.localPosition, x=> textMeshOperation2Transform.localPosition = x, new Vector3 (0f, -0.5f, -0.76f), transitionTime);
				DOTween.To (()=> textMeshOperation2Transform.localScale, x=> textMeshOperation2Transform.localScale = x, new Vector3 (0.05f, 0.05f, 0.05f), transitionTime);
			}
			DOTween.To (()=> textMeshAnswerTransform.localPosition, x=> textMeshAnswerTransform.localPosition = x, new Vector3 (0f, 0.3f, -0.76f), transitionTime);
			DOTween.To (()=> textMeshAnswerTransform.localScale, x=> textMeshAnswerTransform.localScale = x, new Vector3 (0.05f, 0.05f, 0.05f), transitionTime);
			SetText (textMeshUser, turn.user, 2 * userTextMax);

            SelectColor();

			selectedFirst = false;
		}
		if(deselect) {
			cube.DOScale (new Vector3 (2f, 1f, 1f), transitionTime);
			DOTween.To (() => textMeshUserTransform.localPosition, x => textMeshUserTransform.localPosition = x, new Vector3 (0f, 0f, -0.51f), transitionTime);
			if (textMeshOperation1Transform != null) {
				DOTween.To (()=> textMeshOperation1Transform.localPosition, x=> textMeshOperation1Transform.localPosition = x, new Vector3 (0f, 0f, -0.51f), transitionTime);
				DOTween.To (()=> textMeshOperation1Transform.localScale, x=> textMeshOperation1Transform.localScale = x, new Vector3 (0f, 0f, 0f), transitionTime);
			}
			if (textMeshOperation2Transform != null) {
				DOTween.To (()=> textMeshOperation2Transform.localPosition, x=> textMeshOperation2Transform.localPosition = x, new Vector3 (0f, -0.5f, -0.51f), transitionTime);
				DOTween.To (()=> textMeshOperation2Transform.localScale, x=> textMeshOperation2Transform.localScale = x, new Vector3 (0f, 0f, 0f), transitionTime);
			}
			DOTween.To (()=> textMeshAnswerTransform.localPosition, x=> textMeshAnswerTransform.localPosition = x, new Vector3 (0f, 0.3f, -0.51f), transitionTime);
			DOTween.To (()=> textMeshAnswerTransform.localScale, x=> textMeshAnswerTransform.localScale = x, new Vector3 (0f, 0f, 0f), transitionTime);
			SetText (textMeshUser, turn.user, userTextMax);

            DeselectColor();

            deselect = false;
		}
	}

    void SelectColor() {
        // Color
        cubeRenderer.materials[0].DOColor(selecColor, transitionTime);

        if (jumpNode != null) {
            jumpNode.JumpColor();
        }

        foreach(Node node in connectedNodes) {
            node.SelectColor();
        }

        foreach(Link link in links) {
            link.SelectColor();
        }
    }

    public void DeselectColor() {
        // Color
        cubeRenderer.materials[0].DOColor(color, transitionTime);

        if (jumpNode != null) {
            jumpNode.DeselectJumpColor();
        }

        foreach (Node node in connectedNodes) {
            node.DeselectColor();
        }

        foreach (Link link in links) {
            link.DeselectColor();
        }
    }

    void JumpColor() {
        cubeRenderer.materials[0].DOColor(selecColor, transitionTime);
    }

    void DeselectJumpColor() {
        cubeRenderer.materials[0].DOColor(color, transitionTime);
    }

	private void SetText(TextMesh tm, string text, int max) {
		if (text.Length > max) {
			tm.text = text.Substring (0, max - 3) + "...";
		} else {
			tm.text = text;
		}
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        Selected = true;
    }

    void OnTriggerExit(Collider other)
    {
        Selected = false;
    }
}
