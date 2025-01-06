using System;
using UnityEditor;
using UnityEngine;

namespace TpLab.MaterialBuilder.Editor
{
    [CreateAssetMenu(menuName = "TpLab/MaterialBuilder/CreateShaderSettings", fileName = "ShaderSettings")]
    public class ShaderSettings : ScriptableObject
    {
        static readonly string ResourcePath = "Packages/com.tp-lab.material-builder/Runtime/ShaderSettings.asset";

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
                    _instance = AssetDatabase.LoadAssetAtPath<ShaderSettings>(ResourcePath);
                }

                return _instance;
            }
        }
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