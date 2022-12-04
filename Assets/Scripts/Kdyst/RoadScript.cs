using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : BuilderBase
{
    [SerializeField] private int floorPrefab = 30;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = Vector3.one * unit / 6;
        //spawn tile
        for (int x = 0; x < xWidth; x++)
            for (int z = 0; z < zWidth; z++)
                Spawn(floorPrefab, new Vector3(
                    3f + 6 * x,
                    0.01f,
                    3f + 6 * z), 2 * Vector3.one, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
