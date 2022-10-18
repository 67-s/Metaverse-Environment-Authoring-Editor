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

	public GameObject btn;
	List<GameObject[]> btns;

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
		btns = new List<GameObject[]>();
		for(int i=0;i< rowCnt; i++)
		{
			btns.Add(new GameObject[colCnt]);
			for(int j=0;j<colCnt;j++)
			{
				btns[i][j] = Instantiate(btn, this.transform.position + new Vector3(width * j, height * i, 0), new Quaternion(0,0,0,1));
				btns[i][j].transform.SetParent(transform);

				RectTransform rect = btns[i][j].GetComponent<RectTransform>();
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
}
