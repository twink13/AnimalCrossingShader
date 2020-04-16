﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Collections;
using UnityEngine;

public class TestCanvasPlane : MonoBehaviour
{
    public Camera m_Camera;
    public LayerMask m_HitLayers;

    public Texture2D m_CanvasTexture;
    public int m_CanvasSize = 16;

    public Material m_CanvasMaterial;

    void Awake()
    {
        m_Camera = m_Camera != null ? m_Camera : Camera.main;

        m_CanvasTexture = new Texture2D(m_CanvasSize, m_CanvasSize, TextureFormat.ARGB32, false, true);
        m_CanvasTexture.filterMode = FilterMode.Point;

        m_CanvasMaterial.SetTexture("_MainTex", m_CanvasTexture);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 100f, m_HitLayers);

            if (hit.collider != null)
            {
                //Debug.Log("hit.textureCoord = " + hit.textureCoord);
                Vector2Int pos = Uv2Pos(hit.textureCoord, m_CanvasSize);
                DrawTextureAtPos(pos);
            }
        }
    }

    private void DrawTextureAtPos(Vector2Int pos)
    {
        //Debug.Log("DrawTextureAtPos! pos = " + pos);

        //NativeArray<uint> rawData = m_CanvasTexture.GetRawTextureData<uint>();
        //// a, r, g, b
        //// b, g, r, a
        //// todo: check big-ending
        //rawData[(pos.y * m_CanvasSize + pos.x)] = 0x000001FF;
        //m_CanvasTexture.LoadRawTextureData<uint>(rawData);
        //m_CanvasTexture.Apply();

        byte[] rgbData = m_CanvasTexture.GetRawTextureData();
        // a, r, g, b
        rgbData[(pos.y * m_CanvasSize + pos.x) * 4 + 1] = 0xFF;
        m_CanvasTexture.LoadRawTextureData(rgbData);
        m_CanvasTexture.Apply();
    }

    private static Vector2Int Uv2Pos(Vector2 uv, int size)
    {
        Vector2Int result = Vector2Int.zero;
        result.x = Mathf.Min((int)(uv.x * size), size - 1);
        result.y = Mathf.Min((int)(uv.y * size), size - 1);
        return result;
    }
}
