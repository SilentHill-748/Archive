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
        private readonly IDbService _dbService;


        public LoadWindowViewModel()
        {
            _model = new LoadWindowModel();
            _builderService = ServiceFactory.GetService<IDocumentBuilderService<Document>>();
            _dbService = ServiceFactory.GetService<IDbService>();
        }


        #region Properties
        public LoadWindowModel Model => _model;
        #endregion

        #region Commands
        private readonly RelayCommand _loadFilesCommand;
        public RelayCommand LoadFilesCommand
        {
            get => _loadFilesCommand ?? new RelayCommand(LoadFilesAsync, CanLoadFiles);
        }

        private readonly RelayCommand _selectPathCommand;
        public RelayCommand SelectPathCommand
        {
            get => _selectPathCommand ?? new RelayCommand(SelectPath, o => true);
        }

        private readonly RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get => _closeCommand ?? new RelayCommand(CloseView);
        }
        #endregion


        private async void LoadFilesAsync(object? commandParameter)
        {
            Model.LoadingDocumentsCount = File.ReadAllLines(Model.ConfigurationFilePath).Length;

            try
            {
                await Task.Run(() =>
                {
                    if (_dbService.UnitOfWork.DbContext.Set<Document>().Any())
                        return;

                    _builderService.Builded += BuilderService_Builded;

                    List<Document> documents = _builderService.Build(Model.ConfigurationFilePath);

                    _dbService.UnitOfWork.GetRepository<Document>().Add(documents);
                    _ = _dbService.UnitOfWork.SaveChanges();
                }
                ).ConfigureAwait(true);

                GC.Collect();
                CloseView(commandParameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);

                //_logger.Log(ex)
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
