using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace Esmiylara.Serializers
{
    /// <summary>
    /// Json serializer, reads and writes objects to and from files.
    /// </summary>
    /// <typeparam name="Type">The object type to work with.</typeparam>
    public static class JsonSerializer<Type>
    {
#if DEBUG
        /// <summary>
        /// The json serializer format.
        /// </summary>
        public const Formatting DefaultFormatting = Formatting.Indented;
#else
        /// <summary>
        /// The json serializer format.
        /// </summary>
        public const Formatting DefaultFormatting = Formatting.None;
#endif

        /// <summary>
        /// Internal settings object.
        /// </summary>
        private static JsonSerializerSettings settings;

        /// <summary>
        /// Constructs a new Json serializer instance.
        /// </summary>
        static JsonSerializer()
        {
            // Make the settings object.
            settings = new JsonSerializerSettings()
            {
                // THIS NEEDS TO BE HERE!
                TypeNameHandling = TypeNameHandling.All
            };
        }

        /// <summary>
        /// Deserializes a json string and returns the object.
        /// </summary>
        /// <param name="json">The json string to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public static Type Deserialize(string json)
        {
            // Deserialize the object.
            return JsonConvert.DeserializeObject<Type>(json, settings);
        }

        /// <summary>
        /// Serializes an object and returns a json string.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="formatting">The formatting type to use.</param>
        /// <returns>The serialized json string.</returns>
        public static string Serialize(Type obj, Formatting formatting = DefaultFormatting)
        {
            // Serialize the object.
            return JsonConvert.SerializeObject(obj, formatting, settings);
        }

        /// <summary>
        /// Deserializes a json file and returns the object.
        /// </summary>
        /// <param name="filename">The file to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public static Type DeserializeFile(string filename)
        {
            // Check to see if the file exists.
            if (File.Exists(Application.dataPath + filename))
            {
                // Open the file.
                using (var fs = new StreamReader(Application.dataPath + Path.DirectorySeparatorChar + filename))
                {
                    // Return the deserialized object.
                    return Deserialize(fs.ReadToEnd());
                }
            }

            // WHOA! The file doesn't exist!
            throw new FileNotFoundException("Unable to deserialize json file, \"" + filename + "\" does not exist!");
        }
        
        /// <summary>
        /// Serializes an object and saves it as a json file.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="filename">The file to save the object to.</param>
        public static void SerializeFile(Type obj, string filename)
        {
            // Open the file.
            using (var fs = new StreamWriter(Application.dataPath + Path.DirectorySeparatorChar + filename, false))
            {
                fs.Write(Serialize(obj));
                fs.Close();
            }
        }
    }
}