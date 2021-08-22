using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabricatedElementRotation : MonoBehaviour
{
    [SerializeField] float speedRotation;

    void Start() {
        
    }

    void LateUpdate() {
        transform.Rotate(Vector3.up * Time.deltaTime * speedRotation);
    }
}
