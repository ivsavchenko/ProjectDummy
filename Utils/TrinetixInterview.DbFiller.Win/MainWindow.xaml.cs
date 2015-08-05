using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using Microsoft.Practices.Unity;
using TrinetixInterview.Contracts;
using TrinetixInterview.DbFiller.Services;
using MessageBox = System.Windows.MessageBox;

namespace TrinetixInterview.DbFiller.Win
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker worker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
            worker.RunWorkerCompleted += this.WorkerRunWorkerCompleted;
            worker.DoWork += this.WorkerDoWork;
        }

        private void PickFolderClick(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtFolderFullName.Text = dialog.SelectedPath;
            }
        }

        private void FillDbClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFolderFullName.Text.Trim()))
            {
                MessageBox.Show("Please select folder");
                return;
            }            

            btnFillDb.IsEnabled = false;
            btnFillDb.Content = "Please wait";

            worker.RunWorkerAsync();
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            Dispatcher.BeginInvoke(
                DispatcherPriority.Input,
                new Action(
                    () =>
                        {
                            try
                            {
                                IUnnormalizedLocationProcessor processor =
                                    CompositionRoot.Container.Resolve<IUnnormalizedLocationProcessor>();
                                IIUnnormalizedLocationPersistor persistor =
                                    CompositionRoot.Container.Resolve<IIUnnormalizedLocationPersistor>();

                                processor.PopulateUnnormalizedData(txtFolderFullName.Text);
                                persistor.Persist(processor.Data);

                                MessageBox.Show("Database were filled with data");
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show(error.Message);                                
                            }
                        }));
        }

        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnFillDb.IsEnabled = true;
            btnFillDb.Content = "Fill DB";
        }
    }
}
