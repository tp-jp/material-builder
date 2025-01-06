using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TpLab.MaterialBuilder.Editor
{
    /// <summary>
    /// SubstancePainterで出力したテクスチャを元にマテリアルを構築する拡張エディタ。
    /// </summary>
    public class MaterialBuilder : EditorWindow
    {
        ShaderSetting[] _shaderSettings;
        string[] _supportedShaders;
        MaterialBuilderSettings _settings;
        bool _isInitialized;

        /// <summary>
        /// 拡張エディタを開く。
        /// </summary>
        [MenuItem("TpLab/MaterialBuilder", false)]
        static void ShowWindow()
        {
            var window = GetWindow<MaterialBuilder>("MaterialBuilder");
            window.Initialize();
        }

        /// <summary>
        /// 初期化。
        /// </summary>
        void Initialize()
        {
            if (_isInitialized) return;
            minSize = new Vector2(470f, 145f);
            _shaderSettings = ShaderSettings.AllShaderSettings;
            _supportedShaders = _shaderSettings.Select(x => x.shaderName).ToArray();
            _settings = AssetRepository.LoadSettings();
            _isInitialized = true;
        }

        /// <summary>
        /// GUIを表示する。
        /// </summary>
        void OnGUI()
        {
            Initialize();
            EditorGUIUtility.labelWidth = 200f;
            _settings.inputDir = EditorGUILayout.ObjectField("Textureが格納されている入力フォルダ", _settings.inputDir, typeof(DefaultAsset), false) as DefaultAsset;
            _settings.outputDir = EditorGUILayout.ObjectField("Materialを出力する出力フォルダ", _settings.outputDir, typeof(DefaultAsset), false) as DefaultAsset;
            _settings.shaderIndex = EditorGUILayout.Popup("出力するMaterialのShader", _settings.shaderIndex, _supportedShaders);
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Space(EditorGUIUtility.labelWidth + 5f);
                _settings.isAutoSettingNormalMap = EditorGUILayout.ToggleLeft("NormalMapのTextureTypeを自動で設定する", _settings.isAutoSettingNormalMap);
            }
            GUILayout.Space(5);

            using (new EditorGUI.DisabledScope(DrawWarningMessage()))
            {
                if (GUILayout.Button("Build"))
                {
                    if (EditorUtility.DisplayDialogComplex("確認ダイアログ", "Materialを構築します。よろしいですか？", "OK", "キャンセル", null) == 0)
                    {
                        AssetRepository.SaveSettings(_settings);
                        Build();
                        EditorUtility.DisplayDialog("完了", "Materialの構築が完了しました。", "OK");
                    }
                }
            }
        }

        /// <summary>
        /// ウィンドウを閉じた際に呼ばれるイベント。
        /// </summary>
        void OnDestroy()
        {
            AssetRepository.SaveSettings(_settings);
        }

        /// <summary>
        /// 警告メッセージを表示する。
        /// </summary>
        /// <returns>警告がある場合はtrue、それ以外はfalse</returns>
        bool DrawWarningMessage()
        {
            if (_settings.inputDir == null)
            {
                EditorGUILayout.HelpBox("入力フォルダが設定されていません。", MessageType.Warning);
                return true;
            }
            if (_settings.outputDir == null)
            {
                EditorGUILayout.HelpBox("出力フォルダが設定されていません。", MessageType.Warning);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Materialを構築する。
        /// </summary>
        void Build()
        {
            var inputPath = AssetDatabase.GetAssetOrScenePath(_settings.inputDir);
            var outputPath = AssetDatabase.GetAssetOrScenePath(_settings.outputDir);
            var shaderSetting = _shaderSettings[_settings.shaderIndex];
            var shader = Shader.Find(shaderSetting.shaderName);
            var texturePacks = AssetRepository.LoadTexturePacks(inputPath);

            foreach (var texturePack in texturePacks)
            {
                // Material生成
                var material = new Material(shader);
                AssetDatabase.CreateAsset(material, $"{outputPath}/{texturePack.BaseName}.mat");

                // Texture割り当て
                material.SetTexture(shaderSetting.albedoPropertyName, texturePack.Albedo);
                material.SetTexture(shaderSetting.metallicPropertyName, texturePack.Metallic);
                material.SetTexture(shaderSetting.normalPropertyName, texturePack.Normal);
                material.SetTexture(shaderSetting.roughnessPropertyName, texturePack.Roughness);
                material.SetTexture(shaderSetting.occlusionPropertyName, texturePack.Occlusion);

                // NormalMapの自動設定
                var normalMapImporter = texturePack.NormalImporter;
                if (_settings.isAutoSettingNormalMap && normalMapImporter.textureType != TextureImporterType.NormalMap)
                {
                    normalMapImporter.textureType = TextureImporterType.NormalMap;
                    normalMapImporter.SaveAndReimport();
                }
            }
        }
    }
}
