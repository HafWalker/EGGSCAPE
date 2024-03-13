using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using System;
using FishNet.Object.Synchronizing;
using System.Transactions;

public class PlayerController : NetworkBehaviour, IDamageable
{
    [SerializeField] private float movementSpeed = 1;

    #region HEALTH
    
    [SyncVar] public float currentHealth;
    [SerializeField] private float maxHealt = 10f;
    [SerializeField] private HealthBar healthBar;

    #endregion

    public WeaponSword sword;
    public Transform body;

    private bool _isOwner = true;
    private bool _isReady = true;

    private PlayerNetworkSync playerNetworkSync;

    public override void OnStartClient()
    {
        base.OnStartClient();
        _isOwner = base.IsOwner;
        transform.parent = GameManager.Instance.gameCanvas;
        transform.name = "Player_" + base.OwnerId.ToString();

        if (base.IsOwner)
        {
            GameManager.Instance.localPlayer = this;
            GameManager.Instance.mainCamera.GetComponent<SmoothCameraFollow>().target = transform;
        }
        else { 
            GetComponent<PlayerController>().enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerNetworkSync = GetComponent<PlayerNetworkSync>();
        currentHealth = maxHealt;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isReady)
        {

            #region KEYBOARD INPUT ACTIONS

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(1, 0, 0) * movementSpeed * Time.deltaTime);
            }
            else
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-1, 0, 0) * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0, 1, 0) * movementSpeed * Time.deltaTime);
            }
            else
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0, -1, 0) * movementSpeed * Time.deltaTime);
            }

            #endregion

            #region MOUSE INPUT ACTIONS

            if (Input.GetMouseButtonDown(0))
            {
                sword.Attack(true);
                playerNetworkSync.AttackServer(this, true, TimeManager.Tick);
            }

            if (Input.GetMouseButtonUp(0))
            {
                sword.Attack(false);
                playerNetworkSync.AttackServer(this, false, TimeManager.Tick);
            }

            Vector3 mousePosition = Input.mousePosition;

            Vector3 targetPositionScreenSpace = Camera.main.WorldToScreenPoint(transform.position);

            if (mousePosition.x < targetPositionScreenSpace.x)
            {
                body.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                body.localScale = new Vector3(1, 1, 1);
            }

            #endregion

        }
    }

    public void Takedamage(GameObject gameObject, float value)
    {
        if (gameObject.transform != sword.transform)
        {
            currentHealth -= value;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                healthBar.UpdateHealtBarView(currentHealth, maxHealt);

                ResetPlayer();
                SetPlayerReady(false);

                StartCoroutine(WaitToReaedy());
            }
            else 
            { 
                healthBar.UpdateHealtBarView(currentHealth, maxHealt);
            }
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealt;
        healthBar.UpdateHealtBarView(currentHealth, maxHealt);
    }

    public void ResetPlayer()
    {
        GameManager.Instance.spawner.SpawnPlayer(this);
    }

    public void performAttack(PlayerController p,bool value, float delay)
    {
        sword.AttackPredict(value, delay);
    }

    public void PerformFlipBody(int value)
    {
        if (value < 0)
        {
            body.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            body.localScale = new Vector3(1, 1, 1);
        }
    }

    private void SetPlayerReady(bool value) 
    {
        _isReady = value;
        GetComponent<BoxCollider2D>().enabled = _isReady;
    }

    public IEnumerator WaitToReaedy()
    {
        yield return new WaitForSeconds(2);
        
        SetPlayerReady(true);
        ResetHealth();
    }
}