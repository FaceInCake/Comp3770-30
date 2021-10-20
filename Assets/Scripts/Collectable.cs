using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    Collider player;
    Vector3 rotSpd = new Vector3(30, 15, 45);
    void Start()
    {
        player = GameObject.Find("CapsuleCharacter")
            .transform.GetChild(0)
            .GetComponent(typeof(Collider)) as Collider;
    }

    void OnTriggerEnter(Collider other) {
        if (other == player)
            Object.Destroy(this.gameObject);
    }

    void Update() {
        this.gameObject.transform.Rotate(rotSpd * Time.deltaTime);
    }

}
