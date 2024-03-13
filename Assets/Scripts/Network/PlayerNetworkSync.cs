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
    public void AttackServer(PlayerController p, bool value, uint startTick)
    {
        Attack(p, value, startTick);
    }

    [ServerRpc]
    public void FlipBodyServer(PlayerController p, int value)
    {
        FlipBody(p, value);
    }
    
    [ObserversRpc(ExcludeOwner = true)]
    public void Attack(PlayerController p, bool value, uint startTick) 
    {
        float timeDiff = (float)(TimeManager.Tick - startTick) / TimeManager.TickRate;
        p.performAttack(p, value, timeDiff);
    }

    [ObserversRpc]
    public void FlipBody(PlayerController p, int value)
    {
        p.PerformFlipBody(value);
    }

}