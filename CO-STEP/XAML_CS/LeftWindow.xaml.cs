using System;
using System.Windows;

namespace CO_STEP
{
    /// <summary>
    /// LeftWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LeftWindow : Window
    {
        string[] incDec = xmlParsing1.todayAll(); // 오늘 지역별 확진자 수를 나타낸 배열
        string total = xmlParsing1.getTotal(); // 오늘의 총 확진자 수를 나타내는 변수
        public LeftWindow()
        {
            InitializeComponent();
            Init(); // 초기화 함수 호출
        }

        /* 윈도우 닫기 이벤트 */
        private void WindowClosed(object sender, EventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).LWD = false; // 플래그 false
        }

        /* Label 초기화 함수 */
        private void Init()
        {
            cntLabel_Geomyeok.Content = incDec[0];
            cntLabel_Jeju.Content = incDec[1];
            cntLabel_GyeongNam.Content = incDec[2];
            cntLabel_Gyeongbuk.Content = incDec[3];
            cntLabel_Jeonnam.Content = incDec[4];
            cntLabel_Jeonbuk.Content = incDec[5];
            cntLabel_Chungnam.Content = incDec[6];
            cntLabel_Chungbuk.Content = incDec[7];
            cntLabel_Gangwon.Content = incDec[8];
            cntLabel_Gyeonggi.Content = incDec[9];
            cntLabel_Sejong.Content = incDec[10];
            cntLabel_Ulsan.Content = incDec[11];
            cntLabel_Daejeon.Content = incDec[12];
            cntLabel_Gwangju.Content = incDec[13];
            cntLabel_Incheon.Content = incDec[14];
            cntLabel_Daegu.Content = incDec[15];
            cntLabel_Busan.Content = incDec[16];
            cntLabel_Seoul.Content = incDec[17];
            cntLabel_Today.Content = incDec[18];
            allLabel.Content = total;
            stdDate_Label.Content += xmlParsing1.getDate();
        }
    }
}
