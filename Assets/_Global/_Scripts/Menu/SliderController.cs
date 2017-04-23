using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : Slider
{
    protected override void Awake()
    {
        base.Awake();
        value = AudioListener.volume;
    }
}
