using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PrefabObj : MonoBehaviour
{
	Material outline;
	Renderer renderers;
	List<Material> materialList = new List<Material>();
	bool selected = false;
	Installer installer;
	CreationData data;

	private void Awake()
	{
		data = new()
		{
			Target = gameObject
		};
		installer = GameObject.Find("Mediator").GetComponentInChildren<Installer>();
		data.Origin = installer.AdditionalData_Add(data);
	}

	void Start()
	{
		outline = new Material(Shader.Find("Outlined/NewOcclusionOutline"));
	}
	private void Update()
	{
		if (selected && Input.GetMouseButtonDown(1))
		{
			transform.eulerAngles = new Vector3(0,90,0) + transform.eulerAngles;
		}

		float wheelInput = Input.GetAxis("Mouse ScrollWheel");
		if (selected && wheelInput > 0)
		{
			transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
		}
		else if (selected && wheelInput < 0)
		{
			transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
		}

		if (selected && Input.GetKeyDown(KeyCode.D))
		{
			installer.AdditionalData_Remove(data.Origin);
			Destroy(gameObject);
		}
	}

	private void OnMouseDown()
	{
		selected = true;
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
			//materialList.Add(renderers.sharedMaterials.ToArray()[0]);
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

		selected = false;
	}

	public void OnMouseDrag()
	{
		if (!ColorBtn.colorFlag)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 1000f))
				gameObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			else
				gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
		}
	}
}
