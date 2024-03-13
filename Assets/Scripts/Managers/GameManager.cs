using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("GameManager");
                    _instance = singleton.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    public Transform gameCanvas;
    public Transform mainCamera;

    public Spawner spawner;
    public PlayerController player;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Permite que el GameManager persista entre escenas
        }
    }

    public void StartGame()
    {
        Debug.Log("Game Started!");
    }

    public void SpawnPlayer(PlayerController p) 
    { 
        spawner.SpawnPlayer(p);
    }
}