using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;



public class VRPointerDownHandler : MonoBehaviour
{
    [System.Serializable]
    public class SphereArrowMapping 
    {
        public Transform sphere;
        public GameObject arrow;
    }
    public SphereArrowMapping[] mappings;

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean triggerAction;

    private Transform player;
    private int currentMappingIndex = 0;

    void Start()
    {
        player = Camera.main.transform.parent;
        MovePlayerToSphere(currentMappingIndex);
        MoveArrowToSphere(currentMappingIndex);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && triggerAction.GetStateDown(handType))
        {
            ChangeSphere();
        }
    }

    void Update()
    {
        if (triggerAction.GetStateDown(handType))
        {
            ChangeSphere();
            Debug.Log("Trigger button pressed");
        }
    }

    public void ChangeSphere()
    {
        currentMappingIndex = (currentMappingIndex + 1) % mappings.Length;
        MovePlayerToSphere(currentMappingIndex);
        MoveArrowToSphere(currentMappingIndex);
    }

    private void MovePlayerToSphere(int index)
    {
        if (index >= 0 && index < mappings.Length)
        {
            player.position = mappings[index].sphere.position;
            Debug.Log("Player moved to " + mappings[index].sphere.name);
        }
        else
        {
            Debug.LogWarning("Index out of range.");
        }
    }

    private void MoveArrowToSphere(int index)
    {
        if (index >= 0 && index < mappings.Length)
        {
            mappings[index].arrow.transform.position = mappings[index].sphere.position;
            Debug.Log("Arrow moved to " + mappings[index].sphere.name);
        }
        else
        {
            Debug.LogWarning("Index out of range.");
        }
    }
}