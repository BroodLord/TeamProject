using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class AStar : MonoBehaviour
{


    public TileBase tempTile;
    public GameObject goal;
    public Grid grid;
    public Tilemap Nonwalkable;
    JunkPlacer placer;

    //work, please :'(
    public List<Node> npcPathfind(Vector3Int Start, Vector3Int Goal, Tilemap nonWalkable, Grid grid)
    {
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        List<Node> result = new List<Node>();

        //start node is created and a world and grid position is created, Node is custom created as i dont think the grid has its own
        Node startNode = new Node(grid.WorldToCell(Start));
        startNode.worldPosition = Start;
        Vector3Int goal = grid.WorldToCell(Goal);


        openList.Add(startNode);
        Node currentNode = openList[0];

        while (openList.Count > 0)
        {
            //make closest node to the goal the current node
            currentNode = openList[0];

            //remove from the openlist, add to closed 
            openList.Remove(currentNode);
            

            //check to see if current is the goal, if so reverse the path and return the result
            if (currentNode.gridPosition == goal)
            {
                bool foundStart = false;

                do
                {
                    if (currentNode.parent == null)
                    {
                        foundStart = true;
                    }
                    else
                    {
                        result.Insert(0, currentNode);
                        currentNode = currentNode.parent;
                    }
                } while (!foundStart);

                result.Insert(0, startNode);

                return result;
            }

            //checks all directions
            Vector3Int up = new Vector3Int(currentNode.gridPosition.x, currentNode.gridPosition.y + 1, 0);
            Vector3Int right = new Vector3Int(currentNode.gridPosition.x + 1, currentNode.gridPosition.y , 0);
            Vector3Int down = new Vector3Int(currentNode.gridPosition.x, currentNode.gridPosition.y - 1, 0);
            Vector3Int left = new Vector3Int(currentNode.gridPosition.x - 1, currentNode.gridPosition.y, 0);

            Node Temp;

            //add up, right, down, left to open list if nothing is blocking
            if (nonWalkable.GetTile(up) == null)
            {
                Temp = new Node(up);

                //check open and closed list
                if (!searchList(openList, Temp.gridPosition.x, Temp.gridPosition.y) && !searchList(closedList, Temp.gridPosition.x, Temp.gridPosition.y))
                {
                    Temp.parent = currentNode;
                    //all nodes should have a world position and a grid position
                    Temp.worldPosition = grid.CellToWorld(Temp.gridPosition);
                    openList.Add(Temp);
                }
            }

            if (nonWalkable.GetTile(right) == null)
            {
                Temp = new Node(right);
                if (!searchList(openList, Temp.gridPosition.x, Temp.gridPosition.y) && !searchList(closedList, Temp.gridPosition.x, Temp.gridPosition.y))
                {
                    Temp.parent = currentNode;
                    Temp.worldPosition = grid.CellToWorld(Temp.gridPosition);
                    openList.Add(Temp);
                }
            }

            if (nonWalkable.GetTile(down) == null)
            {
                Temp = new Node(down);
                if (!searchList(openList, Temp.gridPosition.x, Temp.gridPosition.y) && !searchList(closedList, Temp.gridPosition.x, Temp.gridPosition.y))
                {
                    Temp.parent = currentNode;
                    Temp.worldPosition = grid.CellToWorld(Temp.gridPosition);
                    openList.Add(Temp);
                }
            }

            if (nonWalkable.GetTile(left) == null)
            {
                Temp = new Node(left);
                if (!searchList(openList, Temp.gridPosition.x, Temp.gridPosition.y) && !searchList(closedList, Temp.gridPosition.x, Temp.gridPosition.y))
                {
                    Temp.parent = currentNode;
                    Temp.worldPosition = grid.CellToWorld(Temp.gridPosition);
                    openList.Add(Temp);
                }
            }

            closedList.Add(currentNode);

            //sorts based on distance to the goal
            openList.Sort((x, y) => { return (goal - x.gridPosition).sqrMagnitude.CompareTo((goal - y.gridPosition).sqrMagnitude); });

            //Debug.Log(openList.Count);
        }

        Debug.Log(closedList.Count);
        return null;
    }

    bool searchList(List<Node> list, int x, int y)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].gridPosition.x == x && list[i].gridPosition.y == y)
            {
                return true;
            }
        }
        return false;
    }
}


