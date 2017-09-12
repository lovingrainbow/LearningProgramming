using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    class Program
    {
        [Serializable]
        private class ClsSerializable
        {
            public int intNumber = 12345;
            public string strDemo = "This is a serialization string";

            [NonSerialized]
            public long lngnumber = 654321;
        }
        static void Main(string[] args)
        {
            Program mySerializable = new Program();
            ClsSerializable clsSerializable = new ClsSerializable();

            mySerializable.SerializeBinary(clsSerializable);
            mySerializable.SerializeSoap(clsSerializable);

            mySerializable.PrintObject();
            Console.Read();
        }

        private void SerializeBinary(ClsSerializable o)
        {
            FileStream myFileStream = new FileStream(@"d:\sbinary.txt",FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Console.WriteLine("開始進行二進位格式序列化......");
            binaryFormatter.Serialize(myFileStream, o);
            Console.WriteLine("序列化完成，資料儲存在sbinary.txt......");
            myFileStream.Close();
        }

        private void SerializeSoap(ClsSerializable o)
        {
            FileStream myFileStream = new FileStream(@"d:\ssoap.txt", FileMode.Create);
            SoapFormatter soapFormatter = new SoapFormatter();
            Console.WriteLine("開始進行SOAP格式序列化......");
            soapFormatter.Serialize(myFileStream, o);
            Console.WriteLine("序列化完成，資料儲存在ssoap.txt......");
            myFileStream.Close();
        }

        private ClsSerializable DeSerialize()
        {
            ClsSerializable o;
            FileStream myFileStream = new FileStream(@"d:\sbinary.txt", FileMode.Open);
            FileStream mySoapFileStream = new FileStream(@"d:\ssoap.txt", FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SoapFormatter soapFormatter = new SoapFormatter();
            Console.WriteLine("開始還原序列化物件......");
            //兩種不同的還原序列化物件方式
            //o = (ClsSerializable)binaryFormatter.Deserialize(myFileStream);
            o = (ClsSerializable)soapFormatter.Deserialize(mySoapFileStream);
            Console.WriteLine("物件還原完成......");
            myFileStream.Close();
            return o;
        }

        private void PrintObject()
        {
            Program mySerializable = new Program();

            ClsSerializable clsSerializable = mySerializable.DeSerialize();

            Console.WriteLine("屬性值intNumber = {0}", clsSerializable.intNumber);
            Console.WriteLine("屬性值strDemo = {0}", clsSerializable.strDemo);
            Console.WriteLine("屬性值lngNumber = {0}", clsSerializable.lngnumber);

        }
    }
}
