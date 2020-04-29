using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Twink.AnimalCrossing
{
    [System.Serializable]
    public class StyleConfigData
    {
        // count 4 for 4 cornerID
        // value for styleID
        public byte[] m_data;
        
        public StyleConfigData(byte style0, byte style1, byte style2, byte style3)
        {
            m_data = new byte[4];
            m_data[0] = style0;
            m_data[1] = style1;
            m_data[2] = style2;
            m_data[3] = style3;
        }

        // cornerID: [0, 3]
        public StyleConfigData CreateRotated(uint cornerID)
        {
            StyleConfigData to = new StyleConfigData(0, 0, 0, 0);
            for (int i = 0; i < 4; i++)
            {
                to.m_data[i] = m_data[(i - cornerID + 4) % 4];
            }
            return to;
        }
        
        public StyleConfigData CreateMirrored()
        {
            StyleConfigData to = new StyleConfigData(m_data[0], m_data[3], m_data[2], m_data[1]);
            return to;
        }
        
        public override string ToString()
        {
            return "[" + m_data[0] + ", " + m_data[1] + ", " + m_data[2] + ", " + m_data[3] + "]";
        }
    }
    public class StaticDataConfig : ScriptableObject
    {
        public StyleConfigData[] m_styleConfigData;
        
        public void CreateEmptyData()
        {
            m_styleConfigData = new StyleConfigData[256];
        }

        //=========================================================================================
        // static
        //=========================================================================================
        
#if UNITY_EDITOR
        [MenuItem("Assets/Create/AnimalCrossing/StaticDataConfig")]
        public static void CreateAsset()
        {
            ScriptableObjectUtility.CreateAsset<StaticDataConfig>();
        }
#endif

        public static byte Rotate(byte data, uint cornerID)
        {
            return CanvasUtil.ShiftRepeat(data, (int)cornerID * 2);
        }

        public static byte Mirror(byte data)
        {
            byte result = data;
            for (int i = 0; i < 7; i++)
            {
                int start = i;
                int end = 7 - 1 - i;
                
                byte startBit = CanvasUtil.GetBitAtRightPosition(data, start);
                result = CanvasUtil.SetBitAtRightPosition(result, end, startBit);
            }
            return result;
        }
    }


    //=========================================================================================
    // editor
    //=========================================================================================
#if UNITY_EDITOR
    [CustomEditor(typeof(StaticDataConfig))]
    [CanEditMultipleObjects]
    public class StaticDataConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            StaticDataConfig config = target as StaticDataConfig;

            if (GUILayout.Button("Create Config Data"))
            {
                config.m_styleConfigData = StyleDataCreatorUtils.CreateStyleConfigData();
            }
        }
    }
#endif
}