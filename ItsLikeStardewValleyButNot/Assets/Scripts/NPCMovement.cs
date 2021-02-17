using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class NPCMovement : MonoBehaviour
{
    private AStar pathfinding;
    private Transform[] waypoints;
    private GameObject waypointMaster;
    private Vector3 target;
    private Tilemap nonWalkable;
    private Grid grid;
    private List<Node> currentPath;
    [SerializeField]private int currentState;
    [SerializeField]private float NPCSpeed = 0.2f;
    private int pathIndex;
    private int waypointIndex;
    float timer = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        pathfinding = new AStar();
        currentState = 3;
        waypointMaster = GameObject.FindGameObjectWithTag("Waypoints");
        waypoints = waypointMaster.gameObject.GetComponentsInChildren<Transform>();
        waypointIndex = 1;
        target = waypoints[waypointIndex].position;
        nonWalkable = GameObject.FindGameObjectWithTag("Non-Walkable").GetComponent<Tilemap>();
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();

    }

    // Update is called once per frame
    void Update()
    {
        //perform A*
        //whilst there is a target and the npc hasnt reached the target do nothing
        //if there isnt a target or the npc reaches a target, go into a waiting state (until a certain time possibly) then cycle to the next target 
         float movement = NPCSpeed * Time.deltaTime;
         
         switch (currentState)
         {
             //Movement State
             case 1:
                 //follow the determined path
                 if (pathIndex >= currentPath.Count)
                 {
                    waypointIndex++;
                    target = waypoints[waypointIndex].position;
                    currentState = 2;
                 }
                 else if (Vector3.Distance(this.transform.position, currentPath[pathIndex].worldPosition) < 0.01f)
                 {
                     pathIndex++;
                 }
                 else
                 {
                     Vector2 v2;
                     v2.x = currentPath[pathIndex].worldPosition.x;
                     v2.y = currentPath[pathIndex].worldPosition.y;
                     this.transform.position = Vector2.MoveTowards(this.transform.position, v2, movement);
                 }
                 //when the destination is reached go to 2
                 break;
             //Waiting state
             case 2:
                //wait until the given time
                
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    timer = 2.0f;
                    currentState = 3;
                }
                 //go to 3
                 
                 break;
             //Path search state
             case 3:
                 //find and update new path
                 Vector3Int thisV3I = Vector3Int.FloorToInt(this.transform.position);
                 Vector3Int targetV3I = Vector3Int.FloorToInt(target);

                 currentPath = pathfinding.npcPathfind(thisV3I, targetV3I, nonWalkable, grid);
                if (currentPath == null)
                {
                    Debug.Log("shits gone tits up");
                    Debug.Break();
                }
                
                 //reset path index
                 pathIndex = 0;
                 //go back to case 1
                 currentState = 1;
                 break;
         }

        //Vector3Int thisV3I = Vector3Int.FloorToInt(this.transform.position);
        //Vector3Int targetV3I = Vector3Int.FloorToInt(target);
        //currentPath = pathfinding.npcPathfind(thisV3I, targetV3I, nonWalkable, grid);
        //Debug.Log(currentPath[0].worldPosition);
    }
}
