using UnityEngine;
using UnityEditor;

public class CreateMaterialfromTextures : MonoBehaviour
{
    [MenuItem("Assets/lilToon/CreateMaterialfromTextures")]
    private static void AssignTextures()
    {
        Object[] selectedObjects = Selection.objects;
        if (selectedObjects.Length == 0)
        {
            Debug.LogError("Please select one or more textures to assign.");
            return;
        }

        Material material = new Material(Shader.Find("lilToon"));

        foreach (Object obj in selectedObjects)
        {
            if (obj is Texture2D)
            {
                Texture2D texture = (Texture2D)obj;
                string textureName = texture.name.ToLower();

                if (textureName.Contains("maintex_"))
                {
                    material.SetTexture("_MainTex", texture);
                }
                else if (textureName.Contains("alphamask_"))
                {
                    material.SetInt("_AlphaMaskMode", 1);
                    material.SetTexture("_AlphaMask", texture);
                }
                else if (textureName.Contains("normal_"))
                {
                    material.SetInt("_UseBumpMap", 1);
                    material.SetTexture("_BumpMap", texture);
                }
                else if (textureName.Contains("metallic_"))
                {
                    material.SetInt("_UseReflection", 1);
                    material.SetInt("_Metallic", 1);
                    material.SetInt("_SpecularToon", 0);
                    material.SetTexture("_MetallicGlossMap", texture);
                }
                else if (textureName.Contains("smoothness_"))
                {
                    material.SetInt("_UseReflection", 1);
                    material.SetTexture("_SmoothnessTex", texture);

                }
                
            }
        }
        string commonDirectory = GetDirectoryPath(AssetDatabase.GetAssetPath(selectedObjects[0]));
        string materialPath = commonDirectory + "/newmaterial.mat";
        AssetDatabase.CreateAsset(material, materialPath);
        AssetDatabase.SaveAssets();

        Debug.Log($"Textures assigned to material: '{materialPath}'");
    }
        private static string GetDirectoryPath(string fullPath)
    {
        int lastSlashIndex = fullPath.LastIndexOf('/');
        if (lastSlashIndex >= 0)
        {
            return fullPath.Substring(0, lastSlashIndex);
        }
        return "";
    }
}
