using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FriendSea;
using System.Linq;

public class ActivateWhenPlaying : MonoBehaviour {
    [SerializeField]
    GameObject target;
    [SerializeField]
    MusicBase music;

	void Update () {
        var playing = MusicManager.Instance.CueList.LastOrDefault();
        target.SetActive(playing == music);
	}
}
