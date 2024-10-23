using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FuelManager : MonoBehaviour
{
    [Header("UI")]
    public Slider _fuelSlider;
    

    [Header("Information")]
    [SerializeField] private float _currentFuel;
    [SerializeField] private float _maxFuel;
    [SerializeField] private float _fuelBurn;
    [SerializeField] private float _delayTimer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fuel")
        {
            Refuel();
            Destroy(other.gameObject);
        }
    }
    void Update()
    { 
        // fuel goes down when pressing W or S
        if (Input.GetKey(KeyCode.W))
        {
            _currentFuel -= _fuelBurn * _delayTimer * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _currentFuel -= _fuelBurn * _delayTimer * Time.deltaTime;
        }

        //fuel slider changing
        _fuelSlider.value = _currentFuel / _maxFuel;

        // when fuel is 0 or lower restart scene
        if (_currentFuel <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Refuel()
    {
        _currentFuel += 35;
    }
}
