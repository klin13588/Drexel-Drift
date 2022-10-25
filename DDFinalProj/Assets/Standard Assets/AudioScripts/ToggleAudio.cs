using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour{
    [SerializeField] private bool _toggleMusic, _toggleEffects;

    public void Toggle(){
        if(_toggleEffects) AudioManager.Instance.ToggleEffects();
        if(_toggleMusic) AudioManager.Instance.ToggleMusic();
    }
}
