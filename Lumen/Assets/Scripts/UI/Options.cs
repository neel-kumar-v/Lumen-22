using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class Options : MonoBehaviour
{

    public AudioMixer audioMixer;
    public AnimationCurve volumeCurve;
    public AnimationCurve sensitivityCurve;

    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI mouseText;
    public TextMeshProUGUI turnText;

    public Slider volumeSlider;
    public Slider mouseSlider;
    public Slider turnSlider;
    public void SetVolume(float volume) {
        float curveVol = volumeCurve.Evaluate(volume);
        volumeText.SetText("VOLUME: {0:0}", volume);
        audioMixer.SetFloat("volume", curveVol);
    }
    public void SetSensitivity(float sensitivity) {
        float curveSensitivity = sensitivityCurve.Evaluate(sensitivity);
        mouseText.SetText("MOUSE SENSITIVITY: {0:0}", sensitivity);
        Debug.Log($"{curveSensitivity}");
        PlayerPrefs.SetFloat("sensitivity", curveSensitivity); 
    }
    public void SetTurnSpeed(float turnSpeed) {
        float turn = turnSpeed * 6f;
        string turnDescription = turnSpeed switch {1f => "Very Slow", 2f => "Slow", 3f => "Medium", 4f => "Fast", 5f => "Very Fast", _ => "" };
        turnText.SetText("MIRROR TURN SPEED: " + turnDescription);
        Debug.Log(turn);
        PlayerPrefs.SetFloat("turn speed", turn); 
    }

    public void Start() {
        SetVolume(50f);
        volumeSlider.value = 50f;
        SetSensitivity(50f);
        mouseSlider.value = 50f;
        SetTurnSpeed(3f);
        mouseSlider.value = 3f;
    }
}
