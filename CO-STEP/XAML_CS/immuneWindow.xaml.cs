using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CO_STEP
{
    /// <summary>
    /// immuneWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class immuneWindow : Window
    {
        /* 집단면역 이미지 창 플래그 */
        public static bool WWD = false;
        public immuneWindow()
        {
            InitializeComponent();
            setText(); // Label의 내용을 설정하는 함수
        }

        /* Label 내용 설정 함수 */
        private void setText()
        {
            double[] day = jsonParsing1.getHowManyDays(); // 1차, 2차 CO-STEP을 받아옴
            string date1 = System.DateTime.Now.AddDays((int)day[0]).ToString("yyyy년 MM월 dd일"); // 1차 예상 날짜
            string date2 = System.DateTime.Now.AddDays((int)day[1]).ToString("yyyy년 MM월 dd일"); // 2차 예상 날짜
            Label1.Content = "이 정도 속도라면        CO-STEP 남았네요!\n예상날짜 : " + date1;
            Label2.Content = "이 정도 속도라면        CO-STEP 남았네요!\n예상날짜 : " + date2;
            costep1.Content = ((int)day[0]).ToString();
            costep2.Content = ((int)day[1]).ToString();
        }

        /* 윈도우 닫기 플래그 */
        private void WindowClosed(object sender, EventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).IWD = false;
        }

        /* 버튼 클릭 이벤트 */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 창이 안열려있으면 창을 새로 열어서 이미지를 보여줌
            if (WWD) return;
            else
            {
                WWD = true;
                whatWindow wwindow = new whatWindow();
                wwindow.Show();
            }

        }
    }
}
