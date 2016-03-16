using System;
using System.Collections.Generic;
using System.IO;
using EnvDTE;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.VisualStudio.TemplateWizard;
using System.Linq;
using EnvDTE100;
using System.Reflection;

namespace CodeEndeavors.ServiceHostTemplateWizards
{
    public class ServiceHostItemWizard : Microsoft.VisualStudio.TemplateWizard.IWizard
    {
        private Dictionary<string, string> _replacementsDictionary = null;
        //private IWizard wizard;

        public void ProjectFinishedGenerating(Project project)
        {

        }

        static string[] _projectPropertiesToAdd = new string[] { "defaultnamespace" };

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, Microsoft.VisualStudio.TemplateWizard.WizardRunKind runKind, object[] customParams)
        {
            replacementsDictionary["$destinationdirectory$"] = replacementsDictionary["$solutiondirectory$"]; //make it same as project...
            replacementsDictionary["$lowercasesafeitemname$"] = replacementsDictionary["$safeitemname$"].ToLower();
            var safeItemName = replacementsDictionary["$safeitemname$"];
            if (safeItemName.Contains("Map"))
            {
                replacementsDictionary["$nomapitemname$"] = safeItemName.Replace("Map", "").Replace("Ext", "");
            }
            if (safeItemName.Contains("Ext"))
            {
                replacementsDictionary["$safeextensionname$"] = safeItemName;
                replacementsDictionary["$originalsafeitemname$"] = safeItemName.Replace("Ext", "");
            }
            else
            {
                replacementsDictionary["$safeextensionname$"] = safeItemName + "Ext";
                replacementsDictionary["$originalsafeitemname$"] = safeItemName;
            }
            _replacementsDictionary = replacementsDictionary;
            //if (NewWidgetItemForm.ShowDialog(replacementsDictionary, automationObject as DTE) == false)
            //    throw new WizardCancelledException("The wizard has been cancelled by the user.");
            //Assembly asm = Assembly.Load("NuGet.VisualStudio.Interop, Version=1.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a");
            //wizard = (IWizard)asm.CreateInstance("NuGet.VisualStudio.TemplateWizard");
            //wizard.RunStarted(automationObject, replacementsDictionary, runKind, customParams);
            try
            {
                _DTE dte = automationObject as _DTE;
                Solution4 solution = (Solution4)dte.Solution;
                Project project = (Project)((object[])dte.ActiveSolutionProjects)[0];
                foreach (Property prop in project.Properties)
                {
                    if (_projectPropertiesToAdd.Contains(prop.Name.ToLower()))
                        replacementsDictionary.Add("$" + prop.Name.ToLower() + "$", (prop.Value == null ? string.Empty : prop.Value.ToString()));
                }

                replacementsDictionary["$solutionname$"] = solution.Properties.Item("Name").Value.ToString();

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
