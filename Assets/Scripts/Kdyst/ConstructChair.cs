using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Kdyst;

public class ConstructChair : MonoBehaviour
{
    public GameObject prefab;
    public float interval;

    [Range(0, 10)]
    public float speed = 3.0f;

    private GameObject cam;
    private Vector3 camPosition;

    private int SquareRoot(int x)
    {
        int i = 0;
        while (i * i < x) ++i;
        return i;
    }

    // Start is called before the first frame update
    void Start()
    {
        int num = KdystContainer.Number, bundle = SquareRoot(num);

        prefab.SetActive(true);
        for(int i = 0; i < num; i++)
        {
            int r = i / bundle, c = i % bundle;
            GameObject.Instantiate(prefab, transform.position + new Vector3(interval * c, 0, interval * r), Quaternion.identity);
        }
        prefab.SetActive(false);
        cam = GameObject.Find("Main Camera");
        camPosition = transform.position + new Vector3(interval * bundle / 2, 150, interval * bundle / 2);
        cam.transform.SetPositionAndRotation(camPosition, cam.transform.localRotation);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = Vector3.zero;
        bool changed = false;
        if (Input.GetKey(KeyCode.W)) { diff += new Vector3(0, 0, 1); changed = true; }
        if (Input.GetKey(KeyCode.A)) { diff += new Vector3(-1, 0, 0); changed = true; }
        if (Input.GetKey(KeyCode.S)) { diff += new Vector3(0, 0, -1); changed = true; }
        if (Input.GetKey(KeyCode.D)) { diff += new Vector3(1, 0, 0); changed = true; }
        if (changed)
        {
            camPosition += diff.normalized * speed;
            cam.transform.SetPositionAndRotation(camPosition, cam.transform.localRotation);
        }
    }
}
