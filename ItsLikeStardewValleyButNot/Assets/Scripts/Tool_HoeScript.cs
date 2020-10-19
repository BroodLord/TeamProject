using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_HoeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            {
                if (hit.transform.tag == ("Floor"))
                {

                }
            }
        }
    }
}
