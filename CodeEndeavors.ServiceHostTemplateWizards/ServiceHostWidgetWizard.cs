using System;
using System.Collections.Generic;
using System.IO;
using EnvDTE;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.VisualStudio.TemplateWizard;
using System.Reflection;
using TemplateBuilder;
using EnvDTE100;
using EnvDTE80;
using System.Threading;
using System.Linq;
using Microsoft.Web.Administration;
using System.Security.AccessControl;

namespace CodeEndeavors.ServiceHostTemplateWizards
{
    public class ServiceHostWidgetWizard : Microsoft.VisualStudio.TemplateWizard.IWizard
    {
        private DTE2 _dte2;
        private Solution4 _solution;
        private IList<Project> _existingProjects;

        //public static Dictionary<string, string> GlobalDictionary = new Dictionary<string,string>();  //SideWaffle

        private Dictionary<string, string> _replacementsDictionary = null;

        public void ProjectFinishedGenerating(Project project)
        {
            var destPath = _replacementsDictionary["$destinationdirectory$"];  //$destinationdirectory$

            copyServiceHostFiles(destPath);
            copyNuspec(destPath);

        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, Microsoft.VisualStudio.TemplateWizard.WizardRunKind runKind, object[] customParams)
        {
            _replacementsDictionary = replacementsDictionary;

            if (NewServiceHostForm.ShowDialog(replacementsDictionary) == false)
                throw new WizardCancelledException("The wizard has been cancelled by the user.");

            RootWizard.GlobalDictionary["$saferootprojectname$"] = replacementsDictionary["$safeprojectname$"]; //need to set this so Child projects have access
            RootWizard.GlobalDictionary["$servicehostdir$"] = replacementsDictionary["$servicehostdir$"]; //need to set this so Child projects have access
            RootWizard.GlobalDictionary["$servicehosturl$"] = replacementsDictionary["$servicehosturl$"]; //need to set this so Child projects have access

            //_replacementsDictionary.Add("$saferootprojectname$", RootWizard.GlobalDictionary["$saferootprojectname$"]);

            _dte2 = automationObject as DTE2;
            if (_dte2 != null) _solution = (Solution4)_dte2.Solution;
            
            _existingProjects = getProjects() ?? new Project[0];
        }

        //https://github.com/ligershark/template-builder/blob/6184bec12552ed32bfc573a05a01bb9f57514b97/src/TemplateBuilder/SolutionWizard.cs
        //https://github.com/ligershark/side-waffle/issues/169
        public void RunFinished() 
        {
            if (_solution == null)
            {
                throw new Exception("No solution found.");
            }

            //Get all projects in solution
            var projects = getProjects().Except(_existingProjects).ToList();
            if (projects == null || !projects.Any()) throw new Exception("No projects found.");

            //projects = projects.OrderByDescending(p => p.Name).ToList();    //don't copy the one that matches the same name until END... LOSE ENTIRE FOLDER STRUCTURE THIS WAY

            //Get the projects directory from first project.
            var originalProjectsDir = Path.GetDirectoryName(Path.GetDirectoryName(projects.First().FullName));
            if (originalProjectsDir == null) return;
            var tempProjectsDir = originalProjectsDir + ".temp"; //name.temp and name dirs 

            var solutionDir = new DirectoryInfo(originalProjectsDir).Parent.FullName;
            //var solutionStructure = projects.Select(p => new KeyValuePair<Project, string>(null, Path.Combine(solutionDir, Path.GetFileNameWithoutExtension(Path.GetFileName(p.FullName))))).ToList();
            var newSolutionProjects = projects.Select(p => Path.Combine(Path.Combine(solutionDir, Path.GetFileNameWithoutExtension(Path.GetFileName(p.FullName))), Path.GetFileName(p.FullName))).ToList();
            
            var slnFileName = Path.Combine(solutionDir, new DirectoryInfo(originalProjectsDir).Name + ".sln");
            _solution.SaveAs(slnFileName);

            //get project references
            var projectReferences = new Dictionary<string, List<string>>();
            foreach (var project in projects)
            {
                projectReferences[project.Name] = new List<string>();

                var vsProj = project.Object as VSLangProj.VSProject;
                foreach (VSLangProj.Reference reference in vsProj.References)
                {
                    if (reference.SourceProject != null)
                        projectReferences[project.Name].Add(reference.SourceProject.Name);
                }
            }

            //Remove the projects from the solution
            foreach (var project in projects)
            {
                _solution.Remove(project);
                _solution.SaveAs(slnFileName);
            }

            //move the project dir to temp dir (we have same name between our projects dir and the service dir)
            Directory.Move(originalProjectsDir, tempProjectsDir);
            copyDirectory(tempProjectsDir, solutionDir);    //move all contents of directory into solution dir

            //Restructure solution
            foreach (var projectFile in newSolutionProjects)
            {
                _solution.AddFromFile(projectFile, false);
                _solution.SaveAs(slnFileName);
            }

            //add project references back
            var newProjects = getProjects();
            var projectDict = newProjects.ToDictionary(p => p.Name);
            foreach (var project in newProjects)
            {
                var vsProj = project.Object as VSLangProj.VSProject;
                foreach (var projectName in projectReferences[project.Name])
                    vsProj.References.AddProject(projectDict[projectName]);
            }

            //add build dependencies for Test project
            var testProject = newProjects.Where(p => p.Name.EndsWith(".Test")).FirstOrDefault();
            foreach (var project in newProjects.Where(p => p.Name != testProject.Name))
                _solution.SolutionBuild.BuildDependencies.Item(testProject.UniqueName).AddProject(project.UniqueName);

            _solution.Properties.Item("StartupProject").Value = testProject.Name;    //set startup project

            Directory.Delete(tempProjectsDir, true);  //remove 

            //open help for servicehost
            var url = RootWizard.GlobalDictionary["$servicehosturl$"];
            if (!url.EndsWith("/"))
                url += "/";
            _dte2.ItemOperations.Navigate(url + "version", vsNavigateOptions.vsNavigateOptionsNewWindow);

            //ThreadPool.QueueUserWorkItem(state =>
            //{
            //    //Wait for *.sln file and obsolete folder to be created.
            //    System.Threading.Thread.Sleep(4000);

            //    //Delete the old directory
            //    DeleteDirectory(tempProjectsDir);
            //});        


        }

