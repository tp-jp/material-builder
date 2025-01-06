using UnityEditor;
using UnityEngine;

namespace TpLab.MaterialBuilder.Editor
{
    /// <summary>
    /// MaterialBuilderの設定。
    /// </summary>
    public class MaterialBuilderSettings : ScriptableObject
    {
        /// <summary>
        /// 入力ディレクトリ
        /// </summary>
        public DefaultAsset inputDir;

        /// <summary>
        /// 出力ディレクトリ
        /// </summary>
        public DefaultAsset outputDir;

        /// <summary>
        /// シェーダー参照値
        /// </summary>
        public int shaderIndex;

        /// <summary>
        /// NormalMapのTextureTypeを自動で設定する。
        /// </summary>
        public bool isAutoSettingNormalMap = true;
    }
}
