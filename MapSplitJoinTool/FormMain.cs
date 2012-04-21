using System;
using System.IO;
using System.Windows.Forms;
using Aesir5;

namespace MapSplitJoinTool
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            StartupUtilities.lblStatus = lblStatus;
            TileManager.lblStatus = lblStatus;

            if (!StartupUtilities.ExtractFiles(Application.StartupPath)) return;
            TileManager.Load(Application.StartupPath);
        }

        private void buttonMapSplitFind_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = @"NexusTK Map Files|*.map|Encrypted NexusTK Map Files|*.mape" };
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxMapSplitPath.Text = openFileDialog.FileName;
            }
        }

        private void buttonMapJoinFind_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = @"NexusTK Split Map Files|*.maps|Encrypted NexusTK Split Map Files|*.mapes" };
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxMapJoinPath.Text = openFileDialog.FileName;
            }
        }

        private void buttonSplit_Click(object sender, EventArgs e)
        {
            try
            {
                string path = textBoxMapSplitPath.Text;
                bool encrypted = path.ToLowerInvariant().EndsWith("mape");
                Map map = new Map(path, encrypted);
                SplitJoin.SplitMap(map, (int)numericUpDownSplit.Value, encrypted, Path.GetDirectoryName(path), checkBoxSaveImages.Checked);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonJoin_Click(object sender, EventArgs e)
        {
            try
            {
                SplitJoin.JoinMap(textBoxMapJoinPath.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
