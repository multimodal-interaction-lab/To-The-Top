using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour
{

    
    void Start()
    {
    }

   
    void FixedUpdate()
    {
        Rigidbody rigbody = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;

        Debug.Log("Tag: " + gameObject.tag);
        Debug.Log("Kinematic: " + rigbody.isKinematic);
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
