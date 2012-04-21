using System.Windows.Forms;

namespace Aesir5
{
    public sealed partial class NumberInputForm : Form
    {
        public int Number { get; set; }

        public NumberInputForm(string caption)
        {
            InitializeComponent();
            Text = caption;
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            int number;
            int.TryParse(textBoxNumber.Text, out number);
            Number = number;
        }
    }
}
