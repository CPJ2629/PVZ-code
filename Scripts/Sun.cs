using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sun : MonoBehaviour
{
    public float moveDur = 1;
    public int point = 50;
    
    public void LinerTo(Vector3 targetPos)
    {
        transform.DOMove(targetPos, moveDur);
    }

    public void JumpTo(Vector3 pos)
    {
        Vector3 center = (pos + transform.position) / 2;
        float distance = Vector3.Distance(transform.position, pos);
        center.y += (distance / 2);

        transform.DOPath(new Vector3[] { transform.position, center, pos }, moveDur, PathType.CatmullRom).SetEase(Ease.OutQuad); 
    }

    public void OnMouseDown()
    {
        transform.DOMove(SunManager.instance.GetSunPointPos(), moveDur).SetEase(Ease.OutQuad).OnComplete(
            () =>{ 
                Destroy(this.gameObject);
                SunManager.instance.AddSun(point);
                 }
            );
    }
}

