using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.VisualBasic.CompilerServices;

namespace PracticeTime.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        public const string EntryPropertyName = "Entry";
        private string entry;

        public string Entry
        {
            get { return entry; }

            set
            {
                if (entry == value)
                {
                    return;
                }

                entry = value;
                RaisePropertyChanged(EntryPropertyName);
                SaveEntryCommand.RaiseCanExecuteChanged();
                IsSaved = false;
            }
        }

        public const string EntrySliderPropertyName = "EntrySilder";
        private string entrySlider;
        public string EntrySlider
        {
            get { return entrySlider; }

            set
            {
                if (entrySlider == value)
                {
                    return;
                }

                entrySlider = value;
                RaisePropertyChanged(EntrySliderPropertyName);
            }
        }

        public const string IsSavedPropertyName = "IsSavedProperty";
        private bool isSaved = true;
        public bool IsSaved
        {
            get { return isSaved; }

            set
            {
                if (isSaved == value)
                {
                    return;
                }

                isSaved = value;
                RaisePropertyChanged(IsSavedPropertyName);
            }
        }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        /// 
        public MainViewModel()
        {
        }

        private RelayCommand _saveEntryRelayCommand;
        public RelayCommand SaveEntryCommand {
            get
            {
                if (_saveEntryRelayCommand == null)
                {
                    this._saveEntryRelayCommand = new RelayCommand(SaveEntryCommandExecute,
                        SaveEntryCommandCanExecute);
                }
                return this._saveEntryRelayCommand;
            }
        }

        private bool SaveEntryCommandCanExecute()
        {
            return !string.IsNullOrEmpty(Entry) && !IsSaved;
        }

        private void SaveEntryCommandExecute()
        {
            string entryToave = this.entry;
            string entrySliderToSave = this.entrySlider;
            IsSaved = true;

        }
    }
}