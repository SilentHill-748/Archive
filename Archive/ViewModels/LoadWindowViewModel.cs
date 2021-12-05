using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forms = System.Windows.Forms;
using System.Windows;

using Archive.Core;
using Archive.Models;
using Archive.Logic.Services;
using Archive.Logic.Services.Interfaces;
using Archive.Data.Entities;
using Archive.Logic.Interfaces;
using System.IO;

namespace Archive.ViewModels
{
    public class LoadWindowViewModel : ObservableObject
    {
        private readonly LoadWindowModel _model;
        private readonly IDocumentBuilderService _builderService;
        private readonly IDbService _dbService;


        public LoadWindowViewModel(Window view)
        {
            _model = new LoadWindowModel();
            _builderService = ServiceFactory.GetService<DocumentBuilderService, IDocumentBuilderService>();
            _dbService = ServiceFactory.GetService<DbService, IDbService>();

            View = view;
        }


        #region Properties
        public LoadWindowModel Model => _model;

        public Window View { get; }
        #endregion

        #region Commands
        public RelayCommand LoadFilesCommand
        {
            get
            {
                return new RelayCommand(LoadFilesAsync, CanLoadFiles);
            }
        }

        public RelayCommand SelectPathCommand
        {
            get
            {
                return new RelayCommand(SelectPath);
            }
        }
        #endregion


        private async void LoadFilesAsync(object? commandParameter)
        {
            if (commandParameter is null)
                return;

            string configFile = commandParameter.ToString()! + "\\Конфиг.txt";

            Model.LoadedDocumentsCount = File.ReadAllLines(configFile).Length;

            try
            {
                IMapperService mapper = ServiceFactory.GetService<MapperService, IMapperService>();

                await Task.Run(() =>
                {
                    _builderService.Builded += BuilderService_Builded;

                    List<ITextDocument> documents = _builderService.Build(configFile);

                    _dbService.UnitOfWork
                        .Getrepository<Document>()
                        .Add(mapper.Map<Document, ReferenceDocument>(documents));

                    _ = _dbService.UnitOfWork.SaveChanges();
                }
                ).ConfigureAwait(true);

                View.DialogResult = false;
                CloseView();
            }
            catch (Exception ex)
            {
                //_logger.Log(ex)
            }
        }

        private void BuilderService_Builded(ITextDocument obj)
        {
            Model.LoadingDocumentsCount++;
        }

        private bool CanLoadFiles(object? commandParameter)
        {
            return commandParameter is not null && !string.IsNullOrWhiteSpace(commandParameter.ToString());
        }

        private void SelectPath(object? commandParameter)
        {
            using Forms.FolderBrowserDialog fbd = new();

            if (fbd.ShowDialog() == Forms.DialogResult.OK)
                Model.SelectedPath = fbd.SelectedPath;
        }

        private void CloseView()
        {
            View.Owner.Focus();
            View.Dispatcher.Invoke(View.Close);
        }
    }
}
