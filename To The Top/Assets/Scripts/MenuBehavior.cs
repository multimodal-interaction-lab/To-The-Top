using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    // Each list represents the components of a 'screen' in the menu
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

        Debug.Log("Finished hiding All Elements");
    }

    // Switches to MM screen
    public void menuMM()
    {
        menuHideAllElements();          // Hides all elements
        showObjectList(ref MMObjects);  // Shows only the elements in the main menu

        Debug.Log("Showing main menu screen");
    }

    // Switches to multiplayer select screen
    public void menuMP()
    {
        menuHideAllElements();
        showObjectList(ref MPObjects);
        Debug.Log("Showing multiplayer select screen");
    }

    // Switches to create room screen
    // On hold until multiplayer testing is done
    public void menuCR()
    {
        Debug.Log("Showing create room screen");
    }

    // Switches to join room screen
    // On hold until multiplayer testing is done
    public void menuJR()
    {
        Debug.Log("Showing join room screen");
    }

    // Switches to settings screen
    // On hold
    public void menuST()
    {
        menuHideAllElements();
        showObjectList(ref STObjects);
        Debug.Log("Showing settings screen");
    }   

    // Switches to credits screen
    public void menuCD()
    {
        menuHideAllElements();
        showObjectList(ref CDObjects);
        Debug.Log("Showing Credits");
    }

    // Switches to quit game confirmation screen
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
        Debug.Log("Attempting to quit game...");
        Application.Quit();
    }

    // Will initialize a local singleplayer game
    // On hold until we have a singleplayer scene set up
    public void startSPGame()
    {
        Debug.Log("Attempted to start a SP game");
    }

    // Joins random MP game
    // In progress...
    public void joinMPGame()
    {
        Debug.Log("Attempted to join a random MP game");
    }
}
