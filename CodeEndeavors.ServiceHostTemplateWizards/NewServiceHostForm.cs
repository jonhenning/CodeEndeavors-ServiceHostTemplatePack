using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeEndeavors.ServiceHostTemplateWizards
{
    public partial class NewServiceHostForm : Form
    {
        protected Dictionary<string, string> _replacementsDictionary;
        public NewServiceHostForm()
        {
            InitializeComponent();
        }

        public static bool ShowDialog(Dictionary<string, string> replacementsDictionary)
        {
            var form = new NewServiceHostForm();

            form._replacementsDictionary = replacementsDictionary;

            form.folderBrowserDialog1.SelectedPath = Properties.Settings.Default.ServiceHostDir;
            form.txtServiceHostDir.Text = Properties.Settings.Default.ServiceHostDir;
            form.txtServiceHostUrl.Text = Properties.Settings.Default.ServiceHostUrl;

            return form.ShowDialog() == DialogResult.OK;
        }

        private bool validForm()
        {
            var err = "";
            var serviceHostDir = Path.Combine(_replacementsDictionary["$destinationdirectory$"] + "\\projectdirectory", txtServiceHostDir.Text);

            //if (Directory.Exists(serviceHostDir))
            //{
            //    err = fileExists(serviceHostDir, @"bin\CodeEndeavors.ServiceHost.dll");
            //}
            //else
            //{
            if (!Directory.Exists(serviceHostDir) || fileExists(serviceHostDir, @"bin\CodeEndeavors.ServiceHost.dll").Length > 0)
            {
                if (MessageBox.Show("Directory is not a ServiceHost folder.  Do you wish to create one?", "Create Service Host", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    _replacementsDictionary["CreateServiceHost"] = "true";
                else
                    err = string.Format("Service Host Directory {0} does not exist.  Please select the location of your service host installation", serviceHostDir);
            }
                //DO YOU WISH TO CREATE IT?
            //}

            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err);
                return false;
            }
            return true;
        }


        private string fileExists(string dir, string fileName)
        {
            if (!File.Exists(Path.Combine(dir, fileName)))
                return string.Format("File does not exist ({0}).  Service Host directory invalid.", fileName);
            return "";
        }

        private string fileVersion(string dir, string fileName, Version version)
        {
            var a = Assembly.LoadFile(Path.Combine(dir, fileName));
            if (a.GetName().Version < version)
                return string.Format("Version of file {0} needs to be version {1} or later.  Version found {2}.", fileName, version, a.GetName().Version);
            return "";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtServiceHostDir.Text = makeRelative(folderBrowserDialog1.SelectedPath, _replacementsDictionary["$destinationdirectory$"] + "\\projectdirectory"); //destination is the root where sln is stored, we need one level deeper
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.codeendeavors.com");

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (validForm())
            {
                var serviceHostDir = txtServiceHostDir.Text;
                var serviceHostUrl = txtServiceHostUrl.Text;

                Properties.Settings.Default.ServiceHostDir = serviceHostDir;
                Properties.Settings.Default.ServiceHostUrl = serviceHostUrl;
                _replacementsDictionary["$servicehostdir$"] = serviceHostDir;
                _replacementsDictionary["$servicehosturl$"] = serviceHostUrl;

                Properties.Settings.Default.Save();
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }

        private string makeRelative(string filePath, string referencePath)
        {
            if (referencePath.EndsWith("\\") == false)
                referencePath += "\\";
            var fileUri = new Uri(filePath);
            var referenceUri = new Uri(referencePath);
            return referenceUri.MakeRelativeUri(fileUri).ToString().Replace('/', Path.DirectorySeparatorChar).Replace("%20", " ");
        }

        private void picLogo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.codeendeavors.com");
        }


    }
}
