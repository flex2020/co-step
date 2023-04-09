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
    /// whatWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class whatWindow : Window
    {
        public whatWindow()
        {
            InitializeComponent();
        }

        /* 윈도우 닫기 플래그 */
        private void WindowClosed(object sender, EventArgs e)
        {
            immuneWindow.WWD = false;
        }
    }
}
