using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

/* 예방접종 */
namespace CO_STEP
{
    class jsonParsing1
    {
        /* 7일전~오늘까지 예방접종데이터를 저장하는 JObject 객체 */
        private static JObject json1 = loadJson_before(0);
        private static JObject json2 = loadJson_before(1);
        private static JObject json3 = loadJson_before(2);
        private static JObject json4 = loadJson_before(3);
        private static JObject json5 = loadJson_before(4);
        private static JObject json6 = loadJson_before(5);
        private static JObject json7 = loadJson_before(6);
        private static JObject json8 = loadJson_before(7);
        /* 지역의 수를 저장하는 변수 len */
        private static int len = Int32.Parse(json1["currentCount"].ToString());
        /* 매일 변화하는 JSON 파일의 지역 순서를 고정적인 순서로 정렬하여 새로운 인덱스 배열을 생성*/
        private static int[] idx = Index();

        /* 인구수를 저장하는 배열(2021년 5월 기준) */
        private static double[] population = { 51683025, 9575355, 3367334, 2402940, 2936382, 1457161, 1443154, 1127175, 362036, 13488910, 1535491, 1596955, 2117260, 1794682, 1842423, 2633592, 3327298, 674877 };

        /* CO-STEP계산에 쓰이는 함수 */
        public static double[] getHowManyDays()
        {
            // day배열에 1차,2차 CO-STEP 저장
            // p에 전국의 인구 수 저장
            // i에 전국의 인덱스 저장
            double[] day = new double[2];
            double p = (int)population[0];
            int i = idx[0];
            // 7일간 접종 수를 계산하여 7일간 하루 평균 접종자 수를 계산하여 앞으로 백신을 맞아야할 사람들로 나눈 것 => CO-STEP
            int numBefore1 = Int32.Parse(json8["data"][i]["totalFirstCnt"].ToString()); // 1차접종의 일주일전 통계
            int numCurrent1 = Int32.Parse(json1["data"][i]["totalFirstCnt"].ToString()); // 1차접종 오늘의 통계
            int numBefore2 = Int32.Parse(json8["data"][i]["totalSecondCnt"].ToString()); // 2차 접종의 일주일전 통계
            int numCurrent2 = Int32.Parse(json1["data"][i]["totalSecondCnt"].ToString()); // 2차 접종의 오늘의 통계
            int gap1 = numCurrent1 - numBefore1; // 1차 7일간 접종량
            int gap2 = numCurrent2 - numBefore2; // 2차 7일간 접종량
            day[0] = (int)((p * 0.7 - numCurrent1) / (gap1 / 7.0)); // (전체 인구 70% -  현재 맞은사람) / (최근 7일간 하루평균) 1차
            day[1] = (int)((p * 0.7 - numCurrent2) / (gap2 / 7.0)); // (전체 인구 70% -  현재 맞은사람) / (최근 7일간 하루평균) 2차
            return day;
        }
        /* 7일간 누적 접종자를 리턴하는 함수 */
        public static int[] getNums(int n)
        {
            // n이 1이면 1차 접종, n이 2이면 2차접종
            string TARGET = "";
            if (n == 1) TARGET = "totalFirstCnt";
            else if (n == 2) TARGET = "totalSecondCnt";
            else return null;
            // 7일간 1차접종자 수를 itemArr에 저장하여 리턴
            int[] itemArr = new int[7];
            int i = idx[0]; // 전국의 인덱스를 i에 저장
            // itemArr에 7일간 누적접종자를 저장
            itemArr[0] = Int32.Parse(json7["data"][i][TARGET].ToString());
            itemArr[1] = Int32.Parse(json6["data"][i][TARGET].ToString());
            itemArr[2] = Int32.Parse(json5["data"][i][TARGET].ToString());
            itemArr[3] = Int32.Parse(json4["data"][i][TARGET].ToString());
            itemArr[4] = Int32.Parse(json3["data"][i][TARGET].ToString());
            itemArr[5] = Int32.Parse(json2["data"][i][TARGET].ToString());
            itemArr[6] = Int32.Parse(json1["data"][i][TARGET].ToString());

            return itemArr;
        }
        /* 지역별 접종률 계산하는 함수 */
        public static string[] getPer(int n)
        {
            // n이 1이면 1차 접종, n이 2이면 2차접종
            string TARGET = "";
            if (n == 1) TARGET = "totalFirstCnt";
            else if (n == 2) TARGET = "totalSecondCnt";
            else return null;

            double[] tmp = new double[len];
            string[] per = new string[len];
            for (int i = 0; i < len; i++)
            {
                int j = idx[i]; // j에는 임의로 정한 지역의 순서대로 인덱스가 들어감
                tmp[i] = Double.Parse(json1["data"][j][TARGET].ToString()); // 누적접종자를 tmp에 저장
                tmp[i] = Math.Round(tmp[i] / population[i] * 100, 2); // % 계산 후 소수점2자리까지 반올림
                per[i] = " " + tmp[i].ToString() + "% ";
            }
            return per;
        }
        /* 금일 접종자 계산 함수 */
        public static string[] getCnt(int n)
        {
            // n이 1이면 1차 접종, n이 2이면 2차접종
            string TARGET = "";
            if (n == 1) TARGET = "firstCnt";
            else if (n == 2) TARGET = "secondCnt";
            else return null;

            string[] cnt = new string[len];
            for (int i = 0; i < len; i++)
            {
                int j = idx[i];
                cnt[i] = json1["data"][j][TARGET].ToString(); // 금일 접종자를 cnt에 저장
                // 1,000이상 인 상황, 1,000,000 이상인 상황에 , 붙이기
                if (cnt[i].Length > 3)
                {
                    cnt[i] = cnt[i].Insert(cnt[i].Length - 3, ","); 
                    if (cnt[i].Length > 7)
                    {
                        cnt[i] = cnt[i].Insert(cnt[i].Length - 7, ",");
                    }
                }
                cnt[i] = cnt[i].Insert(0, " +");
                cnt[i] += "명 ";

            }
            return cnt;
        }
        /* 누적 계산 함수 */
        public static string[] getAccCnt(int n)
        {
            // n이 1이면 1차 접종, n이 2이면 2차접종
            string TARGET = "";
            if (n == 1) TARGET = "totalFirstCnt";
            else if (n == 2) TARGET = "totalSecondCnt";
            else return null;
            string[] accCnt = new string[len];
            for (int i = 0; i < len; i++)
            {
                int j = idx[i];
                accCnt[i] = json1["data"][j][TARGET].ToString(); // accCnt에 누적접종자 저장
                // 1,000이상 인 상황, 1,000,000 이상인 상황에 , 붙이기
                if (accCnt[i].Length > 3)
                {
                    accCnt[i] = accCnt[i].Insert(accCnt[i].Length - 3, ",");
                    if (accCnt[i].Length > 7)
                    {
                        accCnt[i] = accCnt[i].Insert(accCnt[i].Length - 7, ",");
                    }
                }
                accCnt[i] = accCnt[i].Insert(0, " +");
                accCnt[i] += "명 ";
            }
            return accCnt;
        }
        /* 길이 반환 함수 */
        public static int getLen() { return len; }
        /* 기준일시 반환 함수 */
        public static string getDate()
        {
            string date = json1["data"][0]["baseDate"].ToString();
            return date;
        }

