using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    public WaveSpawner waveSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            waveSpawner.ActivateWaveSpawner();
            Destroy(gameObject); // Destroy the trigger collider
        }
    }
}
