﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ColorData : ScriptableObject
{
    [SerializeField] byte[] m_colorPattern = { 20, 40, 63, 86, 109, 132, 155, 188, 211, 234, 247, 255 };
    public int PatternNum { get => m_colorPattern.Length; }
    [SerializeField] Vector3[] m_rgbPattern = {
            new Vector3(0.7f, 0.7f, 0.7f), new Vector3(1, 0, 0), new Vector3(1, 0.3f, 0), new Vector3(1, 0.5f, 0), new Vector3(1, 0.8f, 0), new Vector3(1, 1, 0), new Vector3(0.5f, 1, 0),
            new Vector3(0, 1, 0), new Vector3(0, 1, 0.5f), new Vector3(0, 1, 1), new Vector3(0, 0.8f, 1), new Vector3(0, 0.5f, 1), new Vector3(0, 0.2f, 1),new Vector3(0, 0, 1),
            new Vector3(0.2f, 0, 1), new Vector3(0.5f, 0, 1), new Vector3(0.7f, 0, 1) , new Vector3(1, 0, 1), new Vector3(1, 0, 0.8f), new Vector3(1, 0, 0.5f) };
    public int ColorTypeNum { get => m_rgbPattern.Length; }
    public Color GetColor(int patternNum,int colorType)
    {
        if (patternNum >= m_colorPattern.Length)
        {
            return new Color32((byte)(m_rgbPattern[colorType].x * 127 + m_colorPattern[patternNum - PatternNum] * 0.5f),
                         (byte)(m_rgbPattern[colorType].y * 127 + m_colorPattern[patternNum - PatternNum] * 0.5f),
                         (byte)(m_rgbPattern[colorType].z * 127 + m_colorPattern[patternNum - PatternNum] * 0.5f), 255);
        }
        return new Color32((byte)(m_colorPattern[patternNum] * m_rgbPattern[colorType].x),
                         (byte)(m_colorPattern[patternNum] * m_rgbPattern[colorType].y),
                         (byte)(m_colorPattern[patternNum] * m_rgbPattern[colorType].z), 255);
    }
    public Color GetColor(int number)
    {
        return GetColor(number % (PatternNum * 2), number / (PatternNum * 2));
    }
}
