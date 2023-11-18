using System;
using System.Collections.Generic;
using System.IO;
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
        
        
        private void LoadObjectFromFile(string filePath)
        {
            _figure.Clear();
            vertices.Clear();
            CordList.Items.Clear();
            
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