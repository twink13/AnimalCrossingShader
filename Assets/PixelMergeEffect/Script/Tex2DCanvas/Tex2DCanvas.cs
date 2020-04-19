using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twink.AnimalCrossing
{
    public class Tex2DCanvas
    {
        // main data
        private Texture2D _tex;

        // temp data
        private byte[] _texRawData;
        private int _pixelByteCount = 1;

        //============================================================================================================
        // public function
        //============================================================================================================

        public Tex2DCanvas(Texture2D tex)
        {
            _tex = tex;

            _texRawData = _tex.GetRawTextureData();
            _pixelByteCount = CanvasUtil.GetTextureFormatByteCount(_tex.format);
        }

        public byte GetData(int x, int y, int slot = 0)
        {
            return _texRawData[(y * _tex.width + x) * _pixelByteCount + slot];
        }

        public void SetData(int x, int y, byte data, int slot = 0)
        {
            _texRawData[(y * _tex.width + x) * _pixelByteCount + slot] = data;
        }

        //============================================================================================================
        // static factory
        //============================================================================================================
    }
}
