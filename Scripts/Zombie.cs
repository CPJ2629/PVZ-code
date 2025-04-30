using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieState { Move,Eat,Die,Pause}

public class Zombie : MonoBehaviour
{
    private Rigidbody2D rgd;
    public float moveSpeed = 2;
    private Animator anim;
    public ZombieState zombieState=ZombieState.Move;

    public int atkValue;
    public float atkDur = 2;
    private float atkTimer = 0;

    private Plant curEatPlant;
    public int HP = 100;
    public int curHP;
    public GameObject zombieHeadPrefab;
    private bool done = false;

    void Start()
    {
        rgd=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        curHP = HP;
    }

    void Update()
    {
        switch (zombieState)
        {
            case ZombieState.Move:
                MoveUpdate();
                break;
            case ZombieState.Eat:
                EatUpdate();
                break;
            case ZombieState.Die:
                DieUpdate();
                break;
            default:
                break;
        }
    }

    private void MoveUpdate()
    {
        rgd.MovePosition(rgd.position + Vector2.left * moveSpeed * Time.deltaTime);

    }

    private void EatUpdate()
    {
        atkTimer+= Time.deltaTime;
        if(atkTimer > atkDur && curEatPlant!=null)
        {
            curEatPlant.TakeDamage(atkValue);
            atkTimer = 0;
        }

    }


    private void DieUpdate()
    {
        

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "plant")
        {
            anim.SetBool("is_attacking", true);
            TrantoEat();
            curEatPlant =collision.GetComponent<Plant>();
        }

        else if(collision.tag == "House")
        {
            GameManager.Instance.GameEndFail();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "plant")
        {
            anim.SetBool("is_attacking", false);
            zombieState = ZombieState.Move;
        }
    }

    void TrantoEat()
    {
        zombieState = ZombieState.Eat;
        atkTimer = 0;
    }

    public void TakeDamage(int damge)
    {
        if (curHP <= 0) return;

        this.curHP -= damge;
        if (curHP <= 0)
        {
            curHP = -1;
            Dead();
        }
        float hpPercent = curHP*1f / HP;
        anim.SetFloat("HPPercent", hpPercent);
        if (hpPercent < .5f && !done)
        {
            GameObject go = GameObject.Instantiate(zombieHeadPrefab,transform.position, Quaternion.identity);
            Destroy(go, 2);
            done = true;
        }
    }

    private void Dead()
    {
        if (zombieState == ZombieState.Die) return;
        zombieState = ZombieState.Die;
        GetComponent<Collider2D>().enabled = false;
        ZombieManager.instance.RemoveZombie(this);

        Destroy(this.gameObject, 2);
    }

    public void TrantoPause()
    {
        zombieState = ZombieState.Pause;
        anim.enabled = false;
        rgd.bodyType = RigidbodyType2D.Static;
    }
}
