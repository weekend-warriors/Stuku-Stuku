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
    private List<GameObject> TaggedRunners;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        foreach(GameObject obj in PaintedParts)
        {
            obj.GetComponent<MeshRenderer>().material = VadaColor;
        }

        if (isLocalPlayer)
        {
            StartCoroutine(Deblind(BlindPeriod));
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

    private void OnTriggerEnter(Collider other)
    {
        if (!isBlind && other.CompareTag("FlagName")) // Need to tag flag
        {
            for(int i = TaggedRunners.Count; i >= 0; i--)
            {
                // Stuck player i
                TaggedRunners.RemoveAt(i);
            }
        }
    }

    IEnumerator Deblind(float time)
    {
        yield return new WaitForSeconds(time);

        isBlind = false;
    }
}
