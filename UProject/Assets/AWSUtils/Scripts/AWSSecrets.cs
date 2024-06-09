using System;
using System.IO;
using UnityEngine;

namespace AWSUtils
{
    /// <summary>
    /// AWS認証情報を扱うクラス
    /// </summary>
    [System.Serializable]
    public class AwsSecrets
    {
        public string AccessKey;
        public string SecretKey;

        public string BucketName;

        public string RegionName;

        public string DistrubutionId;

        public static readonly string DefaultPath = Application.persistentDataPath + "/AWSSECRETS.json";
        public static AwsSecrets Load(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"Cannot find json file in specified path: {jsonFilePath}");
            }
            
            var json = File.ReadAllText(jsonFilePath);
            var secrets = JsonUtility.FromJson<AwsSecrets>(json);
            return secrets;
        }

        /// <summary>
        /// TODO: 認証情報の暗号化（ローカル保存のため現在は考慮しない）
        /// </summary>
        /// <param name="secrets"></param>
        /// <param name="jsonFilePath"></param>
        public static void Save(AwsSecrets secrets, string jsonFilePath)
        {
            var json = JsonUtility.ToJson(secrets,true);
            File.WriteAllText(jsonFilePath, json);
            
            Debug.Log($"[Debug][Secrets][Save]:{json}");
        }
    }
}