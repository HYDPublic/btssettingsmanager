using System.IO;
using System.Xml.Serialization;
using System;

namespace BTSSettingsManager
{
    /// <summary>
    /// Helper class for Serialization / Deserialization.
    /// </summary>
    public class SerializationHelper
    {
        #region Singleton Implementation

        /// <summary>
        /// Instance of SerializationHelper class.
        /// </summary>
        private static volatile SerializationHelper _Instance;

        /// <summary>
        /// Syncronization lock for implementing multithreaded singleton.
        /// </summary>
        private static object _SyncRoot = new Object();

        /// <summary>
        /// Instance of SerializationHelper class.
        /// </summary>
        public static SerializationHelper Instance
        {
            get 
            {
                if (_Instance == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_Instance == null)
                            _Instance = new SerializationHelper();
                    }
                }

                return _Instance;
            }
        }
        
        /// <summary>
        /// Initializes an instance of the SerializationHelper class.
        /// </summary>
        private SerializationHelper() 
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Deserializes the input XML document to the specified typed object.
        /// </summary>
        /// <typeparam name="T">Type of the object to be used in the deserialization.</typeparam>
        /// <param name="input">Input string xml.</param>
        /// <returns>An instance of the informed type with the object being deserialized.</returns>
        public T Deserialize<T>(string input) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringReader reader = new StringReader(input))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Serializes the input typed object into an XML string.
        /// </summary>
        /// <typeparam name="T">Type of the object to be used in the serialization.</typeparam>
        /// <param name="input">Input typed object.</param>
        /// <returns>A XML string containing the input object serialized.</returns>
        public string Serialize<T>(T input) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, input);
                return writer.ToString();
            }
        }

        #endregion
    }
}
