using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContainer : MonoBehaviour, IBuilder
{
    public float unit;
    public int xWidth;
    public int zWidth;
    [Range(0, 0.5f)]
    public float marginRatio = 0.01f;
    public float heightRatio = 1.2f;

    private GameObject box;    
    public void Initialize(float unit, int xWidth, int zWidth, EBuildDirection direction)
    {
        this.unit = unit;
        this.xWidth = xWidth;
        this.zWidth = zWidth;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 vector = new Vector3(xWidth, heightRatio, zWidth);
        box = GameObject.CreatePrimitive(PrimitiveType.Cube);
        box.transform.SetParent(transform);
        box.transform.localPosition = vector * unit / 2;
        box.transform.localScale = (vector - 2 * marginRatio * new Vector3(1, 0, 1)) * unit;
        box.GetComponent<Renderer>().material.color = Color.Lerp(Color.black, Color.white, Random.value);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
