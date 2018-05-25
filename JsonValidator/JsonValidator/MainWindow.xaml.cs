using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;
namespace JsonValidator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            var validator = new SchemaValidator();
            validator.jsonFullFileName = txtInputFile.Text;
            validator.SchemaPath = txtSchemaPath.Text;
            IEnumerable<string> errors = validator.Validate();


            Dispatcher.Invoke(() =>
            {
                if (errors != null && errors.Count() > 0)
                    foreach (var error in errors)
                        lstError.Items.Add(error);
                else
                    lstError.Items.Add("No Error");
            });


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnInputFilePicker_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Dispatcher.Invoke(() =>
                {
                    txtInputFile.Text = openFileDialog.FileName;
                });

        }

        private void btnSchemaFolderPicker_Click(object sender, RoutedEventArgs e)
        {
            var openFolderDialog = new FolderBrowserDialog();


            if (openFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Dispatcher.Invoke(() =>
                {
                    txtSchemaPath.Text = openFolderDialog.SelectedPath;
                });
        }
    }
}

