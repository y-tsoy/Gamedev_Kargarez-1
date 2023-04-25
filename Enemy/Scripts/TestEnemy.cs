using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;

public class TestEnemy : MonoBehaviour
{
    private Grid<SbyteGridObject> grid;

    // Start is called before the first frame update
    void Start()
    {
        this.grid = new Grid<SbyteGridObject>(10, 10, 2, Vector3.zero, (Grid<SbyteGridObject> g, int x, int y) => new SbyteGridObject(g, x, y));
        Pathfinding pathfinding = new Pathfinding(new int2(10, 10));
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++)
            {
                SbyteGridObject costGridObject = grid.GetGridObject(x, y);
                costGridObject.value = 0;
                grid.SetGridObject(x, y,costGridObject);
            }
        }

        Debug.Log(pathfinding.FindPath(new int2(0, 0), new int2(9, 9), grid)[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class SbyteGridObject
{

    private Grid<SbyteGridObject> grid;
    private int x;
    private int y;
    public sbyte value = 0;

    public SbyteGridObject(Grid<SbyteGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public sbyte GetValue() { 
        return value;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}