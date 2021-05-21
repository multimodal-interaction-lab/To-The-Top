using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Attach this script to a despawn plane which should have a collider with trigger enabled and a rigidbody that is kinematic.
 * The despawn plane should be invisible and have a parent, child, or sibling that is a visible plane with a mesh collider to stop the object
 */

public class Boundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Called when a collision is detected.
    private void OnTriggerEnter(Collider other)
    {

        // if the colliding component is attached to a block with a BuildingBlock script its Despawn function is called
        if (other.gameObject.TryGetComponent(typeof(BuildingBlock), out Component component))
        {
            ((BuildingBlock)component).Despawn() ;
        }
    }
}
