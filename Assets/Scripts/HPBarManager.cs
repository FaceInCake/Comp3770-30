using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarManager : MonoBehaviour
{
    RectTransform rt;
    Alive php;

    void Start()
    {
        GameObject bar = transform.GetChild(1).gameObject;
        rt = bar.GetComponent<RectTransform>();
        php =  transform.GetComponentInParent<Alive>();
    }

    // Update is called once per frame
    void Update()
    {
        float size = (php.getCurrentHealth() / php.getMaxHealth()) * 256;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
    }
}
