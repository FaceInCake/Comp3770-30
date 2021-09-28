using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Vector3[] scale;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ResizePerSecond());
        
    }

    // Update is called once per frame
    void Update()
    {
        IEnumerator ResizePerSecond();
        while(true)
        {
            for(int i = 0; i < scale.Length; i++)
            {
                yield return new WaitForSeconds(1f);
                transform.localScale = differentScale[i];
                }
        }
    }
}
