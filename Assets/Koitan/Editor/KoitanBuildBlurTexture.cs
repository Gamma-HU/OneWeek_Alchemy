using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Rendering;
using UnityEditor.U2D;
using UnityEngine.UIElements;

public class KoitanBuildBlurTexture
{
    static string tmpPath;

    [MenuItem("KoitanLib/BuildBlurTexture")]
    static void ShowSelectionObjects()
    {
        Object[] selectedAsset = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);
        foreach (var go in selectedAsset)
        {
            var tex = (Texture2D)go;
            CreateBlurTexture(tex);
        }
    }


    static Texture2D CreateBlurTexture(Texture2D tex)
    {

        // 保存処理
        string assetPath = AssetDatabase.GetAssetPath(tex);
        var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        // 書き込み不可なら一時的に許可する
        bool isReadable = importer.isReadable;
        if (isReadable == false)
        {
            importer.isReadable = true;
            importer.SaveAndReimport();
        }

        Texture2D newTex = KoitanBuildTexture.BuildTexture(tex, 8, 2);

        string d = Path.GetDirectoryName(assetPath);
        string f = Path.GetFileNameWithoutExtension(assetPath);
        string e = Path.GetExtension(assetPath);
        string newPath = Path.Combine(d, $"{f}_blur{e}");
        tmpPath = newPath;
        if (!string.IsNullOrEmpty(newPath))
        {
            var png = newTex.EncodeToPNG();
            File.WriteAllBytes(newPath, png);

            AssetDatabase.ImportAsset(newPath);
            var newImporter = AssetImporter.GetAtPath(newPath) as TextureImporter;
            newImporter.textureCompression = TextureImporterCompression.Uncompressed;
            newImporter.filterMode = FilterMode.Point;
            // フォーマットを変えるのがめんどい
            var defaultPlatform = newImporter.GetDefaultPlatformTextureSettings();
            defaultPlatform.format = TextureImporterFormat.Alpha8;
            newImporter.SetPlatformTextureSettings(defaultPlatform);
            newImporter.SaveAndReimport();
            //var asset = AssetDatabase.LoadAssetAtPath(newPath, typeof(Texture2D));
        }

        // read/writeをもどす
        if (isReadable == false)
        {
            importer.isReadable = false;
            importer.SaveAndReimport();
        }

        return AssetDatabase.LoadAssetAtPath<Texture2D>(newPath);
    }

    [MenuItem("GameObject/Create Outline", false, 0)]
    public static void CreateOutlineOnHierarchy()
    {
        var go = Selection.activeGameObject;
        if (go == null) return;
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;
        Texture2D blurTexture = CreateBlurTexture(spriteRenderer.sprite.texture);
        Sprite blurSprite = AssetDatabase.LoadAssetAtPath<Sprite>(tmpPath);
        GameObject outlineObject = new GameObject("Outline");
        outlineObject.transform.SetParent(go.transform);
        outlineObject.transform.localPosition = Vector3.zero;
        outlineObject.transform.transform.localRotation = Quaternion.identity;
        outlineObject.transform.localScale = Vector3.one;
        SpriteRenderer outlineSpriteRenderer = outlineObject.AddComponent<SpriteRenderer>();
        outlineSpriteRenderer.sprite = blurSprite;
        outlineSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
        outlineSpriteRenderer.material = AssetDatabase.LoadAssetAtPath<Material>("Assets/Koitan/Shaders/Outline.mat");
        EditorUtility.SetDirty(outlineObject);
        /*
        Debug.Log($"{AssetDatabase.GetAssetPath(outlineSpriteRenderer.sprite.texture)}");
        var png = outlineSpriteRenderer.sprite.texture.EncodeToPNG();
        File.WriteAllBytes(AssetDatabase.GetAssetPath(outlineSpriteRenderer.sprite.texture), png);
        */
    }
}
