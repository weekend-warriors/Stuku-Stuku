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

    void Start()
    {
        
    }

    void Update()
    {
        
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

    [Command]
    void RunnerWin()
    {
        Debug.Log(isServer);
    }
}