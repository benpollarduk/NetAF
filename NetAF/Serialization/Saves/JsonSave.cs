using Newtonsoft.Json;
using System;
using System.IO;

namespace NetAF.Serialization.Saves
{
    /// <summary>
    /// Provides persistence for a save in the Json format.
    /// </summary>
    public static class JsonSave
    {
        /// <summary>
        /// Convert a save to Json.
        /// </summary>
        /// <param name="save">The save.</param>
        /// <returns>The save in Json.</returns>
        public static string ToJson(Save save)
        {
            return JsonConvert.SerializeObject(save);
        }

        /// <summary>
        /// Create a save from Json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>The save created from the Json.</returns>
        public static Save FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Save>(json);
        }

        /// <summary>
        /// Persist a save to a file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="save">The save to persist.</param>
        /// <param name="message">A message detailing the result of the save, if the save was unsuccessful. If the save was successful this will be empty.</param>
        /// <returns>True if the save was successful else false.</returns>
        public static bool ToFile(string path, Save save, out string message)
        {
            try
            {
                var json = ToJson(save);
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
        /// Return a save from a file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="save">The save.</param>
        /// <param name="message">A message detailing the result of the load, if the load was unsuccessful. If the load was successful this will be empty.</param>
        /// <returns>True if the load was successful else false.</returns>
        public static bool FromFile(string path, out Save save, out string message)
        {
            try
            {
                string json;

                using (var reader = new StreamReader(path))
                    json = reader.ReadToEnd();

                save = FromJson(json);
                message = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                save = null;
                message = e.Message;
                return false;
            }
        }
    }
}
