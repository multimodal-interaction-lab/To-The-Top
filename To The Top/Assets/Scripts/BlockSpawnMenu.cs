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

    bool responsive;

    void Start()
    {
        responsive = true;

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
        else if (spawnButtons[i].name.Equals("BoxSpawnButton"))
        {
            spawnedButton.transform.localPosition += new Vector3(-0.0367f, 0f, 0f);
        }
    }


    // called by SpawnButton when pressed
    public void ButtonPressed(GameObject button, GameObject block)
    {
        if (responsive)
        {
            responsive = false;
            pressedButton = button;
            blockToSpawn = block;
            spawnPoint.GetComponent<BlockGenerator>().ReplaceBlock(blockToSpawn);
            StartCoroutine(WaitCoroutine());
        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        responsive = true;
    }

    public void ReplaceButton()
    {
        // Destroy all buttons
        for (int i = 0; i < pressedButton.transform.childCount; i++) {
            Destroy(pressedButton.transform.GetChild(i).gameObject);
        }
        // Place new putton
        InstantiateButton(pressedButton);
    }

}
