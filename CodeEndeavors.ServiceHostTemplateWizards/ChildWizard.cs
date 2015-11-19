using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateBuilder;

namespace CodeEndeavors.ServiceHostTemplateWizards
{
    public class ChildWizard : IWizard
    {
        public void BeforeOpeningFile(EnvDTE.ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(EnvDTE.Project project)
        {
        }

        public void ProjectItemFinishedGenerating(EnvDTE.ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            foreach (var key in RootWizard.GlobalDictionary.Keys)
                replacementsDictionary[key] = RootWizard.GlobalDictionary[key];
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
