using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PathNode
{
    public int x;
    public int y;

    public int index;

    public int gCost; //Стоймость всего пройденого пути до этой ноды
    public int hCost; //Примерная стоймость от этой ноды до конца
    public int fCost; //Приоритет пути (g + h)

    public sbyte walkCost;//Стоймость передвижения по этой клетке (-1 - передвижение не возможно)

    public int cameFromNodeIndex;

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    public void SetWalkCost(sbyte cost)
    {
        walkCost = cost;
    }
}
