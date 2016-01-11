using System;
using System.Collections.Generic;
using System.IO;
using EnvDTE;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.VisualStudio.TemplateWizard;
using System.Linq;

namespace CodeEndeavors.ServiceHostTemplateWizards
{
    public class ServiceHostItemWizard : Microsoft.VisualStudio.TemplateWizard.IWizard
    {
        private Dictionary<string, string> _replacementsDictionary = null;

        public void ProjectFinishedGenerating(Project project)
        {

        }

        static string[] _projectPropertiesToAdd = new string[] { "defaultnamespace" };

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, Microsoft.VisualStudio.TemplateWizard.WizardRunKind runKind, object[] customParams)
        {
            replacementsDictionary["$destinationdirectory$"] = replacementsDictionary["$solutiondirectory$"]; //make it same as project...

            _replacementsDictionary = replacementsDictionary;
            //if (NewWidgetItemForm.ShowDialog(replacementsDictionary, automationObject as DTE) == false)
            //    throw new WizardCancelledException("The wizard has been cancelled by the user.");

            try
            {
                _DTE dte = automationObject as _DTE;
                Project project = (Project)((object[])dte.ActiveSolutionProjects)[0];
                foreach (Property prop in project.Properties)
                {
                    if (_projectPropertiesToAdd.Contains(prop.Name.ToLower()))
                        replacementsDictionary.Add("$" + prop.Name.ToLower() + "$", (prop.Value == null ? string.Empty : prop.Value.ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void RunFinished() { }
        public void BeforeOpeningFile(ProjectItem projectItem) { }
        // This method is only called for item templates, not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            //var renamedProjectItems = new Dictionary<string, ProjectItem>();
            //renameProjectItem(projectItem, renamedProjectItems);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        private void renameProjectItem(ProjectItem item, Dictionary<string, ProjectItem> renamedProjectItems)
        {
            var filePath = item.Properties.Item("FullPath").Value;
            var newPath = getReplacementString(filePath);
            if (filePath != newPath)
                renamedProjectItems[newPath] = item;
        }

        private string getReplacementString(string val)
        {
            var newVal = val;
            foreach (var key in _replacementsDictionary.Keys)
                newVal = newVal.Replace(key, _replacementsDictionary[key]);
            return newVal;
        }

    }
}
