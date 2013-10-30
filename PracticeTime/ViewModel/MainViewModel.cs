using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

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



        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        /// 
        public MainViewModel()
        {

        }

        public RelayCommand SaveEntryCommand {
            get { return new RelayCommand(SaveEntryCommandExecute, () => true); }
        }

        private void SaveEntryCommandExecute()
        {
            string entryToave = this.entry;
            string entrySliderToSave = this.entrySlider;

        }
    }
}