using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    public float shootDur = 2;
    private float shootTimer = 0;
    public Transform shootPointTransform;
    public PeaBullet peaBulletPrefab;

    public float bulletSpeed = 5;
    protected override void EnableUpdate()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootDur)
        {
            Shoot();
            shootTimer = 0;
        }
    }

    void Shoot()
    {
        PeaBullet pea = GameObject.Instantiate(peaBulletPrefab,shootPointTransform.position,Quaternion.identity);
        pea.setSpeed(bulletSpeed);
    }
}
