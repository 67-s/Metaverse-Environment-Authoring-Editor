using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuilderBase : MonoBehaviour
{
    [SerializeField] protected float unit;
    [SerializeField] protected int xWidth;
    [SerializeField] protected int zWidth;

    [SerializeField] protected EBuildDirection direction;
    [SerializeField] protected int seed;
    
    public BuilderBase SetUnit(float unit)
    {
        this.unit = unit;
        return this;
    }

    public BuilderBase SetGrid(int x, int z)
    {
        this.xWidth = x;
        this.zWidth = z;
        return this;
    }

    public BuilderBase SetDirection(EBuildDirection direction)
    {
        this.direction = direction;
        return this;
    }

    public BuilderBase SetSeed(int seed)
    {
        this.seed = seed;
        return this;
    }


    protected void Spawn(GameObject prefab, Vector3 position, float angle)
    {
        GameObject target = Instantiate(prefab, transform);
        target.transform.localPosition = position;
        target.transform.Rotate(Vector3.up, angle);
    }
}
