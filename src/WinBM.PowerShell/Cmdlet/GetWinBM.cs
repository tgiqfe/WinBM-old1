﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.IO;

namespace WinBM.PowerShell.Cmdlet.Recipe
{
    [Cmdlet(VerbsCommon.Get, "WinBM")]
    public class GetWinBM : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0), Alias("File")]
        public string RecipeFile { get; set; }

        private string _currentDirectory = null;

        protected override void BeginProcessing()
        {
            _currentDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = this.SessionState.Path.CurrentFileSystemLocation.Path;
        }

        protected override void ProcessRecord()
        {
            List<WinBM.Recipe.Page> list = null;
            if (File.Exists(RecipeFile))
            {
                string[] candidate_db = { ".db", ".dat", ".recipe" };
                string[] candidate_yml = { ".yaml", ".yml" };
                string extension = System.IO.Path.GetExtension(RecipeFile);
                if (candidate_db.Any(x => x.Equals(extension, StringComparison.OrdinalIgnoreCase)))
                {
                    list = WinBM.Recipe.Page.Load(RecipeFile).ToList();
                }
                else if (candidate_yml.Any(x => x.Equals(extension, StringComparison.OrdinalIgnoreCase)))
                {
                    list = WinBM.Recipe.Page.Deserialize(RecipeFile);
                }
            }
            else if (Directory.Exists(RecipeFile))
            {
                foreach (string filePath in Directory.GetFiles(RecipeFile))
                {
                    string extension = Path.GetExtension(filePath).ToLower();
                    if (extension == ".yml" || extension == ".yaml")
                    {
                        list ??= new List<WinBM.Recipe.Page>();
                        list.AddRange(WinBM.Recipe.Page.Deserialize(filePath));
                    }
                }
            }

            WriteObject(list);
        }

        protected override void EndProcessing()
        {
            Environment.CurrentDirectory = _currentDirectory;
        }
    }
}
