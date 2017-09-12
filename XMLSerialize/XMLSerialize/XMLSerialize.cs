using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLSerialize
{
    [Serializable]
    public class ClsSerialize
    {
        public int intNumber = 123;
        public string strDemo = "This is a demo string";

        private int intPrice = 5453;
        private string strItem = "This is a private string";

        private int _intCount;
        private string _strResult;

        public ClsSerialize()
        {
            _intCount = 333;
            _strResult = "Result is nothing";
        }

        public int intCount
        {
            get
            { return this._intCount; }
        }
        public string strResult
        {
            get
            { return this._strResult; }
        }


    }
    class XMLSerialize
    {
        
        static void Main(string[] args)
        {
            ClsSerialize clsSerialize = new ClsSerialize();
            XMLSerialize myXMLSerialize = new XMLSerialize();

            ClsSerialize myNewCLSSerialize;
            //start serialize
            myXMLSerialize.SerializeXML(clsSerialize);
            //start deserialize
            myNewCLSSerialize = (ClsSerialize)myXMLSerialize.DeserializeXML();
            //print result

            Console.WriteLine("屬性intNumber - {0}", myNewCLSSerialize.intNumber);
            Console.WriteLine("屬性strDemo - {0}", myNewCLSSerialize.strDemo);

            Console.WriteLine("屬性intCount - {0}", myNewCLSSerialize.intCount);
            Console.WriteLine("屬性strResult - {0}", myNewCLSSerialize.strResult);

            Console.Read();
        }

        private void SerializeXML(ClsSerialize o)
        {
            using (FileStream myFileStream = new FileStream(@"D:\sxml.txt", FileMode.Create))
            {
                XmlSerializer myXmlSerializer = new XmlSerializer(typeof(ClsSerialize));
                Console.WriteLine("序列化開始，資料儲存在sxml.txt......");
                myXmlSerializer.Serialize(myFileStream, o);
                Console.WriteLine("序列化物件完成......");
                myFileStream.Close();

            }
        }

        private ClsSerialize DeserializeXML()
        {
            ClsSerialize o;
            using (FileStream myFileStream = new FileStream(@"D:\sxml.txt", FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ClsSerialize));
                Console.WriteLine("還原序列化物件開始......");
                o = (ClsSerialize)xmlSerializer.Deserialize(myFileStream);
                Console.WriteLine("還原序列化物件完成......");
                myFileStream.Close();
                return o;
            }
        }
    }
}
