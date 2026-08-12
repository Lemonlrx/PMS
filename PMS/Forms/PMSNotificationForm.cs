using System.Drawing;
using System.Windows.Forms;

namespace PMS.Controls
{
    public partial class PMSNotificationForm : Form
    {
        public PMSNotificationForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;

            BackColor = Color.FromArgb(28, 29, 34);
        }
    }
}