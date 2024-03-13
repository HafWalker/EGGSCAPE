using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Demo.AdditiveScenes;
using System;

public class PlayerNetworkSync : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {

        }
        else 
        { 
            GetComponent<PlayerNetworkSync>().enabled = false;
        }
    }

    [ServerRpc]
    public void AttackServer(PlayerController p, bool value)
    {
        Attack(p, value);
    }

    [ServerRpc]
    public void FlipBodyServer(PlayerController p, int value)
    {
        FlipBody(p, value);
    }

    [ServerRpc]
    public void TakeDamageServer(PlayerController p, float value)
    {
        TakeDamange(p, value);
    }
    
    [ServerRpc]
    public void RespawnPlayerServer(PlayerController p)
    {
        RespawnPlayer(p);
    }

    [ObserversRpc]
    public void Attack(PlayerController p, bool value) 
    {
        p.performAttack(value);
    }

    [ObserversRpc]
    public void FlipBody(PlayerController p, int value)
    {
        p.PerformFlipBody(value);
    }

    [ObserversRpc]
    public void TakeDamange(PlayerController p, float value)
    {
        p.PerformTakeDamage(value);
    }

    [ObserversRpc]
    public void RespawnPlayer(PlayerController p) 
    {
        p.PerformRespawn();
    }

}
