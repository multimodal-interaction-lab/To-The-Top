using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
