using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RunnerController : NetworkBehaviour
{
    public Outline Outline;
    public List<Renderer> Renderer;
    public Material TransparentMaterial;
    public Shader TransparentShader;
    private bool IsWon = false;
    public List<Material> Materials;
    public Color LoseColor;
    public Color WinColor;

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

    private void OnTriggerStay(Collider other)
    {
        if (IsWon || !enabled)
        {
            return;
        }

        if (other.CompareTag("DropPoint"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RunnerWin();
            }
        }
    }

    public void GetStucked()
    {
        RunnerLose();
    }

    [Command(ignoreAuthority = true)]
    void RunnerWin()
    {
        WinCharacterUpdate();
    }

    [ClientRpc]
    void WinCharacterUpdate()
    {
        IsWon = true;
        // This bricks the game
        //gameObject.layer = LayerMask.NameToLayer("ExitedPlayer");
        Outline.enabled = true;
        Outline.OutlineColor = WinColor;

        foreach (var renderer in Renderer)
        {
            renderer.material = TransparentMaterial;
            renderer.material.shader = TransparentShader;
        }
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
        Outline.OutlineColor = LoseColor;

        foreach (var renderer in Renderer)
        {
            renderer.material = TransparentMaterial;
            renderer.material.shader = TransparentShader;
        }
    }
}
