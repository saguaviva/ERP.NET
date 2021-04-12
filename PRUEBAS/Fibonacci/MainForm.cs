using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fibonacci
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            txtResult.Text = Fibonacci(10).ToString();
        }

        static int Fibonacci(int x)
        {
            if (x <= 1)
            {
                return 1;
            }
            return Fibonacci(x - 1) + Fibonacci(x - 2);
        }

    }
}
