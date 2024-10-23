using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Timer")] [SerializeField] private TextMeshProUGUI _timerText;

    public float _currentTime;
    [SerializeField] private bool _countDown;

    [SerializeField] private bool _hasFormat;

    public bool _timerStarted = false;

    private bool _finished;

    [Header("Gameloop")]
    //[SerializeField] private TextMeshProUGUI _gameOverText;
    public Button _restartButton;

    [Header("SaveData")] public float _finishedTime;
    //[SerializeField] private TextMeshProUGUI _highScoreText;


    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("FinalScore"))
        {
            _finishedTime = PlayerPrefs.GetFloat("FinalScore");
            Debug.Log(_finishedTime);
        }
    }

    void Update()
    {
        // Calculates the start time and going down or up
        if (!_finished && _timerStarted)
        {
            _currentTime = _countDown ? _currentTime -= Time.deltaTime : _currentTime += Time.deltaTime;
            TimerText();
            Debug.Log("false");
        }

        else
        {
            // _currentTime = Time.timeScale = 0;
            //_highScoreText.text = "" + _finishedTime;
        }
    }


    private void TimerText()
    {
        _timerText.text = _currentTime.ToString("0.00");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish" && _currentTime <= _finishedTime)
        {
            _finished = true;
            Debug.Log(_currentTime);
            _finishedTime = _currentTime;

            SavePrefs();
        }
    }

    void SavePrefs()
    {
        PlayerPrefs.SetFloat("FinalScore", _finishedTime);
        PlayerPrefs.Save();

        Debug.Log(_finishedTime);
    }
}