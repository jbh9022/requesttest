using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace requesttest
{
    class Program
    {
        static string URL = "http://jbh9022.cafe24.com/portTest/update3LED.php";
        private static string postData;
        static void Main(string[] args)
        {
            //보낼정보
            String[] data = new string[2];
            data[0] = "R1";
            data[1] = "1";
            postData = string.Format("color={0}&now={1}", data[0], data[1]);
            //웹 리퀘스트
            HttpCall();
        }

        private static void HttpCall()
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            byte[] sendData = UTF8Encoding.UTF8.GetBytes(postData);
            httpWebRequest.Timeout = 5000;
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            httpWebRequest.ContentLength = sendData.Length;
            Stream requestStream = httpWebRequest.GetRequestStream();
            requestStream.Write(sendData, 0, sendData.Length);
            requestStream.Close();
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            string return_http = streamReader.ReadToEnd();
            streamReader.Close();
            httpWebResponse.Close();

            Console.Write("return: " + return_http);
            Console.ReadKey();
        }
    }
}
