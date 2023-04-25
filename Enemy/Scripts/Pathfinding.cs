using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;

public class Pathfinding
{

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private int2 gridSize;

    public Pathfinding(int2 gridSize)
    { 
        this.gridSize = gridSize;
    }

    

    public int2[] FindPath(int2 startPosition, int2 endPosition, Grid<SbyteGridObject> costGrid) 
    { 
            NativeArray<PathNode> pathNodeArray = new NativeArray<PathNode>(gridSize.x * gridSize.y, Allocator.Temp);

            //Инициализация массива всех нод
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    PathNode pathNode = new PathNode();
                    pathNode.x = x;
                    pathNode.y = y;
                    pathNode.index = CalculateIndex(x, y, gridSize.x);

                    pathNode.gCost = int.MaxValue;
                    pathNode.hCost = CalculateDistanceCost(new int2(x, y), endPosition);
                    pathNode.CalculateFCost();
                    SbyteGridObject costGridObject = costGrid.GetGridObject(x, y);
                    pathNode.SetWalkCost(costGridObject.GetValue());
                    pathNode.cameFromNodeIndex = -1;

                    pathNodeArray[pathNode.index] = pathNode;
                }
            }

            NativeArray<int2> neighbourOffsetArray = new NativeArray<int2>(new int2[] {
            new int2(-1, 0),
            new int2(+1, 0),
            new int2(0, -1),
            new int2(0, +1),
            new int2(-1, -1),
            new int2(-1, +1),
            new int2(+1, -1),
            new int2(+1, +1),
            }, Allocator.Temp);

            int endNodeIndex = CalculateIndex(endPosition.x, endPosition.y, gridSize.x);

            PathNode startNode = pathNodeArray[CalculateIndex(startPosition.x, startPosition.y, gridSize.x)];
            startNode.gCost = 0;
            startNode.CalculateFCost();
            pathNodeArray[startNode.index] = startNode; //Массив всех PathNode

            NativeList<int> openList = new NativeList<int>(Allocator.Temp); //Массив индексов в коттором хранянчтся ноды для исследования
            NativeList<int> closeList = new NativeList<int>(Allocator.Temp); //Массив индексов в коттором хранятся исследованые ноды

            openList.Add(startNode.index);

            while (openList.Length > 0)
            {
                int currentNodeIndex = GetLowestCostFNodeIndex(openList, pathNodeArray);//Индекс ноды, которую будем исследовать
                PathNode currentNode = pathNodeArray[currentNodeIndex];

                if (currentNodeIndex == endNodeIndex)
                {
                    //Мы достигли конца
                    break;
                }

                //Убираем текущую ноду из открытого листа
                for (int i = 0; i < openList.Length; i++)
                {
                    if (openList[i] == currentNodeIndex)
                    {
                        openList.RemoveAtSwapBack(i);
                        break;
                    }
                }

                //Добавляем текущею ноду в закрытый лист
                closeList.Add(currentNodeIndex);

                //Добавляем соседей в открытый лист
                for (int i = 0; i < neighbourOffsetArray.Length; i++)
                {
                    int2 neighbourOffset = neighbourOffsetArray[i];
                    int2 neighbourNodePosition = new(currentNode.x + neighbourOffset.x, currentNode.y + neighbourOffset.y);

                    if (!IsPositionInsideGrid(neighbourNodePosition, gridSize))
                    {
                        continue;
                    }

                    int neighbourNodeIndex = CalculateIndex(neighbourNodePosition.x, neighbourNodePosition.y, gridSize.x);

                    if (closeList.Contains(neighbourNodeIndex))
                    {
                        continue;
                    }

                    PathNode neighbourNode = pathNodeArray[neighbourNodeIndex];
                    if (neighbourNode.walkCost == -1)
                    {
                        continue;
                    }

                    int2 currentNodePosition = new int2(currentNode.x, currentNode.y);
                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNodePosition, neighbourNodePosition);
                    if (tentativeGCost < neighbourNode.gCost)
                    {
                        neighbourNode.cameFromNodeIndex = currentNodeIndex;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.CalculateFCost();
                        pathNodeArray[neighbourNodeIndex] = neighbourNode;

                        if (!openList.Contains(neighbourNode.index))
                        {
                            openList.Add(neighbourNode.index);
                        }
                    }
                }
            }

            PathNode endNode = pathNodeArray[endNodeIndex];
            int2[] pathArray;
            if (endNode.cameFromNodeIndex == -1)
            {
            //Не нашли путь
                pathArray = null;

                openList.Dispose();
                neighbourOffsetArray.Dispose();
                closeList.Dispose();
                pathNodeArray.Dispose();

            return pathArray;
            } else {
                //Нашли путь
                NativeList<int2> path = CalculatePath(pathNodeArray, endNode);//Ноды пути

            pathArray = new int2[path.Length];

            int j = 0;
            for(int i = path.Length - 1; i >= 0; i--)
            {
                pathArray[j] = path[i];
                j++;
            }

            openList.Dispose();
            neighbourOffsetArray.Dispose();
            closeList.Dispose();
            pathNodeArray.Dispose();
            path.Dispose();

            return pathArray;
            }
        }

        private NativeList<int2> CalculatePath(NativeArray<PathNode> pathNodeArray, PathNode endNode)
        {
            if (endNode.cameFromNodeIndex == -1)
            {
                // Couldn't find a path!
                return new NativeList<int2>(Allocator.Temp);
            }
            else
            {
                // Found a path
                NativeList<int2> path = new NativeList<int2>(Allocator.Temp);
                path.Add(new int2(endNode.x, endNode.y));

                PathNode currentNode = endNode;
                while (currentNode.cameFromNodeIndex != -1)
                {
                    PathNode cameFromNode = pathNodeArray[currentNode.cameFromNodeIndex];
                    path.Add(new int2(cameFromNode.x, cameFromNode.y));
                    currentNode = cameFromNode;
                }
                return path;
            }
        }

        private bool IsPositionInsideGrid(int2 gridPosition, int2 gridSize)
        {
            return
                gridPosition.x >= 0 &&
                gridPosition.y >= 0 &&
                gridPosition.x < gridSize.x &&
                gridPosition.y < gridSize.y;
        }

        private int CalculateIndex(int x, int y, int gridWidth)
        {
            return x + y * gridWidth;
        }

        private int CalculateDistanceCost(int2 aPosition, int2 bPosition)
        {
            int xDistance = math.abs(aPosition.x - bPosition.x);
            int yDistance = math.abs(aPosition.y - bPosition.y);
            int remaining = math.abs(xDistance - yDistance);
            return MOVE_DIAGONAL_COST * math.min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }

        private int GetLowestCostFNodeIndex(NativeList<int> openList, NativeArray<PathNode> pathNodeArray)
        {
            PathNode lowestCostPathNode = pathNodeArray[openList[0]];
            for (int i = 1; i < openList.Length; i++)
            {
                PathNode testPathNode = pathNodeArray[openList[i]];
                if (testPathNode.fCost < lowestCostPathNode.fCost)
                {
                    lowestCostPathNode = testPathNode;
                }
            }
            return lowestCostPathNode.index;
        }
}