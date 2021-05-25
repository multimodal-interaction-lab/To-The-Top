using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject blockToSpawn;
    GameObject menuObject;  // grandparent of button
    BlockSpawnMenu menuScript; // script component of menu object

    private void Start()
    {
        menuObject = transform.parent.parent.gameObject;
        menuScript = menuObject.GetComponent<BlockSpawnMenu>();
    }

    // Tell menu this button was pressed and which block to spawn
    public void Pressed()
    {
        menuScript.SetPressedButton(transform.parent.gameObject);
        menuScript.SetBlockToSpawn(blockToSpawn);
    }
}
