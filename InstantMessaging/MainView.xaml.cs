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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InstantMessaging
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private MainWindowDefault windowDefault;
        private MainWindowAlternative windowAlternative;

        public MainView()
        {
            InitializeComponent();

            windowDefault = new MainWindowDefault();
            windowAlternative = new MainWindowAlternative();

           contentControl.Content = windowDefault;
        }

        public void ShowView1()
        {
            contentControl.Content = windowDefault;
        }

        public void ShowView2()
        {
            contentControl.Content = windowAlternative;
        }
    }
}
