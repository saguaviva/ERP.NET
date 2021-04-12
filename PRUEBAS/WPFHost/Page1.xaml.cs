using System.Windows.Controls;
using System.Windows.Forms.Integration;
using Fibonacci;

namespace WPFHost
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private readonly MainForm mainForm = new MainForm();

        public Page1()
        {
            InitializeComponent();

            //Create a Windows Forms Host to host a form
            WindowsFormsHost windowsFormsHost = new WindowsFormsHost();
            
            stackPanel.Width =  mainForm.Width;
            stackPanel.Height = mainForm.Height;
            windowsFormsHost.Width = mainForm.Width;
            windowsFormsHost.Height = mainForm.Height;
          
            mainForm.TopLevel = false;
            
            windowsFormsHost.Child = mainForm;
            
            stackPanel.Children.Add(windowsFormsHost);
        }
    }
}
