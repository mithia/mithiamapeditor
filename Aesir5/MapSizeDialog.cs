using System.Drawing;
using System.Windows.Forms;

namespace Aesir5
{
    public partial class MapSizeDialog : Form
    {
        public Size MapSize { get; private set; }

        public MapSizeDialog()
        {
            InitializeComponent();
        }

        private void MapSizeDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            MapSize = new Size((int)numericUpDownWidth.Value, (int)numericUpDownHeight.Value);
        }
    }
}
