using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;


public class SphereChanger : MonoBehaviour {

    //Объект должен называться 'Space' и помещен над камерой
    GameObject m_Space;

    bool changing = false;

    void Awake()
    {

        m_Space = GameObject.Find("Space");

        if (m_Space == null)
            Debug.LogWarning("No Space object found on camera.");

    }


    public void ChangeSphere(Transform nextSphere)
    {

        StartCoroutine(FadeCamera(nextSphere));
    }


    IEnumerator FadeCamera(Transform nextSphere)
    {

        if (m_Space != null)
        {
            StartCoroutine(FadeIn(0.75f, m_Space.GetComponent<Renderer>().material));
            yield return new WaitForSeconds(0.75f);

            Camera.main.transform.parent.position = nextSphere.position;

            StartCoroutine(FadeOut(0.75f, m_Space.GetComponent<Renderer>().material));
            yield return new WaitForSeconds(0.75f);
        }
        else
        {
            Camera.main.transform.parent.position = nextSphere.position;
        }


    }


    IEnumerator FadeOut(float time, Material mat)
    {
        while (mat.color.a > 0.0f)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }


    IEnumerator FadeIn(float time, Material mat)
    {
        while (mat.color.a < 1.0f)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a + (Time.deltaTime / time));
            yield return null;
        }
    }


}
