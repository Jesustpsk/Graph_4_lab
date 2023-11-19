using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Graph_4_lab.Models;
using Microsoft.Win32;

namespace Graph_4_lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string objectFilePath;
        private static List<Point3D> vertices = new();
        public static List<Point3D> _figure = new();
        public MainWindow()
        {
            InitializeComponent();
            
            Display.SetAxis(helixViewportInput);
            Display.SetAxis(helixViewportOutput);
            // Включение вращения мышью
            helixViewportInput.RotateGesture = new MouseGesture(MouseAction.LeftClick);
            helixViewportOutput.RotateGesture = new MouseGesture(MouseAction.LeftClick);
        }
        
        private void BtnLoadFile_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|Object Files (*.obj)|*.obj|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() != true) return;
            objectFilePath = openFileDialog.FileName;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            LoadObjectFromFile(objectFilePath);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            
            vertices = Display.CreateFigure(_figure, meshVisualInput, helixViewportInput);
            Display.FillListView(CordList, vertices);
        }
        private void BtnProj_OnClick(object sender, RoutedEventArgs e)
        {
            ProjList.Items.Clear();
            
            //построение матрицы проецирования
            var items = new List<ProjectionMatrix> //test
            {
                new() { R1 = "1", R2 = "2", R3 = "3", R4 = "4"},
                new() { R1 = "1", R2 = "2", R3 = "3", R4 = "4"},
                new() { R1 = "1", R2 = "2", R3 = "3", R4 = "4"}
            };

            ProjList.ItemsSource = items;
        }
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (ProjList.Items.Count == 0) return;
            var index = ProjList.SelectedIndex;
            var inputRead = new InputBox("Insert matrix string", "Title", "Arial", 15).ShowDialog();
            var reg = new Regex($@"([0-9] [0-9] [0-9] [0-9])");
            var match = reg.Match(inputRead);
            if (match.ToString() == "") return;
            (ProjList.Items[index] as ProjectionMatrix)!.R1 = inputRead.Split(' ')[0];
            (ProjList.Items[index] as ProjectionMatrix)!.R2 = inputRead.Split(' ')[1];
            (ProjList.Items[index] as ProjectionMatrix)!.R3 = inputRead.Split(' ')[2];
            (ProjList.Items[index] as ProjectionMatrix)!.R3 = inputRead.Split(' ')[3];
            ProjList.Items.Refresh();
            
            //вызов применения матрицы к фигуре
        }
        
        private void LoadObjectFromFile(string filePath)
        {
            _figure.Clear();
            vertices.Clear();
            CordList.Items.Clear();
            ProjList.Items.Clear();
            
            meshVisualInput.Children.Clear();
            meshVisualOutput.Children.Clear();
            helixViewportInput.Children.Clear();
            helixViewportOutput.Children.Clear();
            
            Display.SetAxis(helixViewportInput);
            Display.SetAxis(helixViewportOutput);
            
            // Включение вращения мышью
            helixViewportInput.RotateGesture = new MouseGesture(MouseAction.LeftClick);
            helixViewportOutput.RotateGesture = new MouseGesture(MouseAction.LeftClick);
            
            // Установка камеры
            helixViewportInput.CameraController.ZoomExtents();
            helixViewportOutput.CameraController.ZoomExtents();
            
            using var reader = new StreamReader(filePath);
            var dots = reader.ReadToEnd();
            if (dots == "") return;
            _figure = Display.CreateList3D(dots);
        }
    }
}