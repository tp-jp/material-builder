using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TpLab.MaterialBuilder.Editor
{
    [CreateAssetMenu(menuName = "TpLab/MaterialBuilder/CreateShaderSettings", fileName = "ShaderSettings")]
    public class ShaderSettings : ScriptableObject
    {
        const string SettingFileGuid = "dff6ce94db6a434438553976c3505f19";

        [SerializeField]
        public ShaderSetting[] shaderSettings;

        static ShaderSettings _instance;

        /// <summary>
        /// インスタンス
        /// </summary>
        public static ShaderSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = AssetDatabase.LoadAssetAtPath<ShaderSettings>(AssetDatabase.GUIDToAssetPath(SettingFileGuid));
                }

                return _instance;
            }
        }

        /// <summary>
        /// シェーダー設定一覧
        /// </summary>
        public static ShaderSetting[] AllShaderSettings =>
            Instance.shaderSettings.Where(x => Shader.Find(x.shaderName)).ToArray();
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
        
        [SerializeField]
        public string metallicKeyword;
        
        [SerializeField]
        public string normalKeyword;
        
        [SerializeField]
        public string roughnessKeyword;
    }
}