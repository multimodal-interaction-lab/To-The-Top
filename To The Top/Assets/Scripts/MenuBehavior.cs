using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    public bool MainMenuVisible = true;
    public bool CreateGameVisible = false;
    public bool JoinGameVisible = false;
    public bool CreditsVisible = false;
    public bool TitleVisible = true;

    public GameObject CreateGameUI;
    public GameObject JoinGameUI;
    public GameObject CreditsUI;
    public GameObject TitleText;

    // Start is called before the first frame update
    void Start()
    {
        CreateGameUI.SetActive(CreateGameVisible);
        JoinGameUI.SetActive(JoinGameVisible);
        CreditsUI.SetActive(CreditsVisible);
        TitleText.SetActive(TitleVisible);
        // For MainMenuVisible, we'll should do SetActive to the children, just in case we need to spawn in the scene with it hidden
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // We'll probably want functions to enable or disable the various menus so that we can tie them to the buttons
}
