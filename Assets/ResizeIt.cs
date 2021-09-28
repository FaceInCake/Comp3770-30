using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResizeIt : MonoBehaviour
{
    public Vector3 local;
    public float speed = 1f;//to store local scale
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        local= transform.localScale;
        local.x += Time.deltatime;
        transform.localScale = local;
    }

}