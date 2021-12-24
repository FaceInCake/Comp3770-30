using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TexTile : MonoBehaviour
{
    public float width = 1;
    public float length = 1;
    void Start()
    {
        Renderer re = gameObject.GetComponent<Renderer>();
        Material mat = new Material(re.sharedMaterial);
        Bounds bo = re.bounds;
        mat.SetTextureScale("_MainTex", new Vector2(
            bo.size.x / width,
            bo.size.z / length
        ));
        re.sharedMaterial = mat;
    }

}
