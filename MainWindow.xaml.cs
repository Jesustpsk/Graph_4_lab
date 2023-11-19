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
using static Graph_4_lab.Models.ProjectionMatrix;

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
        public static List<List<double>>? matrix = new();
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
            Display.FillListView(CordListBefore, vertices);
            CBoxTransform.SelectedIndex = -1;
            InfoBlock.Text = "";
        }
        private void BtnProj_OnClick(object sender, RoutedEventArgs e)
        {
            matrix.Clear();
            //построение матрицы проецирования
            //test
            matrix = new()
            {
                new List<double> {1, 1, 1, 1},
                new List<double> {1, 1, 1, 1},
                new List<double> {1, 1, 1, 1},
                new List<double> {1, 1, 1, 1}
            }; 
            

            SetMatrix(matrix, ProjList);
        }
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (ProjList.Items.Count == 0) return;
            var index = ProjList.SelectedIndex;
            var inputRead = new InputBox("Insert matrix string", "Title", "Arial", 15).ShowDialog();
            var reg = new Regex($@"([0-9] [0-9] [0-9] [0-9])");
            var match = reg.Match(inputRead);
            if (match.ToString() == "") return;
            ChangeValue(ProjList, inputRead, index, matrix);
            ProjList.Items.Refresh();
            
            //вызов применения матрицы к фигуре
        }
        
        private void LoadObjectFromFile(string filePath)
        {
            _figure.Clear();
            vertices.Clear();
            matrix!.Clear();
            
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

        private void CBItem_Move_Selected(object sender, RoutedEventArgs e)
        {
            InfoBlock.Text = "Info: Вдоль вектора, перпендикулярного ребру и проходящего через начало координат на расстояние, равное расстоянию от начала координат до ребра; \nУказание ребра";
        }
        
        private void CBItem_Scaling_Selected(object sender, RoutedEventArgs e)
        {
            InfoBlock.Text = "Info: Вдоль нормали к плоскости; \nВвод координат трёх точек плоскости; \nВвод масштабного коэффициента";
        }

        private void CBItem_Reflection_Selected(object sender, RoutedEventArgs e)
        {
            InfoBlock.Text = "Info: Указание двух точек, через которые проходит прямая";
        }

        private void CBItem_Rotation_Selected(object sender, RoutedEventArgs e)
        {
            InfoBlock.Text = "Info: Указание точки P, ввод величины угла поворота";
        }
    }
}