using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCanvasPlane : MonoBehaviour
{
    public Camera m_Camera;
    public LayerMask m_HitLayers;

    void Awake()
    {
        m_Camera = m_Camera != null ? m_Camera : Camera.main;
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
                Debug.Log("hit.textureCoord = " + hit.textureCoord);
            }
        }
    }
}
