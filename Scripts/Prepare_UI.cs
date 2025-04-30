using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prepare_UI : MonoBehaviour
{
    private Animator anim;
    private Action OnComplete;
    private void Start()
    {
        anim= GetComponent<Animator>();
        anim.enabled = false;
    }

    public void Show(Action onComplete)
    {
        this.OnComplete = onComplete;
        anim.enabled = true;
    }
    
    void OnShowComplete()
    {
        OnComplete?.Invoke();
    }
}
