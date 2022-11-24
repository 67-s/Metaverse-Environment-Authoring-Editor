using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBtn : MonoBehaviour
{
	public static bool colorFlag = false;
	public static Material colorMaterial = null;
	public Material material;

	public void OnClickColorBtn()
	{
		colorFlag = true;
		colorMaterial = material;
	}
}
