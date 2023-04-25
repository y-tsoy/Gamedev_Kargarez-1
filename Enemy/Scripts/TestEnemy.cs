using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;

public class TestEnemy : MonoBehaviour
{
    private Grid<SbyteGridObject> grid;
    private int2[] path;
    private int pathIndex = 0;
    private float cellSize = 0.5f;
    private int2 gridSize = new int2(10, 10);
    private Pathfinding pathfinding;
    public float speed = 4;
    
    void Start()
    {
        this.grid = new Grid<SbyteGridObject>(gridSize.x, gridSize.y, cellSize, Vector3.zero, (Grid<SbyteGridObject> g, int x, int y) => new SbyteGridObject(g, x, y));
        pathfinding = new Pathfinding(new int2(gridSize.x, gridSize.y));
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++)
            {
                SbyteGridObject costGridObject = grid.GetGridObject(x, y);
                costGridObject.value = 0;
                grid.SetGridObject(x, y,costGridObject);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.GetXY(transform.position, out int xStart, out int yStart);
            grid.GetXY(mouseWorldPosition, out int x, out int y);
            path = pathfinding.FindPath(new int2(xStart, yStart), new int2(x, y), grid);
            pathIndex = 0;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.GetXY(mouseWorldPosition, out int x, out int y);
            SbyteGridObject costGridObject = grid.GetGridObject(x, y);
            costGridObject.value = -1;
            grid.SetGridObject(x, y, costGridObject);
        }

        if (path == null || path.Length == 0) return;
        Vector2 target = new Vector2(path[pathIndex].x * cellSize + cellSize / 2, path[pathIndex].y * cellSize + cellSize / 2);
        float distance = Vector2.Distance(transform.position, target);
        Vector2 direction = target - (Vector2)transform.position;

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (pathIndex < path.Length - 1 && (distance < 0.1f))
            pathIndex++;
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