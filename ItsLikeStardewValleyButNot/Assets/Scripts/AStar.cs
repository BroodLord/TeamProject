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

    private void Awake()
    {
        //npcPathfind(Vector3Int.RoundToInt(this.transform.position), Vector3Int.RoundToInt(goal.transform.position), Nonwalkable, grid);
    }
    public List<Node> npcPathfind(Vector3Int Start, Vector3Int Goal, Tilemap nonWalkable, Grid grid)
    {
        bool found = false;

        

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        List<Node> result = new List<Node>();

        Node startNode = new Node(grid.WorldToCell(Start));

        Vector3Int goal = grid.WorldToCell(Goal);

        openList.Add(startNode);
        Node currentNode = openList[0];

        while (openList.Count < 100 || openList.Count > 0 || !found)
        {
            //make closest node to the goal the current node
            currentNode = openList[0];

            //remove from the openlist, add to closed 
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //check to see if current is the goal, if so end
            if (currentNode.worldPosition == goal)
            {
                found = true;
                break;
            }

            Vector3Int up = new Vector3Int(currentNode.worldPosition.x, currentNode.worldPosition.y + 1, 0);
            Vector3Int right = new Vector3Int(currentNode.worldPosition.x + 1, currentNode.worldPosition.y , 0);
            Vector3Int down = new Vector3Int(currentNode.worldPosition.x, currentNode.worldPosition.y - 1, 0);
            Vector3Int left = new Vector3Int(currentNode.worldPosition.x - 1, currentNode.worldPosition.y, 0);

            Node Temp;

            //need to stop the adding of the same tile  

            //if not add up, right, down, left to open list 
            if (nonWalkable.GetTile(up) == null)
            {
                Temp = new Node(up);
                if (!openList.Contains(Temp) || !closedList.Contains(Temp))
                {
                    Temp.parent = currentNode;
                    //GameObject instance = Instantiate(tempObj, Temp.worldPosition, Quaternion.identity);
                    openList.Add(Temp);
                }
            }

            if (nonWalkable.GetTile(right) == null)
            {
                Temp = new Node(right);
                if (!openList.Contains(Temp) || !closedList.Contains(Temp))
                {
                    Temp.parent = currentNode;
                    //GameObject instance = Instantiate(tempObj, Temp.worldPosition, Quaternion.identity);
                    openList.Add(Temp);
                }
            }

            if (nonWalkable.GetTile(down) == null)
            {
                Temp = new Node(down);
                if (!openList.Contains(Temp) || !closedList.Contains(Temp))
                {
                    Temp.parent = currentNode;
                    //GameObject instance = Instantiate(tempObj, Temp.worldPosition, Quaternion.identity);
                    openList.Add(Temp);
                }
            }

            if (nonWalkable.GetTile(left) == null)
            {
                Temp = new Node(left);
                if (!openList.Contains(Temp) || !closedList.Contains(Temp))
                {
                    Temp.parent = currentNode;
                    //GameObject instance = Instantiate(tempObj, Temp.worldPosition, Quaternion.identity);
                    openList.Add(Temp);
                }
            }



            openList.Sort((x, y) => { return (goal - x.worldPosition).sqrMagnitude.CompareTo((goal - y.worldPosition).sqrMagnitude); });



        }

        if (found)
        {
            while (currentNode.parent != null)
            {
                result.Add(currentNode.parent);
                nonWalkable.SetTile(currentNode.parent.worldPosition, tempTile);
                currentNode = currentNode.parent;
            }
            return result;
        }
        else
        {
            return null;
        }
       
    }
}


