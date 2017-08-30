//Handles building of turrets. Ensures that a turret is selected and that the player has enough
//gold to build a turret before instantiating the turret prefab.
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    public GameObject standardTurretPrefab;
    public GameObject missileTurretPrefab;

    private TurretBlueprint turretToBuild;

    //Makes sure only one buildmanager is instantiated.
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    //Boolean functions for other classes; checks if a turret is selected or if money is available.
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    //Builds currently selected turret on the node passed in, subtracting the appropriate amount of gold from player stats.
    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret Built, money left: " + PlayerStats.Money);
    }

    //Sets turret to build.
    public void SelectTurretToBuild (TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
