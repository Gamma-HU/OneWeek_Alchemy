using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using static UnityEngine.GraphicsBuffer;

public class KoitanBuildBlurTexture
{
    [MenuItem("KoitanLib/BuildBlurTexture")]
    static void ShowSelectionObjects()
    {
        Object[] selectedAsset = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);
        foreach (var go in selectedAsset)
        {
            var tex = (Texture2D)go;
            Texture2D resTex = KoitanBuildTexture.BuildTexture(tex, 4, 2);
            string assetPath = AssetDatabase.GetAssetPath(tex);
            string d = Path.GetDirectoryName(assetPath);
            string f = Path.GetFileNameWithoutExtension(assetPath);
            string e = Path.GetExtension(assetPath);
            string newPath = Path.Combine(d, $"{f}_blur{e}");
            if (!string.IsNullOrEmpty(newPath))
            {
                var png = resTex.EncodeToPNG();
                File.WriteAllBytes(newPath, png);

                AssetDatabase.ImportAsset(newPath);

                //var asset = AssetDatabase.LoadAssetAtPath(newPath, typeof(Texture2D));
            }
        }
    }
}
