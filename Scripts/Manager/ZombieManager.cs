using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnState { NotStart, Spawning, End }
public class ZombieManager : MonoBehaviour
{
    public static ZombieManager instance {  get; private set; }
    private SpawnState spawnState = SpawnState.NotStart;
    public Transform[] spawnPointList;
    public GameObject zombiePrefab;

    private List<Zombie> zombies=new List<Zombie>();

    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        StartSpawn();
    }

    private void Update()
    {
        if (spawnState == SpawnState.End && zombies.Count == 0)
        {
            GameManager.Instance.GameEndWin();
        }
    }

    public void StartSpawn()
    {
        spawnState = SpawnState.Spawning;
        //StartCoroutine(SpawnZombie());
    }

    public void Pause()
    {
        spawnState = SpawnState.End;
        foreach(Zombie zombie in zombies)
        {
            zombie.TrantoPause();
        }
    }

    public IEnumerator SpawnZombie()
    {
        for(int i = 0; i < 5; i++)
        {
            SpawnRandomZombie();
            yield return new WaitForSeconds(3);
        }
        yield return new WaitForSeconds(3);

        for (int i = 0; i < 10; i++)
        {
            SpawnRandomZombie();
            yield return new WaitForSeconds(3);
        }

        spawnState = SpawnState.End;
        yield return new WaitForSeconds(3);

        for (int i = 0; i < 20; i++)
        {
            SpawnRandomZombie();
            yield return new WaitForSeconds(3);
        }
    }

    private void SpawnRandomZombie()
    {
        if(spawnState == SpawnState.Spawning)
        {
            int index = Random.Range(0, spawnPointList.Length);
            GameObject go= GameObject.Instantiate(zombiePrefab, spawnPointList[index].position, Quaternion.identity);
            zombies.Add(go.GetComponent<Zombie>());
            go.GetComponent<SpriteRenderer>().sortingOrder = spawnPointList[index].GetComponent<SpriteRenderer>().sortingOrder;
        }
    }

    public void RemoveZombie(Zombie zombie)
    {
        zombies.Remove(zombie);
    }

}
