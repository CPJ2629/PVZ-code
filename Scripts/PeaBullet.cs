using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{

    private float speed = 3;
    public int atk = 20;
    public GameObject peaBulletHitPrefab;
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void Start()
    {
        Destroy(this.gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            Destroy(this.gameObject);
            collision.GetComponent<Zombie>().TakeDamage(atk);
            GameObject go=GameObject.Instantiate(peaBulletHitPrefab,transform.position, Quaternion.identity);
            Destroy(go, 1);
        }
    }

}
