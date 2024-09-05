using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

class Cell
{
    public HashSet<GameObject> Objects { get; } = new HashSet<GameObject>();
}

public class GridController : BaseController
{
   UnityEngine.Grid grid;

    Dictionary<Vector3Int,Cell> cells = new Dictionary<Vector3Int, Cell>();

    
    
    public override bool Init()
    {
        Debug.Log("GridController Init");
        base.Init();

        grid = gameObject.GetOrAddComponent<Grid>();    
        

        return true;
    }

    public void Add(GameObject go)
    {
        
        Vector3Int cellPos = grid.WorldToCell(go.transform.position);
        Cell cell = GetCell(cellPos);

        if (cell ==null)
        {
            return;
        }

        cell.Objects.Add(go);


    }
    public void Remove(GameObject go)
    {
        Vector3Int cellPos = grid.WorldToCell(go.transform.position);
        Cell cell = GetCell(cellPos);

        if (cell == null)
        {
            return;
        }

        cell.Objects.Remove(go);

    }
    Cell GetCell (Vector3Int cellPos)
    {
        Cell cell = null;

        if(false==cells.TryGetValue(cellPos, out cell))
        {
            cell = new Cell();
            cells.Add(cellPos, cell);

        }
            return cell;
    }

    public List<GameObject> GatherObjects(Vector3 pos,  float range)
    {
        List<GameObject> objects = new List<GameObject>();
      
        Vector3Int left = grid.WorldToCell(pos - new Vector3(range,0, 0));
       
        Vector3Int right = grid.WorldToCell(pos + new Vector3(range, 0, 0));
        Vector3Int bottom = grid.WorldToCell(pos - new Vector3(0, range, 0));
        Vector3Int top = grid.WorldToCell(pos + new Vector3(0, range, 0));

        int minX = left.x;
        int maxX = right.x;
        int minY = bottom.y;
        int maxY = top.y;

        for(int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                if(cells.ContainsKey(new Vector3Int(x, y, 0)) == false)
                {
                    continue;
                }

               objects.AddRange(cells[new Vector3Int(x, y, 0)].Objects); 
            }
        }
        return objects;
    }


}

