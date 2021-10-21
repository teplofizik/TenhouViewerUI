using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace XMLFile
{
    static class XMLFile
    {
        public static void SaveXML(this object O, string FileName)
        {
            XmlSerializer Writer = new XmlSerializer(O.GetType());
            
            StreamWriter file = new StreamWriter(FileName);
            Writer.Serialize(file, O);
            file.Close();
        }

        public static T LoadXML<T>(string FileName)
        {
            XmlSerializer Writer = new XmlSerializer(typeof(T));
            T O = default(T);

            StreamReader file = new StreamReader(FileName);
            file.ReadToEnd();
            file.BaseStream.Position = 0;

            O = (T)Writer.Deserialize(file);
            file.Close();

            return O;
        }

    }
}
