﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleMovement : MonoBehaviour
{
    protected Vector3 spawnPosition, hidePosition;
    protected GameObject sand;
    protected float delta, speed;
    public bool tilesPressed;
    
    // Setting initial variables.
    void Start()
    {
        spawnPosition = transform.position;
        hidePosition = new Vector3(transform.position.x, transform.position.y - delta, transform.position.z);
        delta = 8;
        speed = 1;
        tilesPressed = false;
        sand = GameObject.Find("EntranceSand");
    }

    /// <summary>
    /// Checks if both static bools are set to true, if so sets tilesPressed to true.
    /// Switchings to a camera in front of the temple.
    /// Switches the camera back if the temple is fully spawned.
    /// </summary>
    void Update()
    {
        if (CheckLeftTile.leftTilePressed && CheckRightTile.rightTilePressed) { tilesPressed = true; }
        if (tilesPressed) { SetToSpawnPosition(); SwitchCamera.activeCamera = "secondCamera"; ; }
        else { SetToHidePosition(); SwitchCamera.activeCamera = "mainCamera"; }

        if (Vector3.Distance(spawnPosition, transform.position) < 0.1f) { SwitchCamera.activeCamera = "mainCamera"; }
    }

    /// <summary>
    /// Moves the temple and the partile effect to the spawn position.
    /// </summary>
    protected void SetToSpawnPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, spawnPosition, speed * Time.deltaTime);
        sand.transform.position = Vector3.MoveTowards(sand.transform.position, new Vector3(sand.transform.position.x, 0, sand.transform.position.z), speed * Time.deltaTime);
    }

    /// <summary>
    /// Method to reset the position of the temple to the start position.
    /// </summary>
    protected void SetToHidePosition()
    {
        if (Vector3.Distance(transform.position, hidePosition) < 0.2f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - delta, transform.position.z); 
        }
    }
}
