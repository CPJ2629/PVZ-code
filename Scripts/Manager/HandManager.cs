using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager instance { get; private set; }
    public List<Plant> plantList;
    private Plant currentPlant;

    private void Awake()
    {
        instance = this; 
    }

    private void Update()
    {
        FollowCursor();
    }

    public bool AddPlant(PlantType plantType)
    {
        if (currentPlant != null) return false;

        Plant plantTypePrefab = GetPlant(plantType);
        if (plantTypePrefab == null)
        {
            print("³ýÈ¥");
            return false;
        }
        currentPlant=GameObject.Instantiate(plantTypePrefab);
        return true;
    }

    private Plant GetPlant(PlantType plantType)
    {
        foreach(Plant plant in plantList)
        {
            if (plant.plantType == plantType) return plant;
        }
        return null;
    }

    void FollowCursor()
    {
        if (currentPlant == null) return;

        Vector3 mouseWorldPosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        currentPlant.transform.position = mouseWorldPosition;
    }

    public void OnCellClick(Cell cell)
    {
        if (currentPlant == null) return;

        bool isSuccess = cell.AddPlant(currentPlant);
        if (isSuccess)
        {
            currentPlant = null;
        }

        //currentPlant.transform.position = cell.transform.position;
    }
}
