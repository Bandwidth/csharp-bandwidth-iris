using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Xunit;

namespace Bandwidth.Iris.Tests
{
    public class Helper
    {
        public const string UserName = "FakeUserName";
        public const string Password = "FakePassword";
        public const string AccountId = "FakeAccountId";

        public static Client CreateClient(string baseUrl = null)
        {
            return Client.GetInstance(AccountId, UserName, Password, baseUrl ?? "http://localhost:3001/");
        }

        public static StringContent CreateXmlContent(object data)
        {
            using (var writer = new Utf8StringWriter())
            {
                var serializer = new XmlSerializer(data.GetType());
                serializer.Serialize(writer, data);
                return new StringContent(writer.ToString(), Encoding.UTF8, "application/xml");
            }
        }

        public async static Task<T> ParseXmlContent<T>(HttpContent content)
        {
            using (var stream = await content.ReadAsStreamAsync())
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }

        public static T ParseXml<T>(string xml)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }

        public static void AssertObjects(object estimated, object value)
        {
            var type = estimated.GetType();
            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var est = property.GetValue(estimated);
                var val = property.GetValue(value);
                if (est == null && val == null) continue;
                var t = (val ?? est).GetType();
                if (t.IsPrimitive || val is IComparable)
                {
                    Assert.Equal(est, val);
                }
                else
                {
                    var valCollection = val as IList;
                    var estCollection = est as IList;
                    if (valCollection != null && estCollection != null)
                    {
                        Assert.Equal(estCollection.Count, valCollection.Count);
                        for (var i = 0; i < estCollection.Count; i++)
                        {
                            if (estCollection[i] is IComparable)
                            {
                                Assert.Equal(estCollection[i], valCollection[i]);
                            }
                            else
                            {
                                AssertObjects(estCollection[i], valCollection[i]);
                            }
                        }
                    }
                    else
                    {
                        AssertObjects(est, val);
                    }
                }
            }
        }

        public static string ToXmlString(object data)
        {
            var serializer = new XmlSerializer(data.GetType());
            using (var writer = new Utf8StringWriter())
            {
                serializer.Serialize(writer, data);
                return writer.ToString();
            }
        }

        public static string ToXmlStringMinified(object data)
        {
            var serializer = new XmlSerializer(data.GetType());



            using (var writer = new Utf8StringWriter())
            using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = false, OmitXmlDeclaration = true }))
            {
                serializer.Serialize(xmlWriter, data);
                return writer.ToString();
            }
        }

        public static void SetEnvironmentVariables(string baseUrl = null)
        {
            Environment.SetEnvironmentVariable(Client.BandwidthApiUserName, UserName);
            Environment.SetEnvironmentVariable(Client.BandwidthApiPassword, Password);
            Environment.SetEnvironmentVariable(Client.BandwidthApiAccountId, AccountId);
            Environment.SetEnvironmentVariable(Client.BandwidthApiEndpoint, baseUrl ?? "http://localhost:3001/");
            Environment.SetEnvironmentVariable(Client.BandwidthApiVersion, "v1.0");
        }
    }
}
