﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour
{

    public GameObject spawnMenuObject;
    public GameObject spawnPointObject;

    Rigidbody rigidbody;
    bool unplaced = true;
    bool despawning = false;
    float timeFromDespawn;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Spawn();
    }

    private void FixedUpdate()
    {
        // Fail safe destruction if the despawn coroutine doesn't work
        if (despawning)
        {
            if (Time.time - timeFromDespawn > 0.3f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void BlockGrasped()
    {
        gameObject.tag = "BlockInHand";
        rigidbody.isKinematic = false;
    }
    
    public void BlockReleased()
    {
        // Call parent spawn point to generate another block as replacement
        if (transform.parent != null)
        {
            if (unplaced)
            {
                // Only spawn replacement if taking from spawn point
                // StartCoroutine(RequestReplacementCoroutine());
            }
        }
        gameObject.tag = "BlockInPlay";
        rigidbody.isKinematic = false;
        unplaced = false;
    }

    // Block will grow
    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    public IEnumerator SpawnCoroutine()
    {
        // Store the default scale as the final scale
        Vector3 finalScale = transform.localScale;
        transform.localScale = transform.localScale * 0.001f;

        // Increase the scale to the correct size
        while (Mathf.Abs(finalScale.x - transform.localScale.x) > 0.0001f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, finalScale, .2f);
            yield return new WaitForSeconds(.01f);
        }
    }

    // Block will shrink and then disappear
    public void Despawn()
    {
        despawning = true;
        timeFromDespawn = Time.time;
        gameObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(DespawnCoroutine());
    }
    
    public IEnumerator DespawnCoroutine()
    {
        yield return null;
        while (transform.localScale.x > .01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), .2f);
            yield return new WaitForSeconds(.01f);
        }
        Destroy(gameObject);
    }
}
