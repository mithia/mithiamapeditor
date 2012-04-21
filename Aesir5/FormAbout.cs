using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace Aesir5
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();

            Assembly assembly = Assembly.GetExecutingAssembly();
            labelProduct.Text = ((AssemblyProductAttribute)assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
            labelVersion.Text = string.Format("v{0}.{1}", assembly.GetName().Version.Major, assembly.GetName().Version.Minor);

            labelCopyright.Text = ((AssemblyCopyrightAttribute)assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            labelCompany.Text = ((AssemblyCompanyAttribute)assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0]).Company;
        }

        private void linkLabelUIDev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.elance.com/s/-robert-/");
        }
    }
}
