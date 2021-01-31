using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class VadaController : NetworkBehaviour
{
    public Material VadaColor;
    public GameObject[] PaintedParts;
    private bool isBlind = true;
    public float BlindPeriod;
    private PlayerController playerController;
    public List<GameObject> TaggedRunners;

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        ColorizeSelf();

        if (isLocalPlayer)
        {
            StartCoroutine(Deblind(BlindPeriod));
        }
    }

    //[ClientRpc]
    void ColorizeSelf()
    {
        foreach (GameObject obj in PaintedParts)
        {
            obj.GetComponent<Renderer>().material = VadaColor;
        }
    }

    void Update()
    {
        if (isBlind)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ClickRaycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ClickRaycast, out RaycastHit ClickedObject))
            {
                if (ClickedObject.collider.CompareTag("Player"))
                {
                    TaggedRunners.Add(ClickedObject.collider.gameObject);
                    // Maybe outline ClickedObject
                }
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isBlind && other.CompareTag("DropPoint")) 
            {
                for(int i = TaggedRunners.Count-1; i >= 0; i--)
                {
                    TaggedRunners[i].GetComponent<RunnerController>().GetStucked();
                    TaggedRunners.RemoveAt(i);
                }
            }
        }
    }

    IEnumerator Deblind(float time)
    {
        yield return new WaitForSeconds(time);

        isBlind = false;
    }
}
