using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableMatrix<T>
{
    [Serializable]
    public class Row
    {
        public List<T> values = new List<T>();
        public T this[int index] => values[index];
    }

    public List<Row> rows = new List<Row>();
    public T this[int x, int y] => rows[x][y];
}
