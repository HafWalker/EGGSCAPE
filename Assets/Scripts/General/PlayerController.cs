using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using System;

public class PlayerController : NetworkBehaviour, IDamageable
{
    public float movementSpeed = 1;

    public Health health;
    public Weapon currentWeapon;

    public Transform body;

    private bool isLocalPlayer = true;

    private PlayerNetworkSync playerNetworkSync;

    public override void OnStartClient()
    {
        base.OnStartClient();
        isLocalPlayer = base.IsOwner;

        transform.parent = GameManager.Instance.gameCanvas;

        if (base.IsOwner)
        {
            GameManager.Instance.player = this;
            GameManager.Instance.mainCamera.GetComponent<SmoothCameraFollow>().target = transform;
        }
        else { 
        
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerNetworkSync = GetComponent<PlayerNetworkSync>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
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
                playerNetworkSync.AttackServer(this, true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                playerNetworkSync.AttackServer(this, false);
            }

            Vector3 mousePosition = Input.mousePosition;

            Vector3 targetPositionScreenSpace = Camera.main.WorldToScreenPoint(transform.position);

            if (mousePosition.x < targetPositionScreenSpace.x)
            {
                playerNetworkSync.FlipBodyServer(this, -1);
            }
            else
            {
                playerNetworkSync.FlipBodyServer(this, 1);
            }

            #endregion

        }
    }

    public void Takedamage(float value, GameObject gameObject)
    {
        if (gameObject.transform != currentWeapon.transform)
        {
            playerNetworkSync.TakeDamageServer(this, value);
        }
    }

    public void ResetPlayer()
    {
        GameManager.Instance.spawner.SpawnPlayer(this);
    }

    public void performAttack(bool value)
    {
        currentWeapon.Attack(value);
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

    public void PerformTakeDamage(float value) 
    {
        health.TakeDamage(value);
        if (health.currentHealth <= 0)
        {
            playerNetworkSync.RespawnPlayerServer(this);
        }
    }

    public void PerformRespawn()
    {
        ResetPlayer();
    }
}