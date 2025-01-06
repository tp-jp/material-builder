using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TpLab.MaterialBuilder.Editor
{
    [CreateAssetMenu(menuName = "TpLab/MaterialBuilder/CreateShaderSettings", fileName = "ShaderSettings")]
    public class ShaderSettings : ScriptableObject
    {
        static readonly string ShaderSettingPath = "Packages/com.tp-lab.material-builder/Runtime/ShaderSettings.asset";

        static readonly string ExtraShaderSettingPath =
            "Packages/com.tp-lab.material-builder/Runtime/ExtraShaderSettings.asset";

        [SerializeField]
        public ShaderSetting[] shaderSettings;

        static ShaderSettings _instance;
        static ShaderSettings _extraShaderSettings;

        /// <summary>
        /// インスタンス
        /// </summary>
        public static ShaderSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = AssetDatabase.LoadAssetAtPath<ShaderSettings>(ShaderSettingPath);
                }

                return _instance;
            }
        }

        static ShaderSettings ExtraShaderSettings
        {
            get
            {
                if (_extraShaderSettings == null)
                {
                    _extraShaderSettings = AssetDatabase.LoadAssetAtPath<ShaderSettings>(ExtraShaderSettingPath);
                }

                return _extraShaderSettings;
            }
        }

        /// <summary>
        /// シェーダー設定一覧
        /// </summary>
        public static ShaderSetting[] AllShaderSettings =>
            Instance.shaderSettings.Concat(ExtraShaderSettings.shaderSettings.Where(x => Shader.Find(x.shaderName)))
                .ToArray();
    }

    [Serializable]
    public class ShaderSetting
    {
        [SerializeField]
        public string shaderName;

        [SerializeField]
        public string albedoPropertyName;

        [SerializeField]
        public string metallicPropertyName;

        [SerializeField]
        public string normalPropertyName;

        [SerializeField]
        public string roughnessPropertyName;

        [SerializeField]
        public string occlusionPropertyName;
    }
}