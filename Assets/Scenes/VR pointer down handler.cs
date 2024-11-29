using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRPointerDownHandler : MonoBehaviour
{
    // public Transform nextSphere;
    // public GameObject arrow;

    // Массив сфер и их соответствующих курсоров
    [System.Serializable]
    public class SphereData
    {
        public Transform sphere;  // Целевая сфера
        public GameObject arrow;  // Курсор для перехода
    }

    public SphereData[] spheres;  // Массив сфер с курсорами
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean triggerAction;

    GameObject m_Space;

    bool changing = false;

    private Transform player;

    void Start()
    {
      
        player = Camera.main.transform.parent;
        Debug.LogError("find player");
        if (player == null)
        {
            Debug.LogError("can't find player");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, что объект, с которым столкнулись, - это рука (или контроллер)
        if (other.CompareTag("Player"))
        {
            // Проверяем, нажата ли кнопка на контроллере
            if (triggerAction.GetStateDown(handType))
            {
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
                // ChangeSphere(nextSphere);
                
            }
        }
    }

    void Update()
    {
        // Вызываем проверку состояния кнопки в методе Update()
        if (triggerAction.GetStateDown(handType))
        {
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
            // ChangeSphere(nextSphere);
            // Дополнительные действия при нажатии кнопки (если нужно)
            Debug.Log("Trigger button pressed");
        }
    }

    public void ChangeSphere(Transform nextSphere)
    {
        Debug.LogError("find player3");
        if (nextSphere != null)
        {
            player.position = nextSphere.position;
            Debug.Log("Player transformed to " + nextSphere.name);
        }
        else
        {
            Debug.LogWarning("Sphere not inserted");
        }
    }

    
}
