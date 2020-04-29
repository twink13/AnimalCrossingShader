using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twink.AnimalCrossing
{
    [System.Serializable]
    public struct MainStyleData
    {
        // count 4 for 4 cornerID
        // value for styleID
        public byte[] m_data;
    }
    public class StaticDataConfig : ScriptableObject
    {
        public MainStyleData[] m_mainStyleData;
        
        public void CreateEmptyData()
        {
            m_mainStyleData = new MainStyleData[256];
        }

        //=========================================================================================
        // static
        //=========================================================================================
        
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

}