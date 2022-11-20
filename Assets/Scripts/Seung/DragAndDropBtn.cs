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
	int i = 0;
	public void BeginDrag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		currObj = Instantiate(prefabs[(int)currType]);
		currObj.name += i++;
		currObj.GetComponentInChildren<BoxCollider>().enabled = false;

		if (Physics.Raycast(ray, out hit, 1000f))
			currObj.transform.position = new Vector3(hit.point.x, hit.point.y + currObj.transform.localScale.y / 2, hit.point.z);
		else
			currObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
	}

	public void OnDrag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000f))
			currObj.transform.position = new Vector3(hit.point.x, hit.point.y + currObj.transform.localScale.y / 2, hit.point.z);
		else
			currObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
	}

	public void EndDrag()
	{
		currObj.GetComponentInChildren<BoxCollider>().enabled = true;
		currObj = null;
	}
}
