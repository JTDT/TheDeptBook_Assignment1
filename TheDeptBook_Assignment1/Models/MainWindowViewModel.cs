using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using TheDeptBook_Assignment1.DTO;

namespace TheDeptBook_Assignment1.Models
{
    public class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<Debtor> _debtors;
        private string filePath = "";

        public MainWindowViewModel()
        {
            _debtors = new ObservableCollection<Debtor>();
            _debtors.Add(new Debtor("Alice", 100));
            _debtors.Add(new Debtor("Bob", 200));
            _debtors.Add(new Debtor("Carol", 50.50));
            _debtors.Add(new Debtor("Dan", -1024));

            CurrentDebtor = null;

        }

        #region Properties

        public ObservableCollection<Debtor> Debtors
        {
            get { return _debtors; }
            set { SetProperty(ref _debtors, value); }
        }

        private Debtor _currentDebtor;

        public Debtor CurrentDebtor
        {
            get { return _currentDebtor; }
            set { SetProperty(ref _currentDebtor, value); }
        }

        private int currentIndex = -1;

        public int CurrentIndex
        {
            get
            {
                return currentIndex;
            }
            set { SetProperty(ref currentIndex, value); }
        }


        private string _filename = "DebtorAssignment1";

        public string Filename
        {
            get { return _filename; }
            set
            {
                SetProperty(ref _filename, value);
                RaisePropertyChanged("File");
            }
        }

        private bool dirty = false;
        public bool Dirty
        {
            get { return dirty; }
            set
            {
                SetProperty(ref dirty, value);
                RaisePropertyChanged("Title");
            }
        }
        #endregion

        #region Commands

        public ICommand _addNewDeptor;

        public ICommand AddNewDeptor
        {
            get
            {
                return _addNewDeptor ?? (_addNewDeptor = new DelegateCommand(() =>
                {
                    var vm = new DebtorViewModel("Add new debtor", _debtors);
                    var dialog = new AddDebtor
                    {
                        DataContext = vm,
                        Owner = App.Current.MainWindow
                    };
                    if (dialog.ShowDialog() == true)
                    {
                        Dirty = true;
                    }
                }));
            }
        }

        private ICommand _editDebtor;

        public ICommand EditDebtorCommand
        {
            get
            {
                return _editDebtor ?? (_editDebtor = new DelegateCommand(() =>
                           {
                               var tempDebtor = CurrentDebtor.Clone();
                               var vm = new DebtorDetailsWindowViewModel("Debtors debt", CurrentDebtor)
                               {
                               };
                               var dialog = new DebtorDetailsWindow
                               {
                                   DataContext = vm,
                                   Owner = App.Current.MainWindow
                               };
                               if (dialog.ShowDialog() == true)
                               {
                                   Dirty = true;
                               }
                           },
                () =>
                {
                    return CurrentIndex >= 0;
                }
            ).ObservesProperty(() => CurrentIndex));
            }
        }

        private ICommand _saveAsCommand;

        public ICommand SaveAsCommand
        {
            get { return _saveAsCommand ?? (_saveAsCommand = new DelegateCommand<string>(SaveAsCommandExecute)); }
        }

        private void SaveAsCommandExecute(string argFileName)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Agent assignment documents|*.agn|All Files|*.*",
                DefaultExt = "agn"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                SaveFile();
            }
        }

        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand =
                           new DelegateCommand(SaveFileCommandExecute, SaveFileCommandCanExecute)
                               .ObservesProperty(() => Debtors.Count));
            }
        }

        private bool SaveFileCommandCanExecute()
        {
            return (Filename != "") && (Debtors.Count > 0);
        }

        private void SaveFileCommandExecute()
        {
            SaveFile();
        }

        private void SaveFile()
        {
            try
            {
                DataFile.SaveFile(filePath, _debtors);
                Dirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to save file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        ICommand _OpenFileCommand;
        public ICommand OpenCommand
        {
            get { return _OpenFileCommand ?? (_OpenFileCommand = new DelegateCommand<string>(OpenFileCommand_Execute)); }
        }

        private void OpenFileCommand_Execute(string argFilename)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Debtor assignment documents|*.agn|All Files|*.*",
                DefaultExt = "debt"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                try
                {
                    DataFile.ReadFile(filePath, out ObservableCollection<Debtor> tempAgents);
                    Debtors = tempAgents;
                    Dirty = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private ICommand _ColorCommand;

        public ICommand ColorCommand
        {
            get { return _ColorCommand ?? (_ColorCommand = new DelegateCommand<string>(ColorCommandExecute)); }
        }

        private void ColorCommandExecute(string colorString)
        {
            SolidColorBrush myBrush = SystemColors.WindowBrush;
            try
            {
                if (colorString != "Default")
                {
                    myBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorString));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown color name!");
            }

            Application.Current.Resources["myBrush"] = myBrush;
        }

        #endregion

    }
}
