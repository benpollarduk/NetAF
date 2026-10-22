using System;
using System.IO;
using System.Text.Json;

namespace NetAF.Persistence.Json
{
    /// <summary>
    /// Provides persistence for a save in the Json format.
    /// </summary>
    public static class JsonSave
    {
        #region StaticMethods

        /// <summary>
        /// Convert a restore point to Json.
        /// </summary>
        /// <param name="restorePoint">The restore point.</param>
        /// <returns>The Json reatore point.</returns>
        public static string ToJson(RestorePoint restorePoint)
        {
            return JsonSerializer.Serialize(restorePoint);
        }

        /// <summary>
        /// Create a restore point from Json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>The restore point created from the Json.</returns>
        public static RestorePoint FromJson(string json)
        {
            return JsonSerializer.Deserialize<RestorePoint>(json);
        }

        /// <summary>
        /// Persist a restore point to a file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="restorePoint">The restore point to persist.</param>
        /// <param name="message">A message detailing the result of the save, if the save was unsuccessful. If the save was successful this will be empty.</param>
        /// <returns>True if the save was successful else false.</returns>
        public static bool ToFile(string path, RestorePoint restorePoint, out string message)
        {
            try
            {
                var json = ToJson(restorePoint);
                json = Encryption.Encrypt(json);

                var directory = Path.GetDirectoryName(path);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                using (var writer = new StreamWriter(path))
                    writer.Write(json);

                message = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Return a restore point from a file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="restorePoint">The restore point.</param>
        /// <param name="message">A message detailing the result of the load, if the load was unsuccessful. If the load was successful this will be empty.</param>
        /// <returns>True if the load was successful else false.</returns>
        public static bool FromFile(string path, out RestorePoint restorePoint, out string message)
        {
            try
            {
                string json;

                using (var reader = new StreamReader(path))
                    json = reader.ReadToEnd();

                // check if was raw json, could be an old unecrypted save file
                if (!IsJson(json))
                    json = Encryption.Decrypt(json);

                restorePoint = FromJson(json);
                message = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                restorePoint = null;
                message = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Detemine if a string is in Json format.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True if the value is in Json format else false.</returns>
        public static bool IsJson(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            return value.StartsWith('{') && value.EndsWith('}');
        }

        #endregion
    }
}
