using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabObj : MonoBehaviour
{
	Material outline;
	Renderer renderers;
	List<Material> materialList = new List<Material>();

	void Start()
	{
		outline = new Material(Shader.Find("Outlined/NewOcclusionOutline"));
	}

	private void OnMouseDown()
	{
		renderers = this.GetComponent<Renderer>();

		materialList.Clear();
		materialList.AddRange(renderers.sharedMaterials);
		materialList.Add(outline);

		renderers.materials = materialList.ToArray();
		gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
	}

	private void OnMouseUp()
	{
		Renderer renderer = this.GetComponent<Renderer>();

		materialList.Clear();
		materialList.AddRange(renderer.sharedMaterials);
		materialList.Remove(outline);

		renderer.materials = materialList.ToArray();
		gameObject.GetComponentInChildren<BoxCollider>().enabled = true;
	}

	public void OnMouseDrag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000f))
			gameObject.transform.position = new Vector3(hit.point.x, hit.point.y + gameObject.transform.localScale.y / 2, hit.point.z);
		else
			gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
	}
}
