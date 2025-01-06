using UnityEngine;

namespace TpLab.MaterialBuilder
{
    public static class StringExtensions
    {
        /// <summary>
        /// 絶対パスからAssets相対パスに変換する。
        /// </summary>
        /// <param name="self">文字列</param>
        /// <returns>相対パス</returns>
        public static string RelativeToAssetsPath(this string self)
        {
            return self.Replace("\\", "/").Replace(Application.dataPath, "Assets");
        }
    }
}
