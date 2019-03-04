using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FriendSea;
using System.Linq;

public class ShowCueList : MonoBehaviour {
    [SerializeField]
    GameObject originalView;

    void UpdateCueList()
    {
        foreach(Transform t in transform)
        {
            if (t == transform) continue;
            Destroy(t.gameObject);
        }
        foreach(var m in MusicManager.Instance.CueList.Reverse())
        {
            var view = Instantiate(originalView, transform);
            view.GetComponentInChildren<UnityEngine.UI.Text>().text = m.name;
        }
    }

    private void Update()
    {
        UpdateCueList();
    }
}
