using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

public class TestBuildTex : MonoBehaviour
{
    [SerializeField]
    Texture2D tex;
    [SerializeField]
    int skinWidth;
    [SerializeField]
    float sig;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

#if UNITY_EDITOR
    [ContextMenu("BuildTexture")]
    void BuildTexture()
    {
        Texture2D resTex = KoitanBuildTexture.BuildTexture(tex, skinWidth, sig);
        var path = EditorUtility.SaveFilePanelInProject(title: "Save Texture", defaultName: $"{tex.name}_blur", extension: "png", message: "Save Texture");

        if (!string.IsNullOrEmpty(path))
        {
            var png = resTex.EncodeToPNG();
            File.WriteAllBytes(path, png);

            AssetDatabase.ImportAsset(path);

            var asset = AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
        }
    }
#endif
}
