using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Prepare_UI prepareUI;
    public CardListUI cardListUI;
    public Fail_UI failUI;
    private bool isEnd = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameStart();
    }

    void GameStart()
    {
        Vector3 curPosition = Camera.main.transform.position;
        Camera.main.transform.DOPath(new Vector3[] { curPosition, new Vector3(4f, 0, -10), curPosition }, 4, PathType.Linear).OnComplete(ShowPrepareUI);
    }

    void ShowPrepareUI()
    {
        prepareUI.Show(OnPrePareUIComlete);
    }

    private void OnPrePareUIComlete()
    {
        SunManager.instance.StartProduce();
        cardListUI.ShowCardList();
        ZombieManager.instance.StartCoroutine(ZombieManager.instance.SpawnZombie());
    }

    public void GameEndFail()
    {
        if (isEnd==true) return;
        isEnd = true;
        failUI.enabled = true;
        failUI.Show();
        ZombieManager.instance.Pause();
        cardListUI.DisableCardList();
        SunManager.instance.StopProduce();
    }

    public void GameEndWin()
    {
        if (isEnd == true) return;
        isEnd = true;


    }
}
