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
                    CanvasCell cell = CanvasCell.Create(this, x, y);
                    _cells[y * _tex.width + x] = cell;

                    cell.Init();
                    
                    // test
                    cell.SetDefaultData();
                }
            }

            // test
            Flush();
        }

        public void Flush()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                CanvasCell cell = _cells[i];
                if (cell.dirty)
                {
                    cell.UpdateData();
                }
            }
            UpdateData();
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

        public CanvasCell GetCellByPos(Vector2Int pos)
        {
            return GetCellByPos(pos.x, pos.y);
        }

        public CanvasCell GetCellByPos(int x, int y)
        {
            if (x < 0 || x >= _tex.width || y < 0 || y >= _tex.height)
            {
                return null;
            }
            return _cells[y * _tex.width + x];
        }

        public CanvasCell GetNeighborCell(CanvasCell centerCell, uint neighborID)
        {
            CanvasCell neighborCell = GetCellByPos(centerCell.position + CanvasUtil.GetDirByNeighborID(neighborID));
            if (neighborCell == null)
            {
                return CanvasCell.Universal;
            }
            return neighborCell;
        }

        //============================================================================================================
        // private function
        //============================================================================================================

        private void UpdateData()
        {
            _tex.LoadRawTextureData(_texRawData);
            _tex.Apply();
        }

        //============================================================================================================
        // static factory
        //============================================================================================================
    }
}
