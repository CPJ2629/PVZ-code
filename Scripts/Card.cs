using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

enum CardState { Colding, WaitingSun, Ready, Disable }
public enum PlantType { SunFlower,PeaShoot}

public class Card : MonoBehaviour
{
    private CardState cardState = CardState.Disable;
    public GameObject cardLight;
    public GameObject cardGray;
    public Image cardMask;
    public PlantType plantType = PlantType.SunFlower;

    [SerializeField]
    private float CDTime = 2;
    private float CDTimer = 0;

    [SerializeField]
    private int needSunPoint=50;

    private void Update()
    {
        switch (cardState)
        {
            case CardState.Colding:
                ColdingUpdate();
                break;
            case CardState.WaitingSun:
                WaitingSunUpdate();
                break;
            case CardState.Ready:
                ReadyUpdate();
                break;
            default:
                break;
        }
    }

    public void ColdingUpdate()
    {
        CDTimer += Time.deltaTime;
        cardMask.fillAmount = (CDTime - CDTimer) / CDTime;

        if (CDTimer >= CDTime) TrantoWaitingSun();
    }

    public void WaitingSunUpdate()
    {
        if (needSunPoint <= SunManager.instance.Sunpoint) TrantoReady();
    }

    public void ReadyUpdate()
    {
        if (needSunPoint > SunManager.instance.Sunpoint) TrantoWaitingSun();
    }

    public void TrantoWaitingSun()
    {
        cardState = CardState.WaitingSun;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(false);
    }

    public void TrantoReady()
    {
        cardState = CardState.Ready;
        cardLight.SetActive(true);
        cardGray.SetActive(false);
        cardMask.gameObject.SetActive(false);
    }

    public void TrantoCooling()
    {
        cardState = CardState.Colding;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(true);
        CDTimer = 0;
    }

    public void OnClick()
    {
        if (cardState == CardState.Disable) return;
        if (needSunPoint > SunManager.instance.Sunpoint) return;

        bool isSuccess=HandManager.instance.AddPlant(plantType);
        if (isSuccess)
        {
            SunManager.instance.SubSun(needSunPoint);
            TrantoCooling();
        }

    }

    public void DisableCard()
    {
        cardState = CardState.Disable;
    }

    public void EnableCard()
    {
        TrantoCooling();
    }
}

