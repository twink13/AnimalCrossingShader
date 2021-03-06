﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Twink.AnimalCrossing
{
    public class CanvasHelper
    {
        //============================================================================================================
        // create id tex
        //============================================================================================================

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
                        rawData[y * size + x] = (byte)AreaID.DIR_5;
                        continue;
                    }

                    int quadrantId = CalcQuadrantId(x, y, halfSize);

                    if (sum < conerSize)
                    {
                        switch (quadrantId)
                        {
                            case 1:
                                rawData[y * size + x] = (byte)AreaID.DIR_9_BIG;
                                break;
                            case 2:
                                rawData[y * size + x] = (byte)AreaID.DIR_3_BIG;
                                break;
                            case 3:
                                rawData[y * size + x] = (byte)AreaID.DIR_1_BIG;
                                break;
                            case 4:
                                rawData[y * size + x] = (byte)AreaID.DIR_7_BIG;
                                break;
                            default:
                                rawData[y * size + x] = (byte)AreaID.DIR_5;
                                break;
                        }
                    }
                    else
                    {
                        switch (quadrantId)
                        {
                            case 1:
                                rawData[y * size + x] = (byte)AreaID.DIR_9_SMALL;
                                break;
                            case 2:
                                rawData[y * size + x] = (byte)AreaID.DIR_3_SMALL;
                                break;
                            case 3:
                                rawData[y * size + x] = (byte)AreaID.DIR_1_SMALL;
                                break;
                            case 4:
                                rawData[y * size + x] = (byte)AreaID.DIR_7_SMALL;
                                break;
                            default:
                                rawData[y * size + x] = (byte)AreaID.DIR_5;
                                break;
                        }
                    }
                }
            }

            tex.LoadRawTextureData(rawData);
            tex.Apply();

            return tex;
        }

        public static int CalcQuadrantId(int x, int y, int halfSize)
        {
            int quadrantId = 1;
            if (x > halfSize)
            {
                if (y > halfSize)
                {
                    quadrantId = 1;
                }
                else
                {
                    quadrantId = 2;
                }
            }
            else
            {
                if (y > halfSize)
                {
                    quadrantId = 4;
                }
                else
                {
                    quadrantId = 3;
                }
            }
            return quadrantId;
        }
        //============================================================================================================
        // write png
        //============================================================================================================

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
