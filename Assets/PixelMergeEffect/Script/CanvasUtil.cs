using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Twink.AnimalCrossing
{
    public class CanvasUtil
    {
        public static Texture2D CreateIDTex(int size)
        {
            Texture2D tex = new Texture2D(size, size, TextureFormat.Alpha8, false, true);
            tex.filterMode = FilterMode.Point;

            byte[] rawData = tex.GetRawTextureData();

            int halfSize = size / 2;
            int conerSize = (int)(halfSize * 1.5f);

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    int sum = Mathf.Abs(x - halfSize) + Mathf.Abs(y - halfSize);
                    if (sum < halfSize)
                    {
                        rawData[y * size + x] = 0x00;
                        continue;
                    }

                    int sideId = CalcSideId(x, y, halfSize);

                    if (sum < conerSize)
                    {
                        switch (sideId)
                        {
                            case 1:
                                rawData[y * size + x] = 0x01;
                                break;
                            case 2:
                                rawData[y * size + x] = 0x02;
                                break;
                            case 3:
                                rawData[y * size + x] = 0x03;
                                break;
                            case 4:
                                rawData[y * size + x] = 0x04;
                                break;
                            default:
                                rawData[y * size + x] = 0x00;
                                break;
                        }
                    }
                    else
                    {
                        switch (sideId)
                        {
                            case 1:
                                rawData[y * size + x] = 0x05;
                                break;
                            case 2:
                                rawData[y * size + x] = 0x06;
                                break;
                            case 3:
                                rawData[y * size + x] = 0x07;
                                break;
                            case 4:
                                rawData[y * size + x] = 0x08;
                                break;
                            default:
                                rawData[y * size + x] = 0x00;
                                break;
                        }
                    }
                }
            }

            tex.LoadRawTextureData(rawData);
            tex.Apply();

            return tex;
        }

        public static int CalcSideId(int x, int y, int halfSize)
        {
            int sideId = 1;
            if (x > halfSize)
            {
                if (y > halfSize)
                {
                    sideId = 1;
                }
                else
                {
                    sideId = 2;
                }
            }
            else
            {
                if (y > halfSize)
                {
                    sideId = 4;
                }
                else
                {
                    sideId = 3;
                }
            }
            return sideId;
        }

        public static void WritePngFile(Texture2D tex, string relativePath)
        {
            var bytes = tex.EncodeToPNG();
            var file = File.Open(Application.dataPath + "/" + relativePath, FileMode.Create);
            var binary = new BinaryWriter(file);
            binary.Write(bytes);
            file.Close();
        }
    }
}
