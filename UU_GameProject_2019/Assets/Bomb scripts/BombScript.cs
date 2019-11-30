﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float fuseLength;
    public float blastRadius;

    float remainingFuse;
    bool hasExpoded;

    // Start is called before the first frame update
    void Start()
    {
        remainingFuse = fuseLength;
        hasExpoded = false;
    }

    // Update is called once per frame
    void Update()
    {
        remainingFuse -= Time.deltaTime;
        if (remainingFuse <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        if(!hasExpoded)
        {
            hasExpoded = true;
            Collider[] bombTargets = Physics.OverlapSphere(this.transform.position, blastRadius);
            for (int i = 0; i < bombTargets.Length; i++)
            {
                IExplodable explodable = bombTargets[i].gameObject.GetComponent<IExplodable>();
                BombScript otherBomb = bombTargets[i].gameObject.GetComponent<BombScript>();

                if (explodable != null)
                    explodable.getExploded();

                if (otherBomb != null)
                    otherBomb.Explode();

            }

            Destroy(gameObject);
        }

        
    }
}
