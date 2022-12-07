using Photon.Bolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropBtn : MonoBehaviour
{
	GameObject prefab;
	public int[] centerTofloor;
	private GameObject currObj = null;
	public CanvasGroup[] canvasGroups;
	private static int canvasGroupIdx = 0;
	public int catalogIdx;

	private void Awake()
	{
		if (catalogIdx >= 101)
		{
			GameObject obj = GameObject.Find("Furniture Catalog");
			PrefabCatalog catalog = obj.GetComponent<PrefabCatalog>();
			prefab = catalog.Find(catalogIdx);
		}
	}

	public void BeginDrag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		currObj = Instantiate(prefab);
		currObj.GetComponentInChildren<BoxCollider>().enabled = false;

		if (Physics.Raycast(ray, out hit, 1000f))
			currObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
		else
			currObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
	}

	public void OnDrag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000f))
			currObj.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
		else
			currObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
	}

	public void EndDrag()
	{
		currObj.GetComponentInChildren<BoxCollider>().enabled = true;
		currObj = null;
	}

	public void PrevBtn()
	{
		BtnClick.CanvasGroupOff(canvasGroups[canvasGroupIdx]);
		canvasGroupIdx = (canvasGroupIdx + canvasGroups.Length - 1) % canvasGroups.Length;
		BtnClick.CanvasGroupOn(canvasGroups[canvasGroupIdx]);
	}
	public void NextBtn()
	{
		BtnClick.CanvasGroupOff(canvasGroups[canvasGroupIdx]);
		canvasGroupIdx = (canvasGroupIdx + 1) % canvasGroups.Length;
		BtnClick.CanvasGroupOn(canvasGroups[canvasGroupIdx]);
	}
}
