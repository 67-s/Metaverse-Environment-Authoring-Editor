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

	public int column = 3;
	public int row = 3;

	public GameObject btn;
	List<GameObject[]> btns;

    void Start()
    {
		Debug.Log(transform.position);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Make2dMap()
	{
		width = GetComponent<RectTransform>().rect.width / column;
		height = GetComponent<RectTransform>().rect.height / row;
		Debug.Log(new Vector2(width, height));
		btns = new List<GameObject[]>();
		for(int i=0;i< row; i++)
		{
			btns.Add(new GameObject[column]);
			for(int j=0;j<column;j++)
			{
				btns[i][j] = Instantiate(btn, this.transform.position + new Vector3(width * j, height * i, 0), new Quaternion(0,0,0,1));
				btns[i][j].transform.SetParent(transform);

				RectTransform rect = btns[i][j].GetComponent<RectTransform>();
				rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
				rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
			}
		}
	}
}
