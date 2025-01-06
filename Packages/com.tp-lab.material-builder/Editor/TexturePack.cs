using UnityEditor;
using UnityEngine;

namespace TpLab.MaterialBuilder
{
    /// <summary>
    /// Materialに設定するTexture一式。
    /// </summary>
    public class TexturePack
    {
        /// <summary>
        /// 基本ファイル名
        /// </summary>
        public string BaseName { get; set; }

        /// <summary>
        /// Albedo
        /// </summary>
        public Texture Albedo { get; set; }

        /// <summary>
        /// Metaillic
        /// </summary>
        public Texture Metallic { get; set; }

        /// <summary>
        /// NormalMap
        /// </summary>
        public Texture Normal { get; set; }

        /// <summary>
        /// Roughness
        /// </summary>
        public Texture Roughness { get; set; }

        /// <summary>
        /// Occlusion
        /// </summary>
        public Texture Occlusion { get; set; }

        /// <summary>
        /// NormalMapのImporter
        /// </summary>
        public TextureImporter NormalImporter { get; set; }
    }
}
