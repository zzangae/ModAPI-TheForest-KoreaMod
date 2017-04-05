using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Teleporter
{
    internal class Xml
    {
        public void Create(string path)
        {
            XmlDocument expr_05 = new XmlDocument();
            XmlDeclaration newChild = expr_05.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement documentElement = expr_05.DocumentElement;
            expr_05.InsertBefore(newChild, documentElement);
            XmlElement newChild2 = expr_05.CreateElement("body");
            expr_05.AppendChild(newChild2);
            expr_05.Save(path + "tp.xml");
        }

        public void Update(string path, string name, float x, float y, float z)
        {
            XmlDocument expr_05 = new XmlDocument();
            expr_05.Load(path + "tp.xml");
            XmlNode xmlNode = expr_05.SelectSingleNode("/body");
            XmlElement xmlElement = expr_05.CreateElement("Location");
            XmlAttribute xmlAttribute = expr_05.CreateAttribute("x");
            xmlAttribute.Value = x.ToString();
            xmlElement.Attributes.Append(xmlAttribute);
            xmlAttribute = expr_05.CreateAttribute("y");
            xmlAttribute.Value = y.ToString();
            xmlElement.Attributes.Append(xmlAttribute);
            xmlAttribute = expr_05.CreateAttribute("z");
            xmlAttribute.Value = z.ToString();
            xmlElement.Attributes.Append(xmlAttribute);
            xmlAttribute = expr_05.CreateAttribute("name");
            xmlAttribute.Value = name;
            xmlElement.Attributes.Append(xmlAttribute);
            xmlNode.AppendChild(xmlElement);
            expr_05.Save(path + "tp.xml");
        }

        public System.Collections.Generic.List<Location> Read(string path)
        {
            XmlDocument expr_05 = new XmlDocument();
            expr_05.Load(path + "tp.xml");
            XmlNodeList arg_26_0 = expr_05.SelectNodes("/body/Location");
            System.Collections.Generic.List<Location> list = new System.Collections.Generic.List<Location>();
            foreach (XmlNode expr_3C in arg_26_0)
            {
                XmlAttribute xmlAttribute = expr_3C.Attributes["name"];
                XmlAttribute xmlAttribute2 = expr_3C.Attributes["x"];
                XmlAttribute xmlAttribute3 = expr_3C.Attributes["y"];
                XmlAttribute xmlAttribute4 = expr_3C.Attributes["z"];
                string value = xmlAttribute.Value;
                float arg_B6_0 = float.Parse(xmlAttribute2.Value);
                float y = float.Parse(xmlAttribute3.Value);
                float z = float.Parse(xmlAttribute4.Value);
                Location item = new Location(arg_B6_0, y, z, value);
                list.Add(item);
            }
            return list;
        }

        public void Delete(string path, Location location)
        {
            XmlDocument expr_05 = new XmlDocument();
            expr_05.Load(path + "tp.xml");
            XmlNode xmlNode = expr_05.SelectSingleNode("/body/Location[@name='" + location.GetName() + "']");
            xmlNode.ParentNode.RemoveChild(xmlNode);
            expr_05.Save(path + "tp.xml");
        }
    }
}
