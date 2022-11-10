using ExitGames.Client.Photon.StructWrapping;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TwoDiMap : MonoBehaviour
{
	float width = 0;
	float height = 0;

	int colCnt = 3;
	int rowCnt = 3;

	public GameObject prefab;
	List<List<GameObject>> prefabs = null;
	static Dictionary<Area, AreaData> buildArea = null;

	public void Make2dMap()
	{
		int rectHeight = (int)GetComponent<RectTransform>().rect.height;

		width = GetComponent<RectTransform>().rect.width / colCnt;
		height = rectHeight / rowCnt;

		prefabs = new List<List<GameObject>>();
		buildArea = new Dictionary<Area, AreaData>();

		for (int i=0;i< rowCnt; i++)
		{
			prefabs.Add(new List<GameObject>());
			for(int j=0;j<colCnt;j++)
			{
				prefabs[i].Add(Instantiate(prefab, transform.position + new Vector3(width * j, rectHeight - height * (i + 1), 0), new Quaternion(0,0,0,1)));
				prefabs[i][j].transform.SetParent(transform);
				prefabs[i][j].transform.name = i.ToString() + "," + j.ToString();
				RectTransform rect = prefabs[i][j].GetComponent<RectTransform>();
				rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
				rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
			}
		}
	}

	public void SetRowCnt(int cnt)
	{
		rowCnt = cnt;
	}

	public void SetColCnt(int cnt)
	{
		colCnt = cnt;
	}

	public Area ChangeObjToArea(GameObject fstGameObject, GameObject sndGameObject)
	{
		int findFlag = 0;
		int fstRow = -1;
		int fstCol = -1;
		int sndRow = -1;
		int sndCol = -1;

		for (int i = 0; i < rowCnt; i++)
		{
			int idx;
			if ((idx = prefabs[i].IndexOf(fstGameObject)) != -1)
			{
				findFlag &= 1;
				fstRow = i;
				fstCol = idx;
			}
			if ((idx = prefabs[i].IndexOf(sndGameObject)) != -1)
			{
				findFlag &= 2;
				sndRow = i;
				sndCol = idx;
			}
			if (findFlag == 3)
				break;
		}

		if (fstRow == -1 || fstCol == -1 || sndRow == -1 || sndRow == -1)
			return null;
		return new Area(fstRow, fstCol, sndRow, sndCol);
	}
	public void SetPrefapColor(Area area, Color color)
	{
		int fstRow = area.FstRow;
		int fstCol = area.FstCol;
		int sndRow = area.SndRow;
		int sndCol = area.SndCol;

		if (fstRow > sndRow)
			(fstRow, sndRow) = (sndRow, fstRow);
		if (fstCol > sndCol)
			(fstCol, sndCol) = (sndCol, fstCol);

		for (int i = fstRow; i <= sndRow; i++)
		{
			for (int j = fstCol; j <= sndCol; j++)
			{
				prefabs[i][j].GetComponent<Image>().color = color;
			}
		}
	}

	public void AddInBuildArea(Area area, int buildIdx)
	{
		buildArea.Add(area, new AreaData(area, buildIdx));
	}

	public void DestroyMap()
	{
		if (prefabs == null) return;

		for (int i = 0; i < rowCnt; i++)
		{
			for (int j = 0; j < colCnt; j++)
			{
				Destroy(prefabs[i][j]);
			}
		}
	}

	public Dictionary<Area, AreaData>GetBuildArea()
	{
		return buildArea;
	}

	public void DelBuildArea(Area area)
	{
		buildArea.Remove(area);

		for (int i = area.FstRow; i <= area.SndRow; i++)
		{
			for (int j = area.FstCol; j <= area.SndCol; j++)
			{
				prefabs[i][j].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
			}
		}
	}
	public void DebugBuildArea()
	{
		foreach(KeyValuePair<Area, AreaData> item in buildArea)
		{
			Debug.Log($"{ item.Value.FstRow},{item.Value.FstCol},{item.Value.SndRow},{item.Value.SndCol}");
		}
	}
}
