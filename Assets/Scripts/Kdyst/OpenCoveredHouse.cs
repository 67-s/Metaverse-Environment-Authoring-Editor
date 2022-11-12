using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCoveredHouse : MonoBehaviour, IBuilder
{
    public float unit;
    public int xWidth;
    public int zWidth;

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject cornerPrefab;
    [SerializeField] private GameObject floorPrefab;

    void IBuilder.Initialize(float unit, int xWidth, int zWidth, EBuildDirection direction)
    {
        this.unit = unit;
        this.xWidth = xWidth;
        this.zWidth = zWidth;
    }

    void spawn(GameObject prefab, Vector3 position, float angle)
    {
        GameObject target = Instantiate(prefab, transform);
        target.transform.localPosition = position;
        target.transform.Rotate(Vector3.up, angle);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = Vector3.one * unit / 6;
        //spawn corner
        spawn(cornerPrefab, 6 * new Vector3(xWidth, 0, 0), 0);
        spawn(cornerPrefab, 6 * new Vector3(0, 0, 0), 90);
        spawn(cornerPrefab, 6 * new Vector3(0, 0, zWidth), 180);
        spawn(cornerPrefab, 6 * new Vector3(xWidth, 0, zWidth), 270);

        //spawn wall x
        for(int x = 1; x < 2 * xWidth; x++)
        {
            spawn(wallPrefab, new Vector3(3 * x, 0, 0), 0);
            spawn(wallPrefab, new Vector3(3 * x, 0, 6 * zWidth), 180);
        }

        //spawn wall z
        for(int z = 1; z < 2 * zWidth; z++)
        {
            spawn(wallPrefab, new Vector3(0, 0, 3 * z), 90);
            spawn(wallPrefab, new Vector3(6 * xWidth, 0, 3 * z), 270);
        }

        //spawn tile
        for (int x = 0; x < 2 * xWidth; x++)
            for (int z = 0; z < 2 * zWidth; z++)
                spawn(floorPrefab, new Vector3(1.5f + 3 * x, 0.01f, 1.5f + 3 * z), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
