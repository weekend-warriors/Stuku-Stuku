using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirror;
using UnityEngine;

class CustomNetworkManager : NetworkManager
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            var hud = GetComponent<NetworkManagerHUD>();
            hud.enabled = !hud.enabled;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public override void OnStartServer()
    {
    }

    public override void OnStopServer()
    {
    }

    public override void OnClientConnect(NetworkConnection connection)
    {
    }

    public override void OnClientDisconnect(NetworkConnection connection)
    {
    }
}