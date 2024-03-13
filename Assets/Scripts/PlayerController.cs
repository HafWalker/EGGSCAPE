using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class PlayerController : NetworkBehaviour, IDamageable
{
    public float movementSpeed = 1;

    public Health health;
    public Weapon currentWeapon;

    public Transform body;

    public TMP_Text playerName;

    private bool isLocalPlayer = true;

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

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
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

            if (Input.GetMouseButtonDown(0))
            {
                currentWeapon.Attack(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                currentWeapon.Attack(false);
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
        }
    }

    public void Takedamage(float value, GameObject gameObject)
    {
        if (gameObject.transform != currentWeapon.transform)
        {
            health.TakeDamage(value);
            if (health.currentHealth <= 0)
            {
                ResetPlayer();
            }
        }
    }

    public void ResetPlayer()
    {
        GameManager.Instance.spawner.SpawnPlayer(this);
    }
}