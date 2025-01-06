using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TpLab.MaterialBuilder
{
    public static class AssetRepository
    {
        /// <summary>
        /// 設定ファイルのパス
        /// </summary>
        static readonly string SettingPath = $"Assets/TpLab/{nameof(MaterialBuilder)}/Settings.asset";

        /// <summary>
        /// MaterialBuilderの設定を読み込む。
        /// </summary>
        /// <returns>MaterialBuilderの設定</returns>
        public static MaterialBuilderSettings LoadSettings() => AssetDatabase.LoadAssetAtPath<MaterialBuilderSettings>(SettingPath) ?? new MaterialBuilderSettings();

        /// <summary>
        /// MaterialBuilderの設定を保存する。
        /// </summary>
        /// <param name="settings">MaterialBuilderの設定</param>
        public static void SaveSettings(MaterialBuilderSettings settings)
        {
            if (!AssetDatabase.Contains(settings as UnityEngine.Object))
            {
                // 新規の場合は作成
                AssetDatabase.CreateAsset(settings, SettingPath);
            }
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// TexturePackを読み込む。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>TexturePackの配列</returns>
        public static TexturePack[] LoadTexturePacks(string filePath)
        {
            var textures = new DirectoryInfo(filePath).GetFiles("*.png");
            return textures
                .Where(x => x.Name.EndsWith("_Base_color.png"))
                .Select(x => x.Name.Replace("_Base_color.png", ""))
                .Select(x => new TexturePack()
                {
                    BaseName = x,
                    Albedo = LoadTexture(textures.FirstOrDefault(f => f.Name == $"{x}_Base_color.png")?.FullName),
                    Metallic = LoadTexture(textures.FirstOrDefault(f => f.Name == $"{x}_Metallic.png")?.FullName),
                    Normal = LoadTexture(textures.FirstOrDefault(f => f.Name == $"{x}_Normal_DirectX.png")?.FullName),
                    Roughness = LoadTexture(textures.FirstOrDefault(f => f.Name == $"{x}_Roughness.png")?.FullName),
                    Occlusion = LoadTexture(textures.FirstOrDefault(f => f.Name == $"{x}_Mixed_AO.png")?.FullName),
                    NormalImporter = LoadTextureImporter(textures.FirstOrDefault(f => f.Name == $"{x}_Normal_DirectX.png")?.FullName),
                })
                .ToArray();
        }

        /// <summary>
        /// Textureを読み込む。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>Texture</returns>
        public static Texture LoadTexture(string filePath)
        {
            return !string.IsNullOrEmpty(filePath)
                ? AssetDatabase.LoadAssetAtPath<Texture>(filePath.RelativeToAssetsPath())
                : null;
        }

        /// <summary>
        /// TextureImporterを読み込む。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>TextureImporter</returns>
        public static TextureImporter LoadTextureImporter(string filePath)
        {
            return !string.IsNullOrEmpty(filePath)
                ? AssetImporter.GetAtPath(filePath.RelativeToAssetsPath()) as TextureImporter
                : null;
        }
    }
}
