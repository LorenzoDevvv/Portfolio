using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager3 : MonoBehaviour
{
    [SerializeField] private int _SpawnWave1TimerInSec;
    [SerializeField] private GameObject Wave1Enemies;
    void Start()

    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(_SpawnWave1TimerInSec);
        Instantiate(Wave1Enemies, new Vector3(7, 6, 0), Quaternion.identity); //Quaternion.identity = rotatie 0,0,0
    }
}
