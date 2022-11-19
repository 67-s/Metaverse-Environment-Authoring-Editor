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
	private Boolean dragFlag = false;
	private GameObject currObj = null;

	public void BeginDrag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000f))
		{
			Vector3 hitPos = hit.point;
			hitPos.y = 0.3f;

			currObj = Instantiate(prefabs[(int)currType]);
			currObj.transform.position = hitPos;
			currObj.GetComponentInChildren<BoxCollider>().enabled = false;
		}
	}

	public void OnDrag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000f))
		{
			Vector3 hitPos = hit.point;
			hitPos.y = 0.3f;

			currObj.transform.position = hitPos;
		}
	}

	public void EndDrag()
	{
		currObj.GetComponentInChildren<BoxCollider>().enabled = true;
	}
}
