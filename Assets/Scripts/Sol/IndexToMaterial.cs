using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexToMaterial : MonoBehaviour
{
    public Material[] materials;

    public Material IndexToColor(byte index)
    {
        return materials[index];
    }
}
