using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeIt : MonoBehaviour
{
    private float scalar;

    // Start is called before the first frame update
    void Start()
    {
        scalar = 1;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(gameObject.transform.localScale.x > 4
        || gameObject.transform.localScale.x < 1)
            scalar *= -1;
        gameObject.transform.localScale += Vector3.one * scalar * Time.deltaTime;
    }
}
