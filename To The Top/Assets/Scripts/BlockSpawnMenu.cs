using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnMenu : MonoBehaviour
{
    public GameObject[] spawnButtons;
    public GameObject spawnPoint;

    GameObject topButton;
    GameObject middleButton;
    GameObject bottomButton;
    GameObject pressedButton;
    GameObject blockToSpawn;
    

    void Start()
    {
        topButton = transform.Find("Top Button").gameObject;
        middleButton = transform.Find("Middle Button").gameObject;
        bottomButton = transform.Find("Bottom Button").gameObject;

        InstantiateButton(topButton);
        InstantiateButton(middleButton);
        InstantiateButton(bottomButton);
    }

    void InstantiateButton(GameObject spawnPoint)
    {
        GameObject spawnedButton;
        int i = Random.Range(0, spawnButtons.Length);
        spawnedButton = Instantiate(spawnButtons[i], spawnPoint.transform.position, spawnPoint.transform.rotation, spawnPoint.transform);
        
        if (spawnButtons[i].name.Equals("ArchSpawnButton"))
        {
            spawnedButton.transform.localPosition += new Vector3(-0.048f, 0f, 0f);
        }
    }

    // called by SpawnButton
    public void SetPressedButton(GameObject button)
    {
        pressedButton = button;
    }

    // called by SpawnButton
    public void SetBlockToSpawn(GameObject block)
    {
        blockToSpawn = block;
    }

    void ReplaceButton()
    {
        Destroy(pressedButton.transform.GetChild(0));
        int i = Random.Range(0, spawnButtons.Length);
        InstantiateButton(pressedButton);
    }

    void FixedUpdate()
    {
        
    }
}
