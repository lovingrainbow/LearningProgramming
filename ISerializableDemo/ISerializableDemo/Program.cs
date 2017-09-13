using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ISerializableDemo
{
    [Serializable]
    public class ClsISerialization : ISerializable
    {
        public int intNumber = 1000;
        public string message = "這是測試字串";
        public long lngTest = 100;
        public int[] intArrayX = new int[10];
        public int[] intArrayY = new int[10];

        public ClsISerialization()
        {
            ChangeMemberValue();
        }

        private void ChangeMemberValue()
        {
            for(int i = 0; i < intArrayX.Length; i++)
            {
                intArrayX[i] = i * 100;
            }
            intNumber = 2000;
            message = "這是修正後的字串";
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("intNumber", intNumber);
            info.AddValue("message", message);
            info.AddValue("intArrayX", intArrayX);
        }
        public ClsISerialization(SerializationInfo info, StreamingContext context)
        {
            intNumber = (int)info.GetValue("intNumber", typeof(int));
            message = (string)info.GetValue("message", typeof(string));
            intArrayX = (int[])info.GetValue("intArrayX", typeof(int[]));
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            ClsISerialization myClsIS = new ClsISerialization();
            FileStream myFileStream = new FileStream(@"D:\isBinary.txt", FileMode.Create);

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(myFileStream, myClsIS);

            myClsIS = null;
            myFileStream.Close();

            myFileStream = new FileStream(@"D:\isBinary.txt", FileMode.Open);
            myClsIS = (ClsISerialization)binaryFormatter.Deserialize(myFileStream);

            myFileStream.Close();

            Console.WriteLine(myClsIS.message);
            Console.WriteLine(myClsIS.intNumber);
            Console.WriteLine(myClsIS.lngTest);
            for(int i = 0; i < myClsIS.intArrayX.Length; i++)
            {
                Console.WriteLine(myClsIS.intArrayX[i]);
            }

            Console.Read();
        }
    }
}
