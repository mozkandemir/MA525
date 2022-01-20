using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calc : MonoBehaviour
{
    public static float map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    public static Color LerpHSV(Color start, Color end, float step)
    {
        float sH, sS, sV;
        float eH, eS, eV;

        Color.RGBToHSV(start, out sH, out sS, out sV);
        Color.RGBToHSV(end, out eH, out eS, out eV);

        return Color.HSVToRGB(Mathf.Lerp(sH, eH, step), sS, sV);
    }

    public static Color SetSV(Color c, float saturation, float value)
    {
        float H, S, V;
        Color.RGBToHSV(c, out H, out S, out V);
        return Color.HSVToRGB(H, saturation, value);
    }

    public static float Spring(float from, float to, float time)
    {
        time = Mathf.Clamp01(time);
        time = (Mathf.Sin(time * Mathf.PI * (.2f + 2.0f * time * time * time)) * Mathf.Pow(1f - time, 2.2f) + time) * (1f + (1.2f * (1f - time)));
        return from + (to - from) * time;
    }

    public static Vector3 Spring(Vector3 from, Vector3 to, float time)
    {
        return new Vector3(Spring(from.x, to.x, time), Spring(from.y, to.y, time), Spring(from.z, to.z, time));
    }

    public static float Berp(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
        return start + (end - start) * value;
    }
}
