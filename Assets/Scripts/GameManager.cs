using System;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public float PlayTime = 0;
    public int Score = 0;
    public Rigidbody2D PlayerRB;
    public Transform SpawnPoint;
    private bool _isReseting;

    public event Action OnReset;
    
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void FixedUpdate()
    {
        if (_isReseting)
        {
            ProceedReset();
            _isReseting = false;
        }
    }

    public void ResetPlayerPosition()
    {
        _isReseting = true;
    }

    private void ProceedReset()
    {
        PlayerRB.velocity = Vector2.zero;
        PlayerRB.angularVelocity = 0;
        PlayerRB.position = SpawnPoint.position;

        AudioManager.Instance.UndoCutoff();

        OnReset?.Invoke();
    }

    private void Update()
    {
        PlayTime += Time.deltaTime;
    }
}