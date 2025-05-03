﻿using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;

using Dependencies.ClrPh;

namespace Dependencies
{
    /// <summary>
    /// DependencyImportList  Filterable ListView for displaying imports.
    /// </summary>
    public partial class DependencyImportList : DependencyCustomListView
    {
		public static readonly RoutedUICommand CopyValuesCommand = new RoutedUICommand();

		public DependencyImportList()
        {
            InitializeComponent();
        }

		public void SetImports(string ModuleFilepath, List<PeExport> Exports, List<PeImportDll> ParentImports, PhSymbolProvider SymPrv, DependencyWindow Dependencies)
		{
			this.Items.Clear();

			// Collect found and missing imports separately and combine them at the end.
			// This is to ensure that the missing imports are always at the top of the list.
			List<DisplayPeImport> missingImports = new List<DisplayPeImport>();
			List<DisplayPeImport> foundImports = new List<DisplayPeImport>();

			foreach (PeImportDll DllImport in ParentImports)
			{
				foreach (var Import in BinaryCache.LookupImports(DllImport, Exports))
				{
					if (Import.Item2) 
					{
						foundImports.Add(new DisplayPeImport(Import.Item1, SymPrv, ModuleFilepath, Import.Item2));
					}
					else
                    {
                        missingImports.Add(new DisplayPeImport(Import.Item1, SymPrv, ModuleFilepath, Import.Item2));
                    }
				}
			}
			
			// Add the missing imports first, then the found imports.
			foreach (var Import in missingImports)
            {
                this.Items.Add(Import);
            }
			foreach (var Import in foundImports)
            {
                this.Items.Add(Import);
            }
		}

        public void SetRootImports(List<PeImportDll> Imports, PhSymbolProvider SymPrv, DependencyWindow Dependencies)
        {
            this.Items.Clear();

            foreach (PeImportDll DllImport in Imports)
            {

                PE ModuleImport = Dependencies.LoadImport(DllImport.Name, null, DllImport.IsDelayLoad() );
                string ModuleFilepath = (ModuleImport != null) ? ModuleImport.Filepath : null;

                foreach( var Import in BinaryCache.LookupImports(DllImport, ModuleFilepath))
                {
                    this.Items.Add(new DisplayPeImport(Import.Item1, SymPrv, ModuleFilepath, Import.Item2));
                }
            }
        }

        private string ImportCopyHandler(object SelectedItem)
        {
            if (SelectedItem == null)
            {
                return "";
            }

            return (SelectedItem as DisplayPeImport).ToString();
        }

		private void ImportListCopySelectedValues(object sender, RoutedEventArgs e)
		{
			if (this.SelectedItems.Count == 0)
				return;

			List<DisplayPeImport> selectedImports = new List<DisplayPeImport>();
			foreach (var import in this.SelectedItems)
			{
				selectedImports.Add((import as DisplayPeImport));
			}

			string SelectedValues = String.Join("\n", selectedImports.Select( imp => imp.ToString()));

			Clipboard.Clear();
			// sometimes another process has "opened" the clipboard, so we need to wait for it
			try
			{
				Clipboard.SetText((string)SelectedValues, TextDataFormat.Text);
				return;
			}
			catch { }
			
		}

		public void ResetAutoSortProperty()
		{
			Wpf.Util.GridViewSort.RemoveSort(this.Items, this);
		}
	}
}
