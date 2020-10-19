using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public GameObject thisObject;
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
