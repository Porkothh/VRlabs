using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Interactable : MonoBehaviour
{
    private Material objectMaterial;
    private Color originalColor;
    private float highlightIntensity = 2f;
    private bool isHovered = false;

    // Start is called before the first frame update
    void Start()
    {
        objectMaterial = GetComponent<Renderer>().material;
        originalColor = objectMaterial.color;
    }

    private void OnHandHoverBegin(Hand hand)
    {
        isHovered = true;
        objectMaterial.color = originalColor + new Color(highlightIntensity, highlightIntensity, highlightIntensity, 0);
    }

    private void OnHandHoverEnd(Hand hand)
    {
        isHovered = false;
        objectMaterial.color = originalColor;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
