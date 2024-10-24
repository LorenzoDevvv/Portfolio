using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerTwo : MonoBehaviour
{
    [SerializeField] private int _SpawnWave1TimerInSec;

    [SerializeField] private int _SpawnWave2TimerInSec;

    [SerializeField] private int _SpawnWave3TimerInSec;

    [SerializeField] private int _SpawnWave4TimerInSec;

    [SerializeField] private int _SpawnWave5TimerInSec;

    [SerializeField] private int _SpawnWave6TimerInSec;

    [SerializeField] private GameObject Wave1Enemies;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(_SpawnWave1TimerInSec);
        Instantiate(Wave1Enemies, new Vector3(25, 17, 19), Quaternion.identity); //Quaternion.identity = rotatie 0,0,0

        yield return new WaitForSeconds(_SpawnWave2TimerInSec);
        Instantiate(Wave1Enemies, new Vector3(25, 17, 19), Quaternion.identity); //Quaternion.identity = rotatie 0,0,0

        yield return new WaitForSeconds(_SpawnWave3TimerInSec);
        Instantiate(Wave1Enemies, new Vector3(25, 17, 19), Quaternion.identity); //Quaternion.identity = rotatie 0,0,0

        yield return new WaitForSeconds(_SpawnWave4TimerInSec);
        Instantiate(Wave1Enemies, new Vector3(25, 17, 19), Quaternion.identity); //Quaternion.identity = rotatie 0,0,0

        yield return new WaitForSeconds(_SpawnWave5TimerInSec);
        Instantiate(Wave1Enemies, new Vector3(25, 17, 19), Quaternion.identity); //Quaternion.identity = rotatie 0,0,0

        yield return new WaitForSeconds(_SpawnWave6TimerInSec);
        Instantiate(Wave1Enemies, new Vector3(25, 17, 19), Quaternion.identity); //Quaternion.identity = rotatie 0,0,0
    }



    void Update()
    {

    }
}
