using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227
using Microsoft.Practices.ServiceLocation;
using PracticeTime.Common.Models;
using PracticeTime.ViewModel;

namespace PracticeTime
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private static string OUTPUTFILE = "PracticeTimeData.xml";
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                    LoadData();
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            SaveDataAsync();
            deferral.Complete();
        }

        public async void LoadData()
        {
            MainViewModel mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();

            IReadOnlyList<StorageFile> files = await ApplicationData.Current.LocalFolder.GetFilesAsync();

            StorageFile sessionFile;
            if (files.All(f => f.Name != OUTPUTFILE))
            {
                await ApplicationData.Current.LocalFolder.CreateFileAsync(OUTPUTFILE);
            }


            sessionFile = await ApplicationData.Current.LocalFolder.GetFileAsync(OUTPUTFILE);
            IRandomAccessStream sessionRandomAccess = await sessionFile.OpenAsync(FileAccessMode.Read);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<EventRecord>));

            try
            {
                var result =
                    serializer.Deserialize(sessionRandomAccess.AsStreamForRead()) as ObservableCollection<EventRecord>;
                sessionRandomAccess.Dispose();
                foreach (EventRecord data in result)
                {
                    mainViewModel.AddEventRecord(data);
                }
            }
            catch (Exception)
            {
                // unable to deserailize data so don't do anything
            }
        }

        public async void SaveDataAsync()
        {
            MainViewModel mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();

            StorageFile sessionFile = ApplicationData.Current.LocalFolder.CreateFileAsync(OUTPUTFILE, CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();
            IRandomAccessStream sessionRandomAccess = sessionFile.OpenAsync(FileAccessMode.ReadWrite).GetAwaiter().GetResult();
            IOutputStream sessionOutputStream = sessionRandomAccess.GetOutputStreamAt(0);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<EventRecord>));
            serializer.Serialize(sessionOutputStream.AsStreamForWrite(), mainViewModel.EventRecordData);
            sessionRandomAccess.Dispose();
            await sessionOutputStream.FlushAsync();

            sessionOutputStream.Dispose();
        }

    }
}
