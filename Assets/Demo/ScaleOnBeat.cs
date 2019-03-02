using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FriendSea;

public class ScaleOnBeat : MonoBehaviour {
	void Start () {
        MusicManager.Instance.OnBeat += (beat) =>
        {
            transform.localScale = Vector3.one * 2f;
            GetComponent<Renderer>().material.color = Color.HSVToRGB(0.25f * (beat % 4), 1, 1);
        };
	}

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 5f * Time.deltaTime);
    }
}
