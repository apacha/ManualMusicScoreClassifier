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

namespace WpfApp1
{
    using System.IO;
    using System.Reflection;

    using Microsoft.Win32;

    using Newtonsoft.Json;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Classification> _classifications;

        private Classification _currentClassification;

        public MainWindow()
        {
            InitializeComponent();
            _classifications = new List<Classification>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var pathToFiles = @"C:\Users\Alex\Repositories\MusicScoreClassifier\ModelGenerator\data\test_set_resized_256";
            var files = Directory.EnumerateFiles(pathToFiles).ToList();
            files.Shuffle();

            foreach (var file in files)
            {
                var expectedType = new FileInfo(file).Name.StartsWith("score") ? Category.Scores : Category.Other;
                var classification = new Classification(file, expectedType);
                _classifications.Add(classification);
            }

            LoadNextImage();
        }

        private void LoadNextImage()
        {
            var i = 0;
            foreach (var classification in _classifications)
            {
                i++;
                if (classification.UserClassification == Category.Unclassified)
                {
                    image.Source = new BitmapImage(new Uri(classification.FilePath));
                    _currentClassification = classification;
                    labelProgress.Content = $"{i}/{_classifications.Count}";
                    return;
                }
            }

            image.Source = null;
            MessageBox.Show(this, "No more images to classify. \nThank you for Participation :-)");
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKeyUp(e);
        }

        private void ProcessKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift)
            {
                ClassifyAsScores();
            }
            else if (e.Key == Key.RightShift)
            {
                ClassifyAsOther();
            }
            else if (e.Key == Key.Back)
            {
                Undo();
            }
        }

        private void ClassifyAsOther()
        {
            _currentClassification.UserClassification = Category.Other;
            LoadNextImage();
        }

        private void ClassifyAsScores()
        {
            _currentClassification.UserClassification = Category.Scores;
            LoadNextImage();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var json = JsonConvert.SerializeObject(_classifications);
            var resultFile = $"results{DateTime.Now.ToString("hh-mm-ss")}.json";
            File.WriteAllText(resultFile, json);
            MessageBox.Show(this, $"File {resultFile} saved.");
                
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ClassifyAsScores();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ClassifyAsOther();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (dialog.ShowDialog(this).Value)
                {
                    var json = File.ReadAllText(dialog.FileName);
                    _classifications = JsonConvert.DeserializeObject<List<Classification>>(json);
                    LoadNextImage();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while loading json");
            }
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Undo();
        }

        private void Undo()
        {
            for (int i = 0; i < _classifications.Count; i++)
            {
                if (_classifications[i].UserClassification == Category.Unclassified && i > 0)
                {
                    _classifications[i - 1].UserClassification = Category.Unclassified;
                    LoadNextImage();
                    return;
                }
            }
        }

        private void statisticsButton_Click(object sender, RoutedEventArgs e)
        {
            var totalClassifications = _classifications.Where(c => c.UserClassification != Category.Unclassified).Count();
            var wrongClassifications = _classifications.Where(c => c.UserClassification != Category.Unclassified && c.UserClassification != c.ExpectedClassification);
            var wrongClassificationsCount = wrongClassifications.Count();
            var correctClassifications = _classifications.Where(c => c.UserClassification != Category.Unclassified && c.UserClassification == c.ExpectedClassification).Count();

            string message = $"Total classifications: {totalClassifications}\n" + 
                $"Correct classifications: {correctClassifications}\n" + 
                $"Wrong classifications: {wrongClassificationsCount}\n" + 
                $"Accuracy: {correctClassifications * 100.0 / totalClassifications}%\n\n" + 
                $"Incorrect files: {string.Join(" ", wrongClassifications.Select(c => Path.GetFileName(c.FilePath)))}" 
                
                ;
            MessageBox.Show(this, message);
        }
    }

    internal enum Category
    {
        Unclassified,

        Scores,

        Other,

    }

    internal class Classification
    {
        public string FilePath { get; set;  }

        public Category ExpectedClassification { get; set; }

        public Category UserClassification { get; set; }

        public Classification(string file, Category expectedClassification)
        {
            this.FilePath = file;
            this.ExpectedClassification = expectedClassification;
            this.UserClassification = Category.Unclassified;
        }
    }

    public static class ShuffleExtension
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
