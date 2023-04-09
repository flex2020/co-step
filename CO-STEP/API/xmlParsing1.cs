using System;
using System.IO;
using System.Net;
using System.Xml;

/* 확진자 */
namespace CO_STEP
{
    class xmlParsing1
    {
        // n일전 데이터를 갖고 있는 XmlNode들
        static XmlNode xn1 = loadXml_before(0); // 오늘
        static XmlNode xn2 = loadXml_before(1); // 1일전
        static XmlNode xn3 = loadXml_before(2); // 2일전
        static XmlNode xn4 = loadXml_before(3); // 3일전
        static XmlNode xn5 = loadXml_before(4); // 4일전
        static XmlNode xn6 = loadXml_before(5); // 5일전
        static XmlNode xn7 = loadXml_before(6); // 6일전
        static int childnodes = xn1.ChildNodes.Count; // 검역~합계까지 지역 수 = 19개를 저장
        // 검역, 제주, 경남, 경북, 전남, 전북, 충남, 충북, 강원, 경기, 세종, 울산, 대전, 광주, 인천, 대구, 부산, 서울, 합계

        /* 오늘 확진자 수를 지역별로 나누어 저장하는 함수 */
        public static string[] todayAll()
        {
            string[] itemArr = new string[childnodes]; 
            for (int i = 0; i < childnodes; i++)
            {
                // today : 오늘 확진자, yesterday : 어제 확진자, gap : yesterday - today(어제오늘 차이), 증가했으면 +, 감소했으면 -를 붙여줌. 0이면 +
                string today = (Int32.Parse(xn1.ChildNodes[i]["localOccCnt"].InnerText) + Int32.Parse(xn1.ChildNodes[i]["overFlowCnt"].InnerText)).ToString();
                string yesterday = (Int32.Parse(xn2.ChildNodes[i]["localOccCnt"].InnerText) + Int32.Parse(xn2.ChildNodes[i]["overFlowCnt"].InnerText)).ToString();
                string gap = (Int32.Parse(today) - Int32.Parse(yesterday)).ToString();
                if (gap[0] != '-') gap = gap.Insert(0, "+");
                itemArr[i] = " " + today + "명(" + gap + ")" + " ";

            }
            return itemArr;
        }
        /* 최근 7일 전국 일일 확진자 수를 저장한 배열을 리턴하는 함수 */
        public static int[] getNum()
        {
            int[] itemArr = new int[7];
            // localOccCnt : 지역발생 , overFlowCnt : 해외유입
            // 두개를 더하여 itemArr에 저장
            itemArr[0] = Int32.Parse(xn7.ChildNodes[18]["localOccCnt"].InnerText) + Int32.Parse(xn7.ChildNodes[18]["overFlowCnt"].InnerText);
            itemArr[1] = Int32.Parse(xn6.ChildNodes[18]["localOccCnt"].InnerText) + Int32.Parse(xn6.ChildNodes[18]["overFlowCnt"].InnerText);
            itemArr[2] = Int32.Parse(xn5.ChildNodes[18]["localOccCnt"].InnerText) + Int32.Parse(xn5.ChildNodes[18]["overFlowCnt"].InnerText);
            itemArr[3] = Int32.Parse(xn4.ChildNodes[18]["localOccCnt"].InnerText) + Int32.Parse(xn4.ChildNodes[18]["overFlowCnt"].InnerText);
            itemArr[4] = Int32.Parse(xn3.ChildNodes[18]["localOccCnt"].InnerText) + Int32.Parse(xn3.ChildNodes[18]["overFlowCnt"].InnerText);
            itemArr[5] = Int32.Parse(xn2.ChildNodes[18]["localOccCnt"].InnerText) + Int32.Parse(xn2.ChildNodes[18]["overFlowCnt"].InnerText);
            itemArr[6] = Int32.Parse(xn1.ChildNodes[18]["localOccCnt"].InnerText) + Int32.Parse(xn1.ChildNodes[18]["overFlowCnt"].InnerText);
            return itemArr;
        }
        /* 오늘까지 누적확진자를 리턴하는 함수 */
        public static string getTotal()
        {
            string total = xn1.ChildNodes[18]["defCnt"].InnerText;
            if (total.Length > 3) total = total.Insert(total.Length - 3, ",");
            return " " + total + "명 ";
        }
        /* 기준날짜를 받아오는 함수 */
        public static string getDate()
        {
            string date = "";

            date = xn1.ChildNodes[0]["stdDay"].InnerText;
            return date;
        }
        /* API를 호출하여 xml노드를 만드는 함수, 매개변수 : 오늘로부터 지난 날짜 (오늘 : 0 , 어제 : 1) */
        private static XmlNode loadXml_before(int n)
        {
            const string mykey = "API키";
            string dateBefore = System.DateTime.Now.AddDays(-n).ToString("yyyyMMdd");
            string time = System.DateTime.Now.ToString("HHmm");
            // 정보 업데이트 시각 9시35분 ~ 10시사이 랜덤한 시각이기 때문에 오전 10시 이전인지 확인 후 이전이면 전 날 정보를 보여줌
            if (Int32.Parse(time) < 1000)
            {
                DateTime date = DateTime.Now.AddDays(-n);
                dateBefore = date.AddDays(-1).ToString("yyyyMMdd");
            }
            string url = "http://openapi.data.go.kr/openapi/service/rest/Covid19/getCovid19SidoInfStateJson?serviceKey=" + mykey + "&pageNo=1&numOfRows=10&startCreateDt=" + dateBefore + "&endCreateDt=" + dateBefore;
            WebClient wc = new WebClient();
            WebRequest wrq1 = WebRequest.Create(url);
            wrq1.Method = "GET";

            WebResponse wrs1 = wrq1.GetResponse();
            Stream s1 = wrs1.GetResponseStream();
            StreamReader sr1 = new StreamReader(s1);
            string response1 = sr1.ReadToEnd();
            XmlDocument xd1 = new XmlDocument();
            xd1.LoadXml(response1);
            XmlNode xn1 = xd1["response"]["body"]["items"];
            return xn1;
        }
    }
}
