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

    public TextMesh textMeshOperation3;
    public Transform textMeshOperation3Transform;

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
    public Color looseEndColor;
    public bool colored = false;

	private bool selected = false;
	public bool selectedFirst = false;
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
                Big();

                SelectColor();
                this.selectedFirst = true;
			} else {
                Small();

                DeselectColor();
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
		textMeshOperation2 = textMeshOperation2Transform.gameObject.GetComponent<TextMesh> ();
		textMeshOperations.Add (textMeshOperation2);

        textMeshOperation3Transform = transform.GetChild(4);
        textMeshOperation3 = textMeshOperation3Transform.gameObject.GetComponent<TextMesh>();
        textMeshOperations.Add(textMeshOperation3);

        textMeshLocalPosition = textMeshUserTransform.localPosition;

		textMeshAnswerTransform = transform.GetChild (5);

		int counter = 0;
        Debug.Log(turn.operations.Count);
		foreach (Operation operation in turn.operations) {
			if (operation.action != null && textMeshOperations[counter] != null) {
                Debug.Log(operation.action);
				SetText(textMeshOperations[counter], operation.action.Substring(9), 100 * operationTextMax);
				counter++;
			}

			//if (counter >= textMeshOperations.Count) {
			//	break;
			//}
		}
	}

	void Update() {
		originalPosition = transform.localPosition;
		if (selectedFirst) {
            

			selectedFirst = false;
		}
		if(deselect) {
            

            deselect = false;
		}
	}

    public void SelectAllSelected()
    {
        if(Selected)
        {
            Selected = true;
            Debug.Log("Reselecting: " + turn.user);
        }
        foreach(Node node in connectedNodes)
        {
            node.SelectAllSelected();
        }
    }

    public void SelectAllLooseEnds()
    {
        if(connectedNodes.Count == 0 && jumpNode == null)
        {
            LooseEndColor();
        }
        foreach(Node node in connectedNodes)
        {
            node.SelectAllLooseEnds();
        }
    }

    void LooseEndColor()
    {
        cubeRenderer.materials[0].DOColor(looseEndColor, transitionTime);
    }

    void Big() {
        
        // Get maximum length in operations
        float maxOperation = 0f;
        foreach(Operation operation in turn.operations)
        {
            if(operation.action != null)
            {
                if(operation.action.Length > maxOperation)
                {
                    maxOperation = operation.action.Length;
                }
            }
        }
        float xScaleOperation = maxOperation / 10f;
        float xScaleUser = turn.user.Length / 5f;

        // check which scale is bigger
        float xScale = xScaleOperation > xScaleUser ? xScaleOperation : xScaleUser;

        xScale = xScale < 3f ? 3f : xScale;

        cube.DOScale(new Vector3(xScale, 3f, 1.5f), transitionTime);
        DOTween.To(() => textMeshUserTransform.localPosition, x => textMeshUserTransform.localPosition = x, new Vector3(0f, 1f, -0.76f), transitionTime);
        if (textMeshOperation1Transform != null) {
            DOTween.To(() => textMeshOperation1Transform.localPosition, x => textMeshOperation1Transform.localPosition = x, new Vector3(0f, 0f, -0.76f), transitionTime);
            DOTween.To(() => textMeshOperation1Transform.localScale, x => textMeshOperation1Transform.localScale = x, new Vector3(0.05f, 0.05f, 0.05f), transitionTime);
        }

        if (textMeshOperation2Transform != null) {
            DOTween.To(() => textMeshOperation2Transform.localPosition, x => textMeshOperation2Transform.localPosition = x, new Vector3(0f, -0.3f, -0.76f), transitionTime);
            DOTween.To(() => textMeshOperation2Transform.localScale, x => textMeshOperation2Transform.localScale = x, new Vector3(0.05f, 0.05f, 0.05f), transitionTime);
        }

        if (textMeshOperation3Transform != null)
        {
            DOTween.To(() => textMeshOperation3Transform.localPosition, x => textMeshOperation3Transform.localPosition = x, new Vector3(0f, -0.6f, -0.76f), transitionTime);
            DOTween.To(() => textMeshOperation3Transform.localScale, x => textMeshOperation3Transform.localScale = x, new Vector3(0.05f, 0.05f, 0.05f), transitionTime);
        }
        DOTween.To(() => textMeshAnswerTransform.localPosition, x => textMeshAnswerTransform.localPosition = x, new Vector3(0f, 0.3f, -0.76f), transitionTime);
        DOTween.To(() => textMeshAnswerTransform.localScale, x => textMeshAnswerTransform.localScale = x, new Vector3(0.05f, 0.05f, 0.05f), transitionTime);
        SetText(textMeshUser, turn.user, 100 * userTextMax);
    }

    public void Small() {
        cube.DOScale(new Vector3(2f, 1f, 1f), transitionTime);
        DOTween.To(() => textMeshUserTransform.localPosition, x => textMeshUserTransform.localPosition = x, new Vector3(0f, 0f, -0.51f), transitionTime);
        if (textMeshOperation1Transform != null) {
            DOTween.To(() => textMeshOperation1Transform.localPosition, x => textMeshOperation1Transform.localPosition = x, new Vector3(0f, 0f, -0.51f), transitionTime);
            DOTween.To(() => textMeshOperation1Transform.localScale, x => textMeshOperation1Transform.localScale = x, new Vector3(0f, 0f, 0f), transitionTime);
        }
        if (textMeshOperation2Transform != null) {
            DOTween.To(() => textMeshOperation2Transform.localPosition, x => textMeshOperation2Transform.localPosition = x, new Vector3(0f, -0.5f, -0.51f), transitionTime);
            DOTween.To(() => textMeshOperation2Transform.localScale, x => textMeshOperation2Transform.localScale = x, new Vector3(0f, 0f, 0f), transitionTime);
        }

        if (textMeshOperation3Transform != null)
        {
            DOTween.To(() => textMeshOperation3Transform.localPosition, x => textMeshOperation3Transform.localPosition = x, new Vector3(0f, -0.5f, -0.51f), transitionTime);
            DOTween.To(() => textMeshOperation3Transform.localScale, x => textMeshOperation3Transform.localScale = x, new Vector3(0f, 0f, 0f), transitionTime);
        }
        DOTween.To(() => textMeshAnswerTransform.localPosition, x => textMeshAnswerTransform.localPosition = x, new Vector3(0f, 0.3f, -0.51f), transitionTime);
        DOTween.To(() => textMeshAnswerTransform.localScale, x => textMeshAnswerTransform.localScale = x, new Vector3(0f, 0f, 0f), transitionTime);
        SetText(textMeshUser, turn.user, userTextMax);
    }

    void SelectColor() {
        Debug.Log("Select color");
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

        if (connectedNodes.Count == 0 && jumpNode == null)
        {
            LooseEndColor();
        }

        colored = true;
    }

    public void DeselectColor() {
        Debug.Log("deselect");
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

        colored = false;
    }

    void JumpColor() {
        cubeRenderer.materials[0].DOColor(selecColor, transitionTime);
    }

    void DeselectJumpColor() {
        cubeRenderer.materials[0].DOColor(color, transitionTime);
    }

	private void SetText(TextMesh tm, string text, int max) {
        if (text.Length > max)
        {
            tm.text = text.Substring(0, max - 3) + "...";
        }
        else
        {
            tm.text = text;
        }
    }
}