        /* 매일 변화하는 JSON 파일의 지역 순서를 고정적인 순서로 정렬하여 새로운 인덱스 배열을 생성*/
        public static int[] Index()
        {
            int[] index = new int[len];
            string[] sido = new string[len];
            string[] tmp = { "전국", "서울특별시", "부산광역시", "대구광역시", "인천광역시", "대전광역시", "광주광역시", "울산광역시", "세종특별자치시", "경기도", "강원도", "충청북도", "충청남도", "전라북도", "전라남도", "경상북도", "경상남도", "제주특별자치도" };
            for (int i = 0; i < len; i++)
            {
                sido[i] = json1["data"][i]["sido"].ToString(); // sido배열에 json에서 얻어온 지역을 저장
            }
            for (int i = 0; i < len; i++)
            {
                index[i] = Array.IndexOf(sido, tmp[i]); // index배열에 sido배열에서 tmp[i]번째 요소를 찾아 그 인덱스를 저장
            }
            return index;
        }

        /* n일전 정보를 담고있는 JObject객체를 반환하는 함수 */
        private static JObject loadJson_before(int n)
        {
            const string mykey = "API키";
            string date = System.DateTime.Now.AddDays(-n).ToString("yyyy-MM-dd");
            string time = System.DateTime.Now.ToString("HHmm");
            if (Int32.Parse(time) < 1000) // 오전 10시 이전이면 전날로 취급
            {
                DateTime d = DateTime.Now.AddDays(-1-n);
                date = d.ToString("yyyy-MM-dd");
            }
            string url = "https://api.odcloud.kr/api/15077756/v1/vaccine-stat?page=1&perPage=20&returnType=JSON&cond%5BbaseDate%3A%3AEQ%5D=" + date + "%2000%3A00%3A00&serviceKey=" + mykey;
            WebClient wc = new WebClient();
            WebRequest wrq1 = WebRequest.Create(url);
            wrq1.Method = "GET";
            WebResponse wrs1 = wrq1.GetResponse();
            Stream s1 = wrs1.GetResponseStream();
            StreamReader sr1 = new StreamReader(s1);
            string response1 = sr1.ReadToEnd();
            JObject json = JObject.Parse(response1);

            return json;
        }
    }
}
