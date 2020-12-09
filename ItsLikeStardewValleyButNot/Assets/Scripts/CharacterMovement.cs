using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.Tilemaps;
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
    private Vector3 mouseWorldPoint;
    private bool actionLocked;
    private float actionLockedTimer = actionLock;


    //TODO - when an item list is added, check currently equipped item, if its a tool set the correct tool
    // Start is called before the first frame update

    private void Awake()
    {

        if (GameObject.FindGameObjectWithTag("InventoryManager") != null)
        {
            cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryClass>();
        }
    }


    void Start()
    {
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



        if (!actionLocked)
        {

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
                    cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryClass>();
                    cInventory.DisabledNEnable();
                    CoolDown = 0.2f;
                }
            }
        }
    }
}
