//Handles the nodes (tiles / buildspots) in game.
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Color errorColor;
    public Vector3 positionOffset;

    [Header("Optional")]  //Allows a turret to be pre-placed to help the player before the game starts.
    public GameObject turret;

    private Color startColor;
    private Renderer rend;

    BuildManager buildManager;

    //Instantiates the buildmanager, and sets the startColor (the color of the nodes without any changes).
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    //Helper function to properly place turrets.
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    //Changes color of the node when the mouse enters based on whether a turret can be built, and if a player has enough money.
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = errorColor;
        }
    }

    //Builds a turret on mouse click if possible.
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (turret != null)
        {
            Debug.Log("Can't build there! - TODO: Display on screen");
            return;
        }

        buildManager.BuildTurretOn(this);
    }

    //Returns node to original color on mouse exiting the node area.
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
