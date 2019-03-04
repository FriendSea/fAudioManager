using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    [SerializeField]
    Vector3 Axis;
    [SerializeField]
    float Speed;

	void Update () {
        transform.Rotate(Axis, Speed * Time.deltaTime);
	}
}
