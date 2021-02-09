using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempController : MonoBehaviour
{
    // Start is called before the first frame update
    private JunkPlacer junk;
    void Start()
    {
        junk = GameObject.FindGameObjectWithTag("Turnip").GetComponent<JunkPlacer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            junk.PlaceTrees();
        }
    }
}
