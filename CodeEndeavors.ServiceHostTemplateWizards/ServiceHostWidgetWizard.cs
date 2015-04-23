using System;
using System.Collections.Generic;
using System.IO;
using EnvDTE;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.VisualStudio.TemplateWizard;
using System.Reflection;
using TemplateBuilder;

namespace CodeEndeavors.ServiceHostTemplateWizards
{
    public class ServiceHostWidgetWizard : Microsoft.VisualStudio.TemplateWizard.IWizard
    {
        //public static Dictionary<string, string> GlobalDictionary = new Dictionary<string,string>();  //SideWaffle

        private Dictionary<string, string> _replacementsDictionary = null;

        public void ProjectFinishedGenerating(Project project)
        {
            var destPath = _replacementsDictionary["$destinationdirectory$"];  //$destinationdirectory$

            CopyFiles(destPath);

        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, Microsoft.VisualStudio.TemplateWizard.WizardRunKind runKind, object[] customParams)
        {
            _replacementsDictionary = replacementsDictionary;
            RootWizard.GlobalDictionary["$saferootprojectname$"] = replacementsDictionary["$safeprojectname$"]; //need to set this so Child projects have access
            //_replacementsDictionary.Add("$saferootprojectname$", RootWizard.GlobalDictionary["$saferootprojectname$"]);

        }

        public void RunFinished() { }
        public void BeforeOpeningFile(ProjectItem projectItem) { }
        // This method is only called for item templates, not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem projectItem) { }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        private void CopyFiles(string destPath)
        {
            var libDir = Path.Combine(destPath, "lib");
            var serviceHostDir = Path.Combine(destPath, @"..\..\..\ServiceHost");
            var sourceLibDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ProjectTemplates\ServiceHost\lib");
            var sourceServiceHostDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ProjectTemplates\ServiceHost\ServiceHost");

            copyDirectory(sourceLibDir, libDir);
            copyDirectory(sourceServiceHostDir, serviceHostDir);
        }

        private void SafeCopy(string from, string to, bool bypassError = false)
        {
            try
            {
                File.Copy(from, to, true);
            }
            catch (Exception ex)
            {
                //if (bypassError == false)
                    MessageBox.Show(ex.Message);    //keep going!
            }
        }

        private void copyDirectory(string sourceFolder, string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);

            var dirInfo = new DirectoryInfo(sourceFolder);
            var files = dirInfo.GetFiles();
            foreach (var tempfile in files)
            {
                if (!File.Exists(Path.Combine(destinationFolder, tempfile.Name)))
                    tempfile.CopyTo(Path.Combine(destinationFolder, tempfile.Name), false);
            }

            var directories = dirInfo.GetDirectories();
            foreach (var tempdir in directories)
                copyDirectory(Path.Combine(sourceFolder, tempdir.Name), Path.Combine(destinationFolder, tempdir.Name));

        }

    }
}
