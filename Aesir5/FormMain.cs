using System;
using System.Drawing;
using System.Windows.Forms;

namespace Aesir5
{
    public partial class FormMain : Form
    {
        public int sizeModifier;

        public FormMain()
        {
            InitializeComponent();
            MdiChildActivate += FormMain_MdiChildActivate;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Extract files if required
            StartupUtilities.lblStatus = lblStatus;
            StartupUtilities.CreateMapEditorRegistryKey();
            if(!StartupUtilities.ExtractFiles(Application.StartupPath)) return;

            // Load tiles
            TileManager.lblStatus = lblStatus;
            TileManager.Load(Application.StartupPath);

            // Set forms to be MDI and show them
            fTile = new FormTile { MdiParent = this };
            fObject = new FormObject { MdiParent = this };
            fTile.Show();
            fObject.Show();
        }

        private void FormMain_MdiChildActivate(object sender, EventArgs e)
        {
            if (ActiveMdiChild is FormMap)
            {
                FormMap formMap = (FormMap)ActiveMdiChild;
                //formMap.MinimapWindow.Location = new Point(Width - 280, 20);
                formMap.MinimapWindow.SetImage(formMap.pnlImage.Image);
                formMap.MinimapWindow.Visible = formMap.IsMinimapVisible;
                foreach (Form mdiChild in MdiChildren)
                {
                    if (mdiChild is FormMinimap && mdiChild != formMap.MinimapWindow)
                        mdiChild.Visible = false;
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ImageRenderer.Singleton.Dispose();
        }

        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormMap(this).Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form mdiChild in MdiChildren) mdiChild.Close();
            if (MdiChildren.Length == 0) Close();
        }

        private void x48ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Yes;

            foreach (Form mdiChild in MdiChildren)
            {
                if (mdiChild is FormMap)
                {
                    result = MessageBox.Show("This will cause the map to be rendered again. Proceed?", "Resize Tiles", MessageBoxButtons.YesNo);
                    break;
                }
            }

            if (result == DialogResult.Yes)
            {
                x36ToolStripMenuItem.Checked = false;
                x24ToolStripMenuItem.Checked = false;
                ImageRenderer.Singleton.ClearTileCache();
                ImageRenderer.Singleton.ClearObjectCache();
                ImageRenderer.Singleton.sizeModifier = 48;
                fTile.Reload(true);
                fObject.Reload(true);

                foreach (Form mdiChild in MdiChildren)
                {
                    if (mdiChild is FormMap)
                    {
                        FormMap map = (FormMap)mdiChild;
                        map.Reload(true);
                    }
                }
            }
        }

        private void x36ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Yes;

            foreach (Form mdiChild in MdiChildren)
            {
                if (mdiChild is FormMap)
                {
                    result = MessageBox.Show("This will cause the map to be rendered again. Proceed?", "Resize Tiles", MessageBoxButtons.YesNo);
                    break;
                }
            }

            if (result == DialogResult.Yes)
            {
                x48ToolStripMenuItem.Checked = false;
                x24ToolStripMenuItem.Checked = false;
                ImageRenderer.Singleton.ClearTileCache();
                ImageRenderer.Singleton.ClearObjectCache();
                ImageRenderer.Singleton.sizeModifier = 36;
                fTile.Reload(true);
                fObject.Reload(true);

                foreach (Form mdiChild in MdiChildren)
                {
                    if (mdiChild is FormMap)
                    {
                        FormMap map = (FormMap)mdiChild;
                        map.Reload(true);
                    }
                }
            }
        }

        private void x24ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Yes;

            foreach (Form mdiChild in MdiChildren)
            {
                if (mdiChild is FormMap)
                {
                    result = MessageBox.Show("This will cause the map to be rendered again. Proceed?", "Resize Tiles", MessageBoxButtons.YesNo);
                    break;
                }
            }

            if (result == DialogResult.Yes)
            {
                x48ToolStripMenuItem.Checked = false;
                x36ToolStripMenuItem.Checked = false;
                ImageRenderer.Singleton.ClearTileCache();
                ImageRenderer.Singleton.ClearObjectCache();
                ImageRenderer.Singleton.sizeModifier = 24;
                fTile.Reload(true);
                fObject.Reload(true);

                foreach (Form mdiChild in MdiChildren)
                {
                    if (mdiChild is FormMap)
                    {
                        FormMap map = (FormMap)mdiChild;
                        map.Reload(true);
                    }
                }
            }
        }
    }
}