        public void BeforeOpeningFile(ProjectItem projectItem) { }
        // This method is only called for item templates, not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem projectItem) { }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        private void copyNuspec(string destPath)
        {
            var redistDir = Path.Combine(destPath, "redist");
            var sourceRedistDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ProjectTemplates\ServiceHost\redist");
            copyDirectory(sourceRedistDir, redistDir);
        }


        private void copyServiceHostFiles(string destPath)
        {
            var libDir = Path.Combine(destPath, "lib");
            var serviceHostDir = Path.Combine(_replacementsDictionary["$destinationdirectory$"], RootWizard.GlobalDictionary["$servicehostdir$"]); //Path.Combine(destPath, @"..\ServiceHost");
            //MessageBox.Show(serviceHostDir);
            var sourceLibDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ProjectTemplates\ServiceHost\lib");
            var sourceServiceHostDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ProjectTemplates\ServiceHost\ServiceHost");

            copyDirectory(sourceLibDir, libDir);

            if (_replacementsDictionary.ContainsKey("CreateServiceHost") && _replacementsDictionary["CreateServiceHost"] == "true")
            {
                copyDirectory(sourceServiceHostDir, serviceHostDir);

                var url = RootWizard.GlobalDictionary["$servicehosturl$"];
                var uri = new Uri(url);
                var name = uri.Host;
                var resolvedServiceHostDir = new DirectoryInfo(serviceHostDir).FullName;
                createSite(resolvedServiceHostDir, name, name, name);

                if (PSHostsFile.HostsFile.Get().Where(e => e.Hostname.Equals(name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() == null)
                {
                    if (MessageBox.Show("Do you wish to add a HOSTS file entry for " + name + "?", "Add Hosts Entry", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        PSHostsFile.HostsFile.Set(name, "127.0.0.1");
                }

                setFolderPermissions(serviceHostDir, @"iis apppool\" + name);
            }
        }

        private static void setFolderPermissions(string dirPath, string user)
        {
            var dir = new DirectoryInfo(dirPath);
            var ds = dir.GetAccessControl();
            ds.AddAccessRule(new FileSystemAccessRule(user,
            FileSystemRights.FullControl,
            InheritanceFlags.ObjectInherit |
            InheritanceFlags.ContainerInherit,
            PropagationFlags.None,
            AccessControlType.Allow));
            dir.SetAccessControl(ds);
        }


        private void safeCopy(string from, string to, bool bypassError = false)
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

        private IList<Project> getProjects()
        {
            var projects = _solution.Projects;
            var list = new List<Project>();
            var item = projects.GetEnumerator();

            while (item.MoveNext())
            {
                var project = item.Current as Project;
                if (project == null)
                    continue;

                if (project.Kind == ProjectKinds.vsProjectKindSolutionFolder)
                    list.AddRange(getSolutionFolderProjects(project));
                else
                    list.Add(project);
            }
            return list;
        }

        private static IEnumerable<Project> getSolutionFolderProjects(Project solutionFolder)
        {
            var list = new List<Project>();
            for (var i = 1; i <= solutionFolder.ProjectItems.Count; i++)
            {
                var subProject = solutionFolder.ProjectItems.Item(i).SubProject;
                if (subProject == null)
                    continue;

                // If this is another solution folder, do a recursive call, otherwise add
                if (subProject.Kind == EnvDTE80.ProjectKinds.vsProjectKindSolutionFolder)
                    list.AddRange(getSolutionFolderProjects(subProject));
                else
                    list.Add(subProject);
            }
            return list;
        }

       
        private static void createSite(string directoryPath, string siteName, string host, string poolName)
        {
            using (var serverManager = new ServerManager())
            {
                if (serverManager.ApplicationPools[poolName] == null)
                {
                    var newPool = serverManager.ApplicationPools.Add(poolName);
                    newPool.ManagedRuntimeVersion = "v4.0";
                    serverManager.CommitChanges();
                }

                if (serverManager.Sites[siteName] == null)
                {
                    //bindingInformation = Address:Port:Host
                    var bindingInformation = string.Format("{0}:{1}:{2}", "*", "80", host);
                    var newSite = serverManager.Sites.Add(siteName, "http", bindingInformation, directoryPath);
                    newSite.ApplicationDefaults.ApplicationPoolName = poolName;
                    
                    //var app = newSite.Applications.Add("/" + applicationName, directoryPath);
                    //app.ApplicationPoolName = poolName;
                    serverManager.CommitChanges();
                }
            }
        }

    }
}
