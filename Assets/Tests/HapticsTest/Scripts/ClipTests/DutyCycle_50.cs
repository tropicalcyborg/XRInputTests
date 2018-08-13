﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class DutyCycle_50 : ClipTestButton {

	protected override bool GenerateClip(XRNode node, ref byte[] clip)
    {
        HapticCapabilities caps = new HapticCapabilities();

        if (!InputHaptic.TryGetCapabilities(node, out caps))
            return false;

        // Generate actual clip
        clip = new byte[caps.bufferMaxSize];
        for(int i = 0; i < caps.bufferMaxSize; i++)
        {
            clip[i] = (i % 2 == 0) ? byte.MaxValue : (byte)0;
        }

        return true;
    }
}
