using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColour : MonoBehaviour
{
    private Renderer rend;
    private Color clr;

    private void Start() {
        rend = gameObject.GetComponent<Renderer>();
        clr = new Color(0.15f, 0.85f, 0.15f, 0.5f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            rend.material.color = clr;
        }
    }

}
