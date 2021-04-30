using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


/*So turns out the Astar works and isn't the problem, unity has some dumb fucking scaling issues and we upscaled the whole fucking game so this fucker will work... fuck unity*/


public class NPCMovement : MonoBehaviour
{
    private AStar pathfinding;
    private Vector2 target;                 //location of waypoint that is being targeted, this is converted to an intager when calculating pathfinding 
    private Vector2[] waypoints;
    private Animator anim;
    private Tilemap nonWalkable;
    private Grid grid;
    //public TileBase Tile;
    //current path is a list of every cell from NPC to Target
    private List<Node> currentPath;         //calculated by the npcPathfind call in state 3
    [SerializeField] private int currentState;
    [SerializeField] private float NPCSpeed;
    private int pathIndex;                  //current cell on the path
    private int waypointIndex;              //current waypoint thats being targeted 
    private float timer = 2.0f;
    int[] Times;
    private int timeIndex;
    private XMLParser XML;

    enum MovementStates {Idle, Up, Down, Left, Right}
    MovementStates States;


    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        pathfinding = new AStar();
        currentState = 3;
        waypointIndex = 0;
        XML.NPCWaypoints.TryGetValue(this.name, out waypoints);

        target = waypoints[waypointIndex];
        nonWalkable = GameObject.FindGameObjectWithTag("Non-Walkable").GetComponent<Tilemap>();
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        Times = new int[] { 9, 10, 11, 12 };

        timeIndex = 0;


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
                //follow the determined path until it gets to the next cell in the path or the last one, then it will select the next waypoint to target
                if (pathIndex >= currentPath.Count)
                {
                    //waypoint target destination is reached
                    waypointIndex++;
                    target = waypoints[Random.Range(1, waypoints.Length) - 1];
                    currentState = 2;
                }
                else if (this.transform.position.x == currentPath[pathIndex].worldPosition.x + 0.5f && this.transform.position.y == currentPath[pathIndex].worldPosition.y + 0.5f)
                {
                    pathIndex++;
                }
                //where actual movement is done
                else
                {
                    Vector2 v2;
                    v2.x = currentPath[pathIndex].worldPosition.x + 0.5f;
                    v2.y = currentPath[pathIndex].worldPosition.y + 0.5f;
                    Vector2 v2pos;
                    v2pos.x = transform.position.x;
                    v2pos.y = transform.position.y;
                    Vector2 Dir = (v2 - v2pos).normalized;
                    if (Dir.y == 1)
                    {
                        anim.SetBool("Up", true);
                        anim.SetBool("Forward", false);
                        anim.SetBool("Right", false);
                        anim.SetBool("Left", false);
                    }
                    else { anim.SetBool("Up", false);  }
                    if (Dir.x == 1)
                    {
                        anim.SetBool("Right", true);
                        anim.SetBool("Forward", false);
                        anim.SetBool("Up", false);
                        anim.SetBool("Left", false);
                    }
                    else { anim.SetBool("Right", false); }
                    if (Dir.x == -1)
                    {
                        anim.SetBool("Left", true);
                        anim.SetBool("Forward", false);
                        anim.SetBool("Right", false);
                        anim.SetBool("Up", false);
                    }
                    else { anim.SetBool("Left", false); }
                    if (Dir.y == -1)
                    {
                        anim.SetBool("Forward", true);
                        anim.SetBool("Up", false);
                        anim.SetBool("Right", false);
                        anim.SetBool("Left", false);
                    }
                    else { anim.SetBool("Forward", false); }
                    this.transform.position = Vector2.MoveTowards(this.transform.position, v2, movement);
                }
                break;
            //Waiting state
            case 2:
                //just a timer to wait that needs to be replaced with checking the clock time
                anim.SetBool("Forward", false);
                anim.SetBool("Up", false);
                anim.SetBool("Left", false);
                anim.SetBool("Right", false);
                anim.SetBool("Idle", true);
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

                //NPC and target positions are turned to Vector3Ints as the A* cant use floats when converting to grid space
                //Mathf.CeilToInt()
                Vector3Int targetV3I = Vector3Int.FloorToInt(target);
                Vector3Int thisV3I = Vector3Int.FloorToInt(this.transform.position);
                //find and update new path
                currentPath = pathfinding.npcPathfind(thisV3I, targetV3I, nonWalkable, grid);


                if (currentPath == null)
                {
                    Debug.Log("shits gone tits up");
                    Debug.Break();
                }

                for (int i = 0; i < currentPath.Count - 1; i++)
                {
                    Debug.DrawLine(currentPath[i].worldPosition, currentPath[i + 1].worldPosition, Color.red, 50.0f);
                }

                //reset path index
                pathIndex = 0;
                //go back to case 1
                currentState = 1;
                anim.SetBool("Idle", false);
                break;
        }
    }
}
