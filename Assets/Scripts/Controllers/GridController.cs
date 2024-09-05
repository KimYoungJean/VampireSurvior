using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Cell
{
    public HashSet<GameObject> Objects { get; } = new HashSet<GameObject>();
}

public class GridController : BaseController
{
   UnityEngine.Grid grid;

    Dictionary<Vector3Int,Cell> cells = new Dictionary<Vector3Int, Cell>();


}

