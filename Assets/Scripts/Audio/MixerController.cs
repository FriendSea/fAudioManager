using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace FriendSea
{
    public class MixerController : MonoBehaviour
    {
        [SerializeField]
        AudioMixer mixer;

        const string BGMPropertyName = "BGMVolume";
        const string SEPropertyName = "SEVolume";

        public float BGMVolume { set { mixer.SetFloat(BGMPropertyName, Mathf.Max(20f * Mathf.Log10(value), -96f)); } }
        public float SEVolume { set { mixer.SetFloat(SEPropertyName, Mathf.Max(20f * Mathf.Log10(value), -96f)); } }
    }
}
