﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.XR;

[RequireComponent(typeof(Button))]
public class ImpulseTestButton : MonoBehaviour {
    [Tooltip("If the amplitude slider is not connected then the amplitude will default to a controller's maximum value.")]
    public Slider amplitudeSlider;
    [Tooltip("If the duration slider is not connected then the duration will default to either 1 second or DurationMax - whichever is lower.")]
    public Slider durationSlider;
    [Tooltip("If the frequency slider is not connected then the frequency will default to a controller's maximum value.")]
    public Slider frequencySlider;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(SendImpulseToBothControllers);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(SendImpulseToBothControllers);
    }

    private void SendImpulseToBothControllers()
    {
        HapticCapabilities caps = new HapticCapabilities();
        
        if (InputHaptic.TryGetCapabilities(XRNode.LeftHand, out caps))
        {
            SendImpulseToNode(XRNode.LeftHand, caps);
        }

        if (InputHaptic.TryGetCapabilities(XRNode.RightHand, out caps))
        {
            SendImpulseToNode(XRNode.RightHand, caps);
        }
    }

    private void SendImpulseToNode(XRNode node, HapticCapabilities caps)
    {
        float amplitude = (amplitudeSlider != null) ? (amplitudeSlider.value) : 1f;
        float frequency = (frequencySlider != null) ? (frequencySlider.value) : 1f;

        if (frequencySlider != null)
        {
            InputHaptic.SendImpulse(node, 0, amplitude, frequencySlider.value);
            Debug.Log("Impulse sent to " + node 
                + "\n" + "Amplitude = " + amplitude
                + "\n" + "Frequency = " + frequencySlider.value
                );
        }
        else
        {
            InputHaptic.SendImpulse(node, 0, amplitude, 1f);
            Debug.Log("Impulse sent to " + node 
                + "\n" + "Amplitude = " + amplitude
                );
        }
    }
}
