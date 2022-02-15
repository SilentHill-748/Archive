using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

using Microsoft.Win32;

using Archive.Views;
using Archive.Core;
using Archive.Models;
using Archive.Logic.Services;
using Archive.Logic.Services.Interfaces;
using Archive.Data.Entities;

namespace Archive.ViewModels
{
    public class LoadWindowViewModel : ObservableObject
    {
        private readonly LoadWindowModel _model;
        private readonly IDocumentBuilderService<Document> _builderService;


        public LoadWindowViewModel()
        {
            _model = new LoadWindowModel();
            _builderService = ServiceFactory.GetService<IDocumentBuilderService<Document>>();

            _loadFilesCommand = new RelayCommand(LoadFilesAsync, CanLoadFiles);
            _selectPathCommand = new RelayCommand(SelectPath);
            _closeCommand = new RelayCommand(CloseView);
        }


        #region Properties
        public LoadWindowModel Model => _model;
        #endregion

        #region Commands
        private readonly RelayCommand _loadFilesCommand;
        public RelayCommand LoadFilesCommand
        {
            get => _loadFilesCommand;
        }

        private readonly RelayCommand _selectPathCommand;
        public RelayCommand SelectPathCommand
        {
            get => _selectPathCommand;
        }

        private readonly RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get => _closeCommand;
        }
        #endregion


        private async void LoadFilesAsync(object? commandParameter)
        {
            Model.LoadingDocumentsCount = File.ReadAllLines(Model.ConfigurationFilePath).Length;

            try
            {
                await Task.Run(() =>
                {
                    try
                    {
                        if (App.DbService.UnitOfWork.DbContext.Set<Document>().Any())
                            return;

                        _builderService.Builded += BuilderService_Builded;

                        List<Document> documents = _builderService.Build(Model.ConfigurationFilePath);

                        App.DbService.UnitOfWork.GetRepository<Document>().Add(documents);
                        _ = App.DbService.UnitOfWork.SaveChanges();
                    }
                    catch
                    {
                        throw;
                    }
                });

                GC.Collect();
                CloseView(commandParameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Возникла ошибка с текстом:\n" + ex.Message, 
                    "Ошибка", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);

                CloseView(commandParameter);

                throw;
            }
        }

        private void BuilderService_Builded(Document obj)
        {
            Model.LoadedDocumentsCount++;
        }

        private bool CanLoadFiles(object? commandParameter)
        {
            return !string.IsNullOrWhiteSpace(Model.ConfigurationFilePath);
        }

        private void SelectPath(object? commandParameter)
        {
            OpenFileDialog ofd = new();
            ofd.Filter = "Text files .txt | *.txt";

            if (ofd.ShowDialog() == true)
                Model.ConfigurationFilePath = ofd.FileName;
        }

        private void CloseView(object? commandParameter)
        {
            if (commandParameter is not null && 
                commandParameter is LoadWindow view)
            {
                view.DialogResult = true;
                view.Close();
            }
        }
    }
}
