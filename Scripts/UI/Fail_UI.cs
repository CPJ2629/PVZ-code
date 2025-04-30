using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Fail_UI : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim=GetComponent<Animator>();
        anim.enabled = false;
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        anim.enabled=true;
    }

    public void Hide()
    {
        anim.enabled = false;
    }
}
