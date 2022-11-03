using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilder
{
    void Initialize(float unit, int xWidth, int zWidth, EBuildDirection direction);
}
