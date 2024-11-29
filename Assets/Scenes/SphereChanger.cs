using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SphereChanger : MonoBehaviour
{
    // Массив сфер и их соответствующих курсоров
    [System.Serializable]
    public class SphereData
    {
        public Transform sphere;  // Целевая сфера
        public GameObject arrow;  // Курсор для перехода
    }

    public SphereData[] spheres;  // Массив сфер с курсорами

    public SteamVR_Input_Sources handType;  // Контроллер
    public SteamVR_Action_Boolean triggerAction;  // Действие триггера

    private Transform player;  // Игрок

    private void Start()
    {
        player = Camera.main.transform.parent;

        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
    }

    private void Update()
    {
        // Проверяем, нажата ли кнопка триггера
        if (triggerAction.GetStateDown(handType))
        {
            CheckActiveArrow();
        }
    }

    private void CheckActiveArrow()
    {
        if (spheres == null || spheres.Length == 0)
        {
            Debug.LogWarning("No spheres or arrows assigned!");
            return;
        }

        // Используем луч для определения, какой курсор был активирован
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            foreach (SphereData sphereData in spheres)
            {
                if (hit.collider != null && hit.collider.gameObject == sphereData.arrow)
                {
                    Debug.Log($"Arrow selected: {sphereData.arrow.name}");
                    ChangeSphere(sphereData.sphere);
                    return;
                }
            }
        }
    }

    public void ChangeSphere(Transform targetSphere)
    {
        if (targetSphere != null)
        {
            Debug.Log($"Changing sphere to: {targetSphere.name}");
            player.position = targetSphere.position;
        }
        else
        {
            Debug.LogWarning("Target sphere not assigned!");
        }
    }
}
