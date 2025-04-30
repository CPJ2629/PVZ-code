using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Plant currentPlant;
    private void OnMouseDown()
    {
        HandManager.instance.OnCellClick(this);
    }

    public bool AddPlant(Plant plant)
    {
        if (currentPlant != null) return false;
        currentPlant = plant;
        currentPlant.transform.position = transform.position;
        plant.trantoEnable(); //c#对象赋值后指向同一处空间
        return true;
    }

}
