﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RunnerController : NetworkBehaviour
{
    public Outline Outline;
    public List<Renderer> Renderer;
    public Shader TransparentShader;
    private bool IsWon = false;
    public List<Material> Materials;

    private void Start()
    {
        // so we can disable this component
    }

    public void ColorizeSelf(int index)
    {
        foreach (var renderer in Renderer)
        {
            renderer.material = Materials[index];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsWon)
        {
            return;
        }

        if (other.CompareTag("DropPoint"))
        {
            IsWon = true;
            if (isLocalPlayer)
                RunnerWin();
            // This bricks the game
            //gameObject.layer = LayerMask.NameToLayer("ExitedPlayer");
            Outline.enabled = true;

            foreach (var renderer in Renderer)
            {
                renderer.material.shader = TransparentShader;
            }
        }
    }

    public void GetStucked()
    {
        RunnerLose();
    }

    [Command(ignoreAuthority = true)]
    void RunnerLose()
    {
        OutlineCharacter();
    }

    [ClientRpc]
    void OutlineCharacter()
    {
        Outline.enabled = true;

        foreach (var renderer in Renderer)
        {
            renderer.material.shader = TransparentShader;
        }
    }

    

    [Command]
    void RunnerWin()
    {
        Debug.Log(isServer);
    }
}
