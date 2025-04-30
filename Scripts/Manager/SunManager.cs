using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SunManager : MonoBehaviour
{
    public static SunManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    private int sunPoint;    
    public int Sunpoint
    {
        get { return sunPoint; }
    }

    public TextMeshProUGUI sunPointText;
    private Vector3 sunPointPos;
    float proceTimer=0;
    public float proceTime;
    public GameObject sunPrefab;
    private bool isStart = false;

    public void UpdateSunPointText()
    {
        sunPointText.text = Sunpoint.ToString();
    }

    private void Start()
    {
        UpdateSunPointText();
        CalSunPointPos();
        //StartProduce();
    }

    private void Update()
    {
        if (isStart) ProduceSun();
    }

    public void StartProduce()
    {
        isStart = true;
    }

    public void StopProduce()
    {
        isStart=false;
    }

    public void SubSun(int point)
    {
        sunPoint -= point;
        UpdateSunPointText();
    }

    public void AddSun(int point)
    {
        sunPoint += point;
        UpdateSunPointText();
    }

    public Vector3 GetSunPointPos()
    {
        return sunPointPos;
    }

    private void CalSunPointPos()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(sunPointText.transform.position);
        position.z = 0;
        sunPointPos = position;
    }

    public void ProduceSun()
    {
        proceTimer += Time.deltaTime;
        if (proceTimer >= proceTime)
        {
            proceTimer = 0;
            Vector3 position = new Vector3(Random.Range(-5, 6.5f), 6.2f, -1);
            GameObject randSun=GameObject.Instantiate(sunPrefab,position,Quaternion.identity);

            position.y = Random.Range(-3, 4f);
            randSun.GetComponent<Sun>().LinerTo(position);
        }
    }
}
