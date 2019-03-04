using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FriendSea;
using UnityEngine.UI;

public class SequentialMusicView : MonoBehaviour {
    [SerializeField]
    SequentialMusic music;
    [SerializeField]
    Text[] labels;
	
    void UpdateView()
    {
        for(int i = 0; i < labels.Length; i++)
        {
            labels[i].text = "";
            labels[i].color = Color.black;
            if (music.NextBlock == i)
                labels[i].text = "Waiting";
            if (music.CurrentBlock == i)
            {
                labels[i].text = "Playing";
                labels[i].color = Color.red;
            }
        }
    }

    private void Update()
    {
        UpdateView();
    }
}
