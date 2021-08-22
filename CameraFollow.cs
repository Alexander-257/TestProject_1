using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 cameraPosition = new Vector3(5.32f, 10.57f, -6.79f);

    void Start() {
        //cameraPosition = new Vector3(2.92f, 7.4f, -4.38f);
    }

    void LateUpdate() {
        transform.position = player.transform.position + cameraPosition;
    }
}
