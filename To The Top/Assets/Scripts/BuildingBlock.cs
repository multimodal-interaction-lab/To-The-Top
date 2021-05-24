using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour
{

    Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Spawn();
    }


    void FixedUpdate()
    {
    }

    public void BlockGrasped()
    {
        // Call parent spawn point to generate another block as replacement
        if (transform.parent != null)
        {
            if (gameObject.tag.Equals("BlockInSpawn"))
            {
                // Only spawn replacement if taking from spawn point
                StartCoroutine(RequestReplacementCoroutine());
            }
        }

        gameObject.tag = "BlockInHand";
        rigidbody.isKinematic = false;
    }
    IEnumerator RequestReplacementCoroutine()
    {
        // Wait before calling the BlockGenerator spawn
        yield return new WaitForSeconds(0.8f);
        BlockGenerator blockGenerator = gameObject.GetComponentInParent(typeof(BlockGenerator)) as BlockGenerator;
        blockGenerator.SpawnObject();
        yield return null;
    }
    public void BlockReleased()
    {
        gameObject.tag = "BlockInPlay";
        rigidbody.isKinematic = false;
    }

    // Block will grow
    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        // Store the default scale as the final scale
        Vector3 finalScale = transform.localScale;
        transform.localScale = transform.localScale * 0.0001f;

        // Increase the scale to the correct size
        while (Mathf.Abs(finalScale.x - transform.localScale.x) > 0.0001f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, finalScale, .2f);
            yield return new WaitForSeconds(.05f);
        }
        yield return null;
    }

    // Block will shrink and then disappear
    public void Despawn()
    {
        StartCoroutine(DespawnCoroutine());
    }

    IEnumerator DespawnCoroutine()
    {
        while (transform.localScale.x > .01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), .2f);
            yield return new WaitForSeconds(.05f);
        }
        Destroy(gameObject);
        yield return null;
    }
}
