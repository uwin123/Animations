using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObj : MonoBehaviour
{
    List<Transform> spawnPos = new List<Transform>();
    GameObject[] monsters;

    public GameObject monPrefab;
    public int spawnNumber = 1;
    public float respawnDelay = 3f;

    int deadMonsters = 0;

    private void Start()
    {
        MakeSpawnPos();
    }
    void MakeSpawnPos()
    {
        foreach(Transform pos in transform)
        {
            if(pos.tag == "Respawn")
            {
                spawnPos.Add(pos);
            }
        }
        if(spawnNumber < spawnPos.Count)
        {
            spawnNumber = spawnPos.Count;
        }
        monsters = new GameObject[spawnNumber];
        MakeMonsters();
    }

    //프리팹으로부터 몬스터를 만들어 관리하는 함수 
    void MakeMonsters()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject mon = Instantiate(monPrefab, spawnPos[i].position, Quaternion.identity) as GameObject;
            mon.GetComponent<EnemyFSM>().SetRespawnObj(gameObject, i, spawnPos[i].position);
            mon.SetActive(false);

            monsters[i] = mon;
            //GameManager.instance.AddnewMonsters(mon);
        }
    }

    public void RemoveMonster(int spawnID)
    {
        //Destroy(monster[spawnID]);
        deadMonsters++;
        monsters[spawnID].SetActive(false);
        print(spawnID + "monster was killed");

        //죽은 몬스터의 숫자가 몬스터 배열의 크기와 같다면 일정시간 후에 몬스터를 다시 생성
        if(deadMonsters == monsters.Length)
        {
            StartCoroutine(InitMonsters());
            deadMonsters = 0;
        }

    }

    IEnumerator InitMonsters()
    {
        yield return new WaitForSeconds(respawnDelay);
        GetComponent<SphereCollider>().enabled = true;
    }

    void SpawnMonster()
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            //몬스터가 리스폰 될 때 초기화 될 함수를 찾아 실행
            monsters[i].GetComponent<EnemyFSM>().AddToWorldAgain();
            monsters[i].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SpawnMonster();
            GetComponent<SphereCollider>().enabled = false;
        }
    }

    void Update()
    {
        
    }
}
