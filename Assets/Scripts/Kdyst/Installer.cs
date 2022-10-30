using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Installer : MonoBehaviour
{
    //default tile: It means that the tile data is broken.
    [SerializeField]
    private GameObject tileDefault;

    //the sequence of the tiles
    [SerializeField]
    private GameObject[] tileSet;

    //The length of one side of tile.
    [SerializeField]
    [Range(0, 100)]
    private float tileLength;

    //The number of rows/columns of the map
    [Range(0, 33)]
    public int defaultXSize, defaultZSize;
    
    //This is the map.
    private GameObject[,] tileMap = null;
    
    //Make a clone of the gameObject and place it
    GameObject CloneTile(int x, int z, int key)
    {
        GameObject tile = (0 <= key && key < tileSet.Length) ? tileSet[key] : tileDefault;
        GameObject target = Instantiate(tile, gameObject.transform);
        Vector3 vec = new Vector3(x, 0, z) * tileLength;
        target.transform.localScale = tileLength * target.transform.localScale;
        target.transform.Translate(vec);
        target.SetActive(true);
        return target;
    }

    //Resize the map(may be the map will shrink or expand)
    public void Resize(int xSize, int zSize, int key = 0)
    {
        if(xSize <= 0 || zSize <= 0)
        {
            Debug.Log("kdyst/Installer.cs: Installer.Resize(): (rowSize, colSize) == (" + xSize + ", " + zSize + ") is not proper.");
            return;
        }

        GameObject[,] temporary = new GameObject[xSize, zSize];
        tileMap ??= new GameObject[0, 0];
        for(int x = 0; x < xSize; x++)
            for(int z = 0; z < zSize; z++)
            {
                if (x < tileMap.GetLength(0) && z < tileMap.GetLength(1))
                {
                    temporary[x, z] = tileMap[x, z];
                    tileMap[x, z] = null;
                }
                else
                {
                    temporary[x, z] = CloneTile(x, z, key);
                }
            }
        foreach (var gameObject in tileMap)
            Destroy(gameObject);
        (_, tileMap) = (tileMap, temporary);
    }

    // Change the tile data
    public void ChangeTile(int x, int z, int xWidth, int zWidth, int key)
    {
        //exceptions
        if (x < 0 || z < 0 
            || xWidth <= 0 || zWidth <= 0 
            || x + xWidth > tileMap.GetLength(0) || z + zWidth > tileMap.GetLength(1))
        {
            Func<int, int, string> foo = (a,  b) => "(" + a + ", " + b + ")";
            string front = "kdyst/Installer.cs: Installer.ChangeTile():";
            Debug.Log(front 
                + " (x, z) == " + foo(x, z)
                + ", (xWidth, zWidth) == " + foo(xWidth, zWidth)
                + "(xSize, ySize) == " + foo(tileMap.GetLength(0), tileMap.GetLength(1))
                );
            return;
        }
        for(int xDiff = 0; xDiff < xWidth; xDiff++)
            for (int zDiff = 0; zDiff < zWidth; zDiff++)
            {
                GameObject temp = tileMap[x + xDiff, z + zDiff];
                tileMap[x + xDiff, z + zDiff] = CloneTile(x + xDiff, z + zDiff, key);
                Destroy(temp);
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        Resize(defaultXSize, defaultZSize, 1);
    }

    public bool actionResize = false;
    [Range(0, 33)]
    public int actionResizeXSize, actionResizeZSize;

    public bool actionChangeTile = false;
    public int actionChangeTileX, actionChangTileZ;
    public int actionChangeTileXWidth, actionChangTileZWidth;
    public int actionChangeTileKey;
    // Update is called once per frame
    void Update()
    {
        if (actionResize)
        {
            actionResize = false;
            Resize(actionResizeXSize, actionResizeZSize, 2);
        }
        if(actionChangeTile)
        {
            actionChangeTile = false;
            ChangeTile(actionChangeTileX, actionChangTileZ, actionChangeTileXWidth, actionChangTileZWidth, actionChangeTileKey);
        }
    }
}
