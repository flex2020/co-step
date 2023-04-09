using System;
using System.Windows;

namespace CO_STEP
{
    /// <summary>
    /// RightWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RightWindow : Window
    {
        private string[] firstCnt = jsonParsing1.getCnt(1); // 지역별 금일 1차 접종자
        private string[] secondCnt = jsonParsing1.getCnt(2); // 지역별 금일 2차 접종자
        private string[] accfirstCnt = jsonParsing1.getAccCnt(1); // 지역별 누적 1차 접종자
        private string[] accsecondCnt = jsonParsing1.getAccCnt(2); // 지역별 누적 2차 접종자
        private int len = jsonParsing1.getLen(); // 받아오는 지역의 수
        private string[] totalFirst_per = jsonParsing1.getPer(1); // 지역별 1차 % 수치
        private string[] totalSecond_per = jsonParsing1.getPer(2); // 지역별 2차 % 수치
        public RightWindow()
        {
            InitializeComponent();
            Init(); // Label 초기 설정
        }

        /* 윈도우 닫기 이벤트 */
        private void WindowClose(object sender, EventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).RWD = false; // 플래그를 false로 설정
        }
        /* 초기 Label 설정 함수 */
        private void Init()
        {
            Label_Sum.Content = "전국 누적접종자 : ";
            setLabels(firstCnt); // 초기에는 1차 접종자 수로 Label들 설정
            allLabel.Content = accfirstCnt[0]; // 전국확진자 수
            stdDate_Label.Content += jsonParsing1.getDate(); // 기준일시 추가
        }
        /* 콤보박스 메뉴마다 Label 설정 */
        private void setLabels(string[] currentArr)
        {
            /* 오늘의 접종자 수를 확인 할 때*/
            if(ComboBox1.SelectedIndex == 0 || ComboBox1.SelectedIndex == 1)
            {
                cntLabel_Today.Content = currentArr[0];
                if(ComboBox1.SelectedIndex == 0) allLabel.Content = accfirstCnt[0];
                else allLabel.Content = accsecondCnt[0];
            }
            /* 누적 접종자 수를 확인 할 때 */
            else if((ComboBox1.SelectedIndex == 2 || ComboBox1.SelectedIndex == 3) && CheckBox1.IsChecked == false)
            {
                allLabel.Content = currentArr[0];
                if (ComboBox1.SelectedIndex == 2) cntLabel_Today.Content = firstCnt[0];
                else cntLabel_Today.Content = secondCnt[0];
            }
            /* 누적 접종률을 확인 할 때 */ 
            else if((ComboBox1.SelectedIndex == 2 || ComboBox1.SelectedIndex == 3) && CheckBox1.IsChecked == true)
            {
                allLabel.Content = currentArr[0];
                if (ComboBox1.SelectedIndex == 2) cntLabel_Today.Content = firstCnt[0];
                else cntLabel_Today.Content = secondCnt[0];

            }
            /* 매개변수로 설정된 배열에 저장된 값을 각 지역별로 저장 */
            cntLabel_Seoul.Content = currentArr[1];
            cntLabel_Busan.Content = currentArr[2];
            cntLabel_Daegu.Content = currentArr[3];
            cntLabel_Incheon.Content = currentArr[4];
            cntLabel_Daejeon.Content = currentArr[5];
            cntLabel_Gwangju.Content = currentArr[6];
            cntLabel_Ulsan.Content = currentArr[7];
            cntLabel_Sejong.Content = currentArr[8];
            cntLabel_Gyeonggi.Content = currentArr[9];
            cntLabel_Gangwon.Content = currentArr[10];
            cntLabel_Chungbuk.Content = currentArr[11];
            cntLabel_Chungnam.Content = currentArr[12];
            cntLabel_Jeonbuk.Content = currentArr[13];
            cntLabel_Jeonnam.Content = currentArr[14];
            cntLabel_Gyeongbuk.Content = currentArr[15];
            cntLabel_GyeongNam.Content = currentArr[16];
            cntLabel_Jeju.Content = currentArr[17];
        }

        /* 콤보박스 이벤트 */
        private void MenuSelect(object sender, EventArgs e)
        {
            int sel = ComboBox1.SelectedIndex; // 현재 선택된 콤보박스 메뉴
            CheckBox1.IsChecked = false; // 메뉴 이동 시 체크박스 해제
            string[] currentArr = null; // selectLabel 함수의 매개변수로 넘겨줄 currentArr
            if (sel == 0)
            {
                CheckBox1.Visibility = Visibility.Hidden; // 체크박스 안보이게 설정
                currentArr = firstCnt; // currentArr를 금일 1차접종자 수로 설정
            }
            else if (sel == 1)
            {
                CheckBox1.Visibility = Visibility.Hidden; // 체크박스 안보이게 설정
                currentArr = secondCnt; // currentArr를 금일 2차접종자 수로 설정
            }
            else if (sel == 2)
            {
                CheckBox1.Visibility = Visibility.Visible; // 체크박스 보이게 설정
                currentArr = accfirstCnt; // currentArr를 누적 1차접종자 수로 설정
            }
            else if (sel == 3)
            {
                CheckBox1.Visibility = Visibility.Visible; // 체크박스 보이게 설정
                currentArr = accsecondCnt; // currentArr를 누적 2차접종자 수로 설정
            }
            else return;
            setLabels(currentArr); // setLabel함수에게 currentArr를 넘겨줌
        }
        /* 체크박스 체크 이벤트 */
        private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        {
            string[] currentArr = null;
            int sel = ComboBox1.SelectedIndex;
            Label_Sum.Content = "전국 누적접종률 : ";
            if (sel == 2)
            {
                currentArr = totalFirst_per; // currentArr를 1차 접종률로
            }
            else if (sel == 3)
            {
                currentArr = totalSecond_per; // currentArr를 2차 접종률로
            }
            setLabels(currentArr); // setLabel함수에게 currentArr를 넘겨줌
        }

        /* 체크박스 해제 이벤트 */
        private void CheckBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            Label_Sum.Content = "전국 누적접종자 : "; 
            MenuSelect(sender, e); // MenuSelect 함수 호출
        }
    }
}
