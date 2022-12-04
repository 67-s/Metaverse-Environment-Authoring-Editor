using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContainer : BuilderBase
{
    [Range(0, 0.5f)]
    public float marginRatio = 0.01f;
    public float heightRatio = 1.2f;

    [SerializeField] private int whiteBoxID = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 vector = new(xWidth, heightRatio, zWidth);
        //box = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //box.transform.SetParent(transform);
        //box.transform.localPosition = vector * unit / 2;
        //box.transform.localScale = (vector - 2 * marginRatio * new Vector3(1, 0, 1)) * unit;
        //box.GetComponent<Renderer>().material.color = Color.Lerp(Color.black, Color.white, (float)(new System.Random(seed).NextDouble()));
        Vector3 vector = new(xWidth, heightRatio, zWidth);
        gameObject.transform.localScale = (vector - 2 * marginRatio * new Vector3(1, 0, 1)) * unit;
        Spawn(whiteBoxID, 0.5f * Vector3.one, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
