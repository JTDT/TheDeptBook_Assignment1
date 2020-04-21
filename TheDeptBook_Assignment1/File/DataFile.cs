using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TheDeptBook_Assignment1.DTO;

namespace TheDeptBook_Assignment1
{
    public class DataFile
    {
        private DataFile()
        {

        }
        internal static bool ReadFile(string fileName, out ObservableCollection<Debtor> debtors)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Debtor>));
            TextReader reader = new StreamReader(fileName);
            // Deserialize all the debtors.
            debtors = (ObservableCollection<Debtor>)serializer.Deserialize(reader);
            reader.Close();
            return true;
        }

        internal static void SaveFile(string fileName, ObservableCollection<Debtor> debtors)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Debtor>));
            TextWriter writer = new StreamWriter(fileName);
            // Serialize all the debtors.
            serializer.Serialize(writer, debtors);
            writer.Close();
        }
    }
}
