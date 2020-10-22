using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public GameObject thisObject;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float interactRange;
    private static float actionLock = 0.4f;         //how long after executing an action the character is locked in place for 
    ToolScript tool;                                //tool that is currently being used 
    public Camera camera;
    private Vector3 mouseWorldPoint;
    private bool actionLocked;
    private float actionLockedTimer = actionLock;


    //TODO - when an item list is added, check currently equipped item, if its a tool set the correct tool
    // Start is called before the first frame update
    void Start()
    {
        tool = this.GetComponent<HoeScript>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (actionLocked)
        {
            
            actionLockedTimer -= Time.deltaTime;
            if (actionLockedTimer < 0)
            {
                actionLocked = false;
                actionLockedTimer = actionLock;
            }
        }


        if (Input.GetKey(KeyCode.Z))
        {
            tool = this.GetComponent<WateringCanScript>();
        }

        if (Input.GetKey(KeyCode.X))
        {
            tool = this.GetComponent<HoeScript>();
        }

        if (!actionLocked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseWorldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(this.transform.position, mouseWorldPoint) < interactRange)
                {
                    actionLocked = true;
                    tool.useTool();
                }
            }

            if (Input.GetKey(KeyCode.W))
            {
                thisObject.transform.position += new Vector3(0, moveSpeed + Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.A))
            {
                thisObject.transform.position -= new Vector3(moveSpeed + Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                thisObject.transform.position -= new Vector3(0, moveSpeed + Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                thisObject.transform.position += new Vector3(moveSpeed + Time.deltaTime, 0, 0);
            }
        }
    }
}
