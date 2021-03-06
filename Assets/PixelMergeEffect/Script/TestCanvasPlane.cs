﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Collections;
using UnityEngine;

namespace Twink.AnimalCrossing
{
    public class TestCanvasPlane : MonoBehaviour
    {
        public Camera m_Camera;
        public LayerMask m_HitLayers;


        public Texture2D m_CanvasTexture;
        public int m_CanvasSize = 16;

        public Material m_CanvasMaterial;
        public Color[] m_MainColors;

        public Texture2D m_TestAreaIdTex;

        private Tex2DCanvas _Canvas;

        void Awake()
        {
            m_Camera = m_Camera != null ? m_Camera : Camera.main;

            m_CanvasTexture = new Texture2D(m_CanvasSize, m_CanvasSize, TextureFormat.ARGB32, false, true);
            m_CanvasTexture.filterMode = FilterMode.Point;

            m_CanvasMaterial.SetTexture("_MainTex", m_CanvasTexture);
            m_CanvasMaterial.SetColorArray("_MainColors", m_MainColors);

            _Canvas = new Tex2DCanvas(m_CanvasTexture);
            _Canvas.Flush();
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

        private void OnGUI()
        {
            if (GUILayout.Button("Create ID tex"))
            {
                m_TestAreaIdTex = CanvasHelper.CreateIDTex(128);
                CanvasHelper.WritePngFile(m_TestAreaIdTex, "PixelMergeEffect/Texture/IDTex.png");
            }
            if (GUILayout.Button("Test show main styles"))
            {
                string mainStyleList = StyleDataCreatorUtils.CreateCachedMainStyleList();
                Debug.Log("mainStyleList = " + mainStyleList);
            }
            if (GUILayout.Button("Test show cached config data"))
            {
                StyleDataCreatorUtils.CreateStyleConfigData();
            }
        }

        private void DrawTextureAtPos(Vector2Int pos)
        {
            //Debug.Log("DrawTextureAtPos! pos = " + pos);

            CanvasCell cell = _Canvas.GetCellByPos(pos.x, pos.y);
            cell.data0 = 0b_1010_1010;
            cell.data1 = 0b_0010_0001;
            cell.data2 = 0b_0100_0011;
            cell.data3 = 0b_0000_0000;
            cell.dirty = true;

            _Canvas.Flush();
        }

        //============================================================================================================
        // pos
        //============================================================================================================

        // 7 8 9
        // 4 · 6
        // 1 2 3
        private Vector2Int GetNeighborPos(Vector2Int pos, int neighborId)
        {
            switch (neighborId)
            {
                case 1:
                    pos.x += -1;
                    pos.y += -1;
                    break;
                case 2:
                    pos.x += 0;
                    pos.y += -1;
                    break;
                case 3:
                    pos.x += +1;
                    pos.y += -1;
                    break;
                case 4:
                    pos.x += -1;
                    pos.y += 0;
                    break;
                case 5:
                    pos.x += 0;
                    pos.y += 0;
                    break;
                case 6:
                    pos.x += +1;
                    pos.y += 0;
                    break;
                case 7:
                    pos.x += -1;
                    pos.y += +1;
                    break;
                case 8:
                    pos.x += 0;
                    pos.y += +1;
                    break;
                case 9:
                    pos.x += +1;
                    pos.y += +1;
                    break;
                default:
                    pos.x += 0;
                    pos.y += 0;
                    break;
            }
            return CheckBound(pos);
        }

        private Vector2Int CheckBound(Vector2Int pos)
        {
            pos.x = Mathf.Clamp(pos.x, 0, m_CanvasSize - 1);
            pos.y = Mathf.Clamp(pos.y, 0, m_CanvasSize - 1);
            return pos;
        }

        //============================================================================================================
        // static
        //============================================================================================================

        private static Vector2Int Uv2Pos(Vector2 uv, int size)
        {
            Vector2Int result = Vector2Int.zero;
            result.x = Mathf.Min((int)(uv.x * size), size - 1);
            result.y = Mathf.Min((int)(uv.y * size), size - 1);
            return result;
        }
    }
}
