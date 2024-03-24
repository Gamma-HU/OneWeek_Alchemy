using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoitanBuildTexture
{
    public static Texture2D BuildTexture(Texture2D tex, int skinWidth, float sig)
    {
        Texture2D resTex = new Texture2D(tex.width + skinWidth * 2, tex.height + skinWidth * 2);
        Color[] colors = new Color[resTex.width * resTex.height];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.clear;
        }
        resTex.SetPixels(colors);
        for (int y = 0; y < tex.height; y++)
        {
            int ry = y + skinWidth;
            for (int x = 0; x < tex.width; x++)
            {
                int rx = x + skinWidth;
                resTex.SetPixel(rx, ry, tex.GetPixel(x, y));
            }
        }
        resTex = ImageFakeBloom.CreateBlurTexture(resTex, sig);
        resTex.Apply();
        return resTex;
    }
}
