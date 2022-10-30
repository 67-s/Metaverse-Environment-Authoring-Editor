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

	private static int fstRow = -1;
	private static int fstCol = -1;
	private static int sndRow = -1;
	private static int sndCol = -1;

	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Make2dMap()
	{
		width = GetComponent<RectTransform>().rect.width / colCnt;
		height = GetComponent<RectTransform>().rect.height / rowCnt;

		prefabs = new List<List<GameObject>>();
		for(int i=0;i< rowCnt; i++)
		{
			prefabs.Add(new List<GameObject>());
			for(int j=0;j<colCnt;j++)
			{
				prefabs[i].Add(Instantiate(prefab, this.transform.position + new Vector3(width * j, height * i, 0), new Quaternion(0,0,0,1)));
				prefabs[i][j].transform.SetParent(transform);

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

	public void SetPrefapColorWithGameObj(GameObject fstGameObject, GameObject sndGameObject)
	{
		int findFlag = 0;

		for (int i = 0; i < rowCnt; i++) 
		{
			int idx;
			if ((idx = prefabs[i].IndexOf(fstGameObject)) != -1)
			{
				findFlag &= 1;
				fstRow = i;
				fstCol = idx;
			}
			if ((idx = prefabs[i].IndexOf(sndGameObject)) !=-1)
			{
				findFlag &= 2;
				sndRow = i;
				sndCol = idx;
			}
			if (findFlag == 3)
				break;
		}

		if (fstRow == -1 || sndRow == -1 || fstCol == -1 || sndRow == -1)
			return;

		if (fstRow > sndRow)
			(fstRow, sndRow) = (sndRow, fstRow);
		if (fstCol > sndCol)
			(fstCol, sndCol) = (sndCol, fstCol);

		for (int i = fstRow; i <= sndRow; i++)
		{
			for (int j = fstCol; j <= sndCol; j++)
			{ 
				prefabs[i][j].GetComponent<Image>().color = new Color(0.5f, 0.2f, 0.3f);
			}
		}
	}

	public void SetPrefapColorWithPos()
	{
		if (fstRow == -1 || sndRow == -1 || fstCol == -1 || sndRow == -1)
			return;

		if (fstRow > sndRow)
			(fstRow, sndRow) = (sndRow, fstRow);
		if (fstCol > sndCol)
			(fstCol, sndCol) = (sndCol, fstCol);

		for (int i = fstRow; i <= sndRow; i++)
		{
			for (int j = fstCol; j <= sndCol; j++)
			{
				prefabs[i][j].GetComponent<Image>().color = new Color(0.5f, 0.2f, 0.3f);
			}
		}
		fstRow = -1;
		fstCol = -1;
		sndRow = -1;
		sndCol = -1;
	}
	public void DestroyMap()
	{
		for (int i = 0; i < rowCnt; i++)
		{
			for (int j = 0; j < colCnt; j++)
			{
				Destroy(prefabs[i][j]);
			}
		}
	}
}
