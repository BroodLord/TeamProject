﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCollisions : MonoBehaviour
{
    public string LevelName;
    public LoadLevel Load;
    [SerializeField] public Vector3 LoadPosition;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Load = GameObject.FindGameObjectWithTag("LoadManager").GetComponent<LoadLevel>();
        Load.TransferLevel(LevelName, LoadPosition);
    }
}
