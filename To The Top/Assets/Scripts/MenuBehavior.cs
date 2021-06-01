using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    // Each list represents the components of a 'screen' in the menu
    //public List<GameObject> SharedObjects;  // Includes back buttons which are shared by multiple screens
    // 0 -> BackToMM button, 1-> BackToMP button
    public GameObject BackToMM;
    public GameObject BackToMP;
    public List<GameObject> MMObjects;      // Main menu screen
    public List<GameObject> MPObjects;      // Multiplayer mode selection screen
    public List<GameObject> CRObjects;      // Create room screen
    public List<GameObject> JRObjects;      // Join room screen
    public List<GameObject> STObjects;      // Settings screen
    public List<GameObject> CDObjects;      // Credits screen
    public List<GameObject> QGObjects;      // Confirmation screen for quitting the game
    
    // Triggers at the beginning of the scene opening
    void Start()
    {
        menuMM();   // Shows main menu, which also hides each other menu object
        Debug.Log("Starting menu behavior script");
    }

    // Sets the visibility of each element to false, effectively clearing the menu
    void menuHideAllElements()
    {
        // Hides each of the objects in each list
        hideObjectList(ref MMObjects);
        hideObjectList(ref MPObjects);
        hideObjectList(ref CRObjects);
        hideObjectList(ref JRObjects);
        hideObjectList(ref STObjects);
        hideObjectList(ref CDObjects);
        hideObjectList(ref QGObjects);
        //hideObjectList(ref SharedObjects);
        BackToMM.SetActive(false);
        BackToMP.SetActive(false);

        Debug.Log("Finished hiding All Elements");
    }

    // Switches to MM screen
    public void menuMM()
    {
        menuHideAllElements();  // Hides all elements
        showObjectList(ref MMObjects);  // Shows only the elements in the main menu

        Debug.Log("Showing Main Menu");
    }

    // Switches to credits screen
    public void menuCD()
    {
        menuHideAllElements();
        showObjectList(ref CDObjects);
        //SharedObjects[0].SetActive(true);
        BackToMM.SetActive(true);
        Debug.Log("Showing Credits");
    }

    // Switches to multiplayer select screen
    public void menuMP()
    {
        menuHideAllElements();
        showObjectList(ref MPObjects);
        //SharedObjects[0].SetActive(true);
        BackToMM.SetActive(true);
        Debug.Log("Showing Multiplayer");
    }

    public void menuQG()
    {
        menuHideAllElements();
        showObjectList(ref QGObjects);
        Debug.Log("Showing quit game confirmation");
    }

    // Makes each object in the list visible
    void showObjectList(ref List<GameObject> objList)
    {
        foreach(var obj in objList)
        {
            obj.SetActive(true);
        }
    }

    // Makes each object in the list hidden
    void hideObjectList(ref List<GameObject> objList)
    {
        foreach (var obj in objList)
        {
            obj.SetActive(false);
        }
    }

    // Exits the game
    public void quitGame()
    {
        Application.Quit();
    }
}
