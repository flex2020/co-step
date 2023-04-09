using System;
using System.Collections.Generic;
using System.Windows;

namespace CO_STEP
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChartWindow : Window
    {
        private static string[] dates = getDates(); // dates에 일주일간 날짜들 저장
        private static int[] patients = xmlParsing1.getNum(); // 최근 7일간 전국 확진자 수 배열
        private static int[] firstCnt = jsonParsing1.getNums(1); // 최근 7일간 전국 누적 1차접종자 수 배열
        private static int[] secondCnt = jsonParsing1.getNums(2); // 최근 7일간 전국 누적 2차접종자 수 배열
        private static List<KeyValuePair<string, int>> list1 = new List<KeyValuePair<string, int>>(); // chart1(확진자 수)의 DataContext
        private static List<KeyValuePair<string, int>> list2 = new List<KeyValuePair<string, int>>(); // chart2(1차접종자 수)의 DataContext
        private static List<KeyValuePair<string, int>> list3 = new List<KeyValuePair<string, int>>(); // chart3(2차접종자 수)의 DataContext
        public ChartWindow()
        {
            InitializeComponent();
            /* List에 요소 저장 */
            setList();
            /* 차트의 DataContext 설정 */
            ColumnChart1.DataContext = list1;
            ColumnChart2.DataContext = list2;
            ColumnChart3.DataContext = list3;
            /* 차트 보여주기 */
            MakeChart();
        }
        /* 콤보박스 선택메뉴에 따라 차트랑 제목 변경 */
        private void MakeChart()
        {
            if(comboBox.SelectedIndex == 0)
            {
                titleImg1.Visibility = Visibility.Visible;
                titleImg2.Visibility = Visibility.Hidden;
                ColumnChart1.Visibility = Visibility.Visible;
                ColumnChart2.Visibility = Visibility.Hidden;
                ColumnChart3.Visibility = Visibility.Hidden;
            }
            else if(comboBox.SelectedIndex == 1)
            {
                titleImg1.Visibility = Visibility.Hidden;
                titleImg2.Visibility = Visibility.Visible;
                ColumnChart1.Visibility = Visibility.Hidden;
                ColumnChart2.Visibility = Visibility.Visible;
                ColumnChart3.Visibility = Visibility.Hidden;
            }
            else if(comboBox.SelectedIndex == 2)
            {
                titleImg1.Visibility = Visibility.Hidden;
                titleImg2.Visibility = Visibility.Visible;
                ColumnChart1.Visibility = Visibility.Hidden;
                ColumnChart2.Visibility = Visibility.Hidden;
                ColumnChart3.Visibility = Visibility.Visible;
            }

        }
        /* 최근 7일 날짜를 저장하는 getDates 함수 */
        private static string[] getDates()
        {
            string[] d = new string[7];
            string currentTime = System.DateTime.Now.ToString("HHmm");
            DateTime today = System.DateTime.Now;
            // 오전 10시이전에는 전날로 취급
            if(Int32.Parse(currentTime) < 1000)
            {
                today = today.AddDays(-1);
            }
            for(int i = 0 ; i < 7 ; i++)
            {
                d[i] = today.AddDays(i-6).ToString("yy-MM-dd"); // 날짜를 00-00-00 형태로 저장
            }
            return d;
        }
        /* List들을 설정하는 함수 */
        private static void setList()
        {
            for (int i = 0; i < 7; i++)
            {
                KeyValuePair<string, int> tmp1 = new KeyValuePair<string, int>(dates[i], patients[i]); // Key : 날짜 ,Value : 확진자 수
                KeyValuePair<string, int> tmp2 = new KeyValuePair<string, int>(dates[i], firstCnt[i]); // Key : 날짜 ,Value : 1차 접종자 수
                KeyValuePair<string, int> tmp3 = new KeyValuePair<string, int>(dates[i], secondCnt[i]); // Key : 날짜 ,Value : 2차 접종자 수
                list1.Add(tmp1);
                list2.Add(tmp2);
                list3.Add(tmp3);
                // List에 요소 추가
            }
        }
        /* 창 닫기 이벤트 */
        private void WindowClosed(object sender, System.EventArgs e)
        {
            // 창켜짐 플래그 false 설정 후 List들 초기화 (초기화 안할 시 다음에 켰을 때 그래프가 여러겹으로 그려짐)
            ((MainWindow)System.Windows.Application.Current.MainWindow).CWD = false;
            list1.Clear();
            list2.Clear();
            list3.Clear();
        }

        /* 메뉴 선택 이벤트 */
        private void SelectMenu(object sender, EventArgs e)
        {
            MakeChart(); // 메뉴에 따라 차트 생성
        }
    }
}
