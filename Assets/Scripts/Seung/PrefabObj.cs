using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		if (!ColorBtn.colorFlag)
		{
			renderers = this.GetComponent<Renderer>();

			materialList.Clear();
			materialList.AddRange(renderers.sharedMaterials);
			materialList.Add(outline);

			renderers.materials = materialList.ToArray();
			gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
		}
		else
		{
			renderers = this.GetComponent<Renderer>();

			materialList.Clear();
			materialList.Add(renderers.sharedMaterials.ToArray()[0]);
			materialList.Add(ColorBtn.colorMaterial);

			renderers.materials = materialList.ToArray();
		}
	}

	private void OnMouseUp()
	{
		if (!ColorBtn.colorFlag)
		{
			Renderer renderer = this.GetComponent<Renderer>();

			materialList.Clear();
			materialList.AddRange(renderer.sharedMaterials);
			materialList.Remove(outline);

			renderer.materials = materialList.ToArray();
			gameObject.GetComponentInChildren<BoxCollider>().enabled = true;
		}
		else
		{
			ColorBtn.colorFlag = false;
		}
	}

	public void OnMouseDrag()
	{
		if (!ColorBtn.colorFlag)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 1000f))
				gameObject.transform.position = new Vector3(hit.point.x, hit.point.y + gameObject.transform.localScale.y / 2, hit.point.z);
			else
				gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
		}
	}
}
