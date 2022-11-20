using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabObj : MonoBehaviour
{
	public bool isSelected = false;

	Material outline;

	Renderer renderers;
	List<Material> materialList = new List<Material>();
	void Start()
	{
		outline = new Material(Shader.Find("Outlined/NewOcclusionOutline"));
	}

	private void OnMouseDown()
	{
		Debug.Log(gameObject.name);
		renderers = this.GetComponent<Renderer>();

		materialList.Clear();
		materialList.AddRange(renderers.sharedMaterials);
		materialList.Add(outline);

		renderers.materials = materialList.ToArray();
	}

	private void OnMouseUp()
	{
		Renderer renderer = this.GetComponent<Renderer>();

		materialList.Clear();
		materialList.AddRange(renderer.sharedMaterials);
		materialList.Remove(outline);

		renderer.materials = materialList.ToArray();
	}

}
