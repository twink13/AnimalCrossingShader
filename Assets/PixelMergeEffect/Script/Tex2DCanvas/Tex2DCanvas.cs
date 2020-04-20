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
        private CanvasCell[] _cells;

        private bool _dirty = false;

        //============================================================================================================
        // public function
        //============================================================================================================

        public Tex2DCanvas(Texture2D tex)
        {
            _tex = tex;

            _texRawData = _tex.GetRawTextureData();
            _pixelByteCount = CanvasUtil.GetTextureFormatByteCount(_tex.format);

            _cells = new CanvasCell[_tex.width * _tex.height];
            for (int x = 0; x < _tex.width; x++)
            {
                for (int y = 0; y < _tex.height; y++)
                {
                    _cells[y * _tex.width + x] = CanvasCell.Create(this, x, y);

                }
            }
        }

        public byte GetData(int x, int y, int slot = 0)
        {
            return _texRawData[(y * _tex.width + x) * _pixelByteCount + slot];
        }

        public void SetData(int x, int y, byte data, int slot = 0)
        {
            _texRawData[(y * _tex.width + x) * _pixelByteCount + slot] = data;
            _dirty = true;
        }

        public CanvasCell GetCellByPos(int x, int y)
        {
            return _cells[y * _tex.width + x];
        }

        //============================================================================================================
        // static factory
        //============================================================================================================
    }
}
