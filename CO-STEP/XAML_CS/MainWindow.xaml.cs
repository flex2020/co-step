using System.Windows;

namespace CO_STEP
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        /* 윈도우 창 플래그 */
        public bool LWD = false;
        public bool RWD = false;
        public bool CWD = false;
        public bool IWD = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        /* 버튼 클릭 이벤트 */
        private void MenuClick(object sender, RoutedEventArgs e)
        {
            /* 확진자 정보 확인 */
            if (sender == Button1)
            {
                // 플래그가 false면 창이 꺼진 상태, true면 창이 켜져있는 상태
                // 플래그가 false인 경우에만 창을 열음
                if (LWD) return;
                else
                {
                    LWD = true;
                    CO_STEP.LeftWindow lwindow = new CO_STEP.LeftWindow();
                    lwindow.Show();
                }
            }
            /* 예방접종 확인 */
            else if (sender == Button2)
            {
                if (RWD) return;
                else
                {
                    RWD = true;
                    CO_STEP.RightWindow rwindow = new CO_STEP.RightWindow();
                    rwindow.Show();
                }
            }
            /* 그래프 보기 */
            else if (sender == Button3)
            {
                if (CWD) return;
                else
                {
                    CWD = true;
                    CO_STEP.ChartWindow cwindow = new CO_STEP.ChartWindow();
                    cwindow.Show();
                }
            }
            /* CO-STEP 확인 */
            else if (sender == Button4)
            {
                if (IWD) return;
                else
                {
                    IWD = true;
                    CO_STEP.immuneWindow iwindow = new CO_STEP.immuneWindow();
                    iwindow.Show();
                }
            }
        }
    }
}
