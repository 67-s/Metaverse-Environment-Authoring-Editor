using Photon.Bolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragAndDropBtnType
{
	None,
	WallSplitDoor,
}

public class DragAndDropBtn : MonoBehaviour
{
	public DragAndDropBtnType currType;
	public GameObject[] prefabs;
	public int[] centerTofloor;
	private GameObject currObj = null;

	public void BeginDrag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000f))
		{
			currObj = Instantiate(prefabs[(int)currType]);
			currObj.transform.position = new Vector3(hit.point.x, hit.point.y + currObj.transform.localScale.y / 2, hit.point.z);
			currObj.GetComponentInChildren<BoxCollider>().enabled = false;
		}
		else
		{
			currObj = Instantiate(prefabs[(int)currType]);
			currObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
			currObj.GetComponentInChildren<BoxCollider>().enabled = false;
		}
	}

	public void OnDrag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000f))
		{
			currObj.transform.position = new Vector3(hit.point.x, hit.point.y + currObj.transform.localScale.y / 2, hit.point.z);
		}
		else
		{
			currObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
		}
	}

	public void EndDrag()
	{
		currObj.GetComponentInChildren<BoxCollider>().enabled = true;
		currObj = null;
	}
}
