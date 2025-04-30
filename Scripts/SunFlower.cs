using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{

    public float ProceDur = 5;
    private float proceTimer = 0;
    private Animator animator;
    public GameObject SunPrefab;
    public float jumpMin = 0.3f;
    public float jumpMax = 1.5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void EnableUpdate()
    {
        proceTimer += Time.deltaTime;
        if (proceTimer >= ProceDur)
        {
            proceTimer = 0;
            animator.SetTrigger("isGlowing");
        }
    }

    public void BornSun()
    {
        GameObject go = GameObject.Instantiate(SunPrefab, transform.position, Quaternion.identity);

        float dis=Random.Range(jumpMin, jumpMax);
        dis = Random.Range(0, 2) < 1 ? -dis : dis;

        Vector3 vec = transform.position;
        vec.x += dis;

        go.GetComponent<Sun>().JumpTo(vec);
    }


}
