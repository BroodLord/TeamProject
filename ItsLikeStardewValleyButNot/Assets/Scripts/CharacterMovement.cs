﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public GameObject thisObject;
    public Animator PlayerAnim;
    private float CoolDown;
    [SerializeField] InventoryClass cInventory;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float interactRange;
    private static float actionLock = 0.4f;         //how long after executing an action the character is locked in place for 
    ToolScript tool;                                //tool that is currently being used 
    public Camera camera;
    private Vector3 mouseWorldPoint;
    private bool actionLocked;
    private float actionLockedTimer = actionLock;
    JunkPlacer junkPlace;

    //TODO - when an item list is added, check currently equipped item, if its a tool set the correct tool
    // Start is called before the first frame update

    public void GetCamera()
    {
        GameObject Go = GameObject.FindGameObjectWithTag("MainCamera");
        camera = Go.GetComponent<Camera>();
    }

    void Start()
    {
        junkPlace = this.GetComponent<JunkPlacer>();
        GetCamera();
        tool = this.GetComponent<HoeScript>();
        CoolDown = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(CoolDown > 0.0f)
        {
            CoolDown -= Time.deltaTime;
        }
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

        if (Input.GetKey(KeyCode.O))
        {
            junkPlace.PlaceTrees();
        }

        if (!actionLocked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (camera == null) { GetCamera(); }
                mouseWorldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(this.transform.position, mouseWorldPoint) < interactRange)
                {
                    actionLocked = true;
                    tool.useTool();
                }
            }

            if (Input.GetKey(KeyCode.W))
            {
                PlayerAnim.SetBool("Up", true);
                thisObject.transform.position += new Vector3(0, moveSpeed + Time.deltaTime, 0);
            }
            else { PlayerAnim.SetBool("Up", false); }
            if (Input.GetKey(KeyCode.A))
            {
                PlayerAnim.SetBool("Left", true);
                thisObject.transform.position -= new Vector3(moveSpeed + Time.deltaTime, 0, 0);
            }
            else { PlayerAnim.SetBool("Left", false); }
            if (Input.GetKey(KeyCode.S))
            {
                PlayerAnim.SetBool("Forward", true);
                thisObject.transform.position -= new Vector3(0, moveSpeed + Time.deltaTime, 0);

            }
            else { PlayerAnim.SetBool("Forward", false); }
            if (Input.GetKey(KeyCode.D))
            {
                PlayerAnim.SetBool("Right", true);
                thisObject.transform.position += new Vector3(moveSpeed + Time.deltaTime, 0, 0);
            }
            else { PlayerAnim.SetBool("Right", false); }
            if (Input.GetKey(KeyCode.I))
            {
                if (CoolDown <= 0.0f)
                {
                    cInventory.DisabledNEnable();
                    CoolDown = 0.2f;
                }
            }
        }
    }
}
