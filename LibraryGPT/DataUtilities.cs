using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace LibraryGPT
{
    public static class DataUtilities
    {
        // Convert an object to its JSON representation.
        public static string ConvertObjectToJson<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using var memoryStream = new MemoryStream();
            serializer.WriteObject(memoryStream, obj);
            return Encoding.Default.GetString(memoryStream.ToArray());
        }

        // Convert a JSON string back to an object.
        public static T? ConvertJsonToObject<T>(string json) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using var memoryStream = new MemoryStream(Encoding.Default.GetBytes(json));
            return serializer.ReadObject(memoryStream) as T;
        }

        // Convert an object to its XML representation.
        public static string ConvertObjectToXml<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter);
            serializer.Serialize(xmlWriter, obj);
            return stringWriter.ToString();
        }

        // Convert an XML string back to an object.
        public static T? ConvertXmlToObject<T>(string xml) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using var stringReader = new StringReader(xml);
            return serializer.Deserialize(stringReader) as T;
        }

        // Validate if a string is a valid email format.
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch { return false; }
        }

        // Other data-related utility methods can be added here...
    }
}