﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    public float speed = 10f;
    float RotatingSpeed, timer;
    bool returning;
    public Vector3 Destination, playerRelativePos;
    public CharacterControl Owner; //TODO: rewrite this to refer to gameObject instead of script
    private PlayerInventory Inv;

    void Start()
    {
        returning = false;
        RotatingSpeed = 1024;
        timer = 8;
    }

    void Update()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position; //TODO: rewrite this to use Owner
        playerRelativePos = new Vector3(playerPos.x, transform.position.y, playerPos.z);

        //rotates a 'RotatingSpeed' degrees per second around y axis
        transform.Rotate(0, 0, RotatingSpeed * Time.deltaTime, Space.Self); 

        if (returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerRelativePos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, Destination) < 3)
            {
                returning = true;
            }
        }
        //if the boomerang glitches out, then the timer will reach zero. when it does it returns the boomerang to the player
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Inv.Inventory[(int)Weapon.Boomerang]++;
            Destroy(gameObject);
        }
    }

    public void SetDestination(Vector3 destination)
    {
        Destination = destination;
    }

    public void SetOwner(CharacterControl owner, PlayerInventory inv)
    {
        Owner = owner;
        Inv = inv;
    }

    void OnCollisionEnter(Collision target)
    {
        IStunnable stunnable = target.gameObject.GetComponent<IStunnable>();

        if (stunnable != null)
        {
            stunnable.getStunned();
            returning = true;
        }

        if (target.gameObject.tag == "Player" && returning)
        {
            Inv.Inventory[(int)Weapon.Boomerang]++;
            Destroy(gameObject);
        }
        else
        {
            returning = true;
        }
    }
}
