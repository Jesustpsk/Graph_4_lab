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
using static Graph_4_lab.Models.MyMatrix;

namespace Graph_4_lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string objectFilePath;
        public MainWindow()
        {
            InitializeComponent();
            
            Display.SetAxis(helixViewportInput);
            Display.SetAxis(helixViewportOutput);
            // Включение вращения мышью
            helixViewportInput.RotateGesture = new MouseGesture(MouseAction.LeftClick);
            helixViewportOutput.RotateGesture = new MouseGesture(MouseAction.LeftClick);
            
            tbFirst.Visibility = Visibility.Hidden;
            lFist.Visibility = Visibility.Hidden;
            tbSecond.Visibility = Visibility.Hidden;
            lSecond.Visibility = Visibility.Hidden;
            tbThird.Visibility = Visibility.Hidden;
            lThird.Visibility = Visibility.Hidden;
            tbFourth.Visibility = Visibility.Hidden;
            lFourth.Visibility = Visibility.Hidden;
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
            
            Transformations.vertices = Display.CreateFigure(Transformations._points, meshVisualInput, helixViewportInput);
            Display.FillListView(CordListBefore, Transformations.vertices);
            CBoxTransform.SelectedIndex = -1;
            InfoBlock.Text = "";
            InitializeMatrix(gridMatrix);
        }
        
        private void LoadObjectFromFile(string filePath)
        {
            Transformations._points.Clear();
            Transformations.TempPoints.Clear();
            Transformations.vertices.Clear();
            tbFirst.Visibility = Visibility.Hidden;
            lFist.Visibility = Visibility.Hidden;
            tbSecond.Visibility = Visibility.Hidden;
            lSecond.Visibility = Visibility.Hidden;
            tbThird.Visibility = Visibility.Hidden;
            lThird.Visibility = Visibility.Hidden;
            tbFourth.Visibility = Visibility.Hidden;
            lFourth.Visibility = Visibility.Hidden;
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
            Transformations._points = Display.CreateList3D(dots);
        }

        private void ButtonExecute_OnClick(object sender, RoutedEventArgs e)
        {
            double angle;
            switch (CBoxTransform.SelectedIndex)
            {
                case 0:
                    var firstPoint = HelpFunctions.TextToPoint(tbFirst.Text);
                    var secondPoint = HelpFunctions.TextToPoint(tbSecond.Text);
                    Transformations.Translate(gridMatrix, CordListAfter, meshVisualOutput, helixViewportOutput, firstPoint, secondPoint);
                    break;
                case 1:
                    var normal1 = HelpFunctions.TextToPoint(tbFirst.Text);
                    var normal2 = HelpFunctions.TextToPoint(tbSecond.Text);
                    var normal3 = HelpFunctions.TextToPoint(tbThird.Text);
                    angle = Convert.ToDouble(tbFourth.Text.Replace('.',','));
                    Transformations.Scale(gridMatrix, CordListAfter, meshVisualOutput, helixViewportOutput, normal1, normal2, normal3, angle);
                    break;
                case 2:
                    var linePoint1 = HelpFunctions.TextToPoint(tbFirst.Text);
                    var linePoint2 = HelpFunctions.TextToPoint(tbSecond.Text);
                    Transformations.Reflect(gridMatrix, CordListAfter, meshVisualOutput, helixViewportOutput, linePoint1, linePoint2);
                    break;
                case 3:
                    var PointP = HelpFunctions.TextToPoint(tbFirst.Text);
                    angle = Convert.ToDouble(tbSecond.Text.Replace('.',','));
                    Transformations.Rotate(gridMatrix, CordListAfter, meshVisualOutput, helixViewportOutput, PointP, angle);
                    break;
            }
        }

        private void ButtonApply_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateMatrix(gridMatrix);
            switch (CBoxTransform.SelectedIndex)
            {
                case 0:
                    Transformations.Translate(gridMatrix, CordListAfter, meshVisualOutput, helixViewportOutput);
                    break;
                case 1:
                    Transformations.Scale(gridMatrix, CordListAfter, meshVisualOutput, helixViewportOutput);
                    break;
                case 2:
                    Transformations.Reflect(gridMatrix, CordListAfter, meshVisualOutput, helixViewportOutput);
                    break;
                case 3:
                    Transformations.Rotate(gridMatrix, CordListAfter, meshVisualOutput, helixViewportOutput);
                    break;
            }
        }

        #region Info
        
        private void CBItem_Move_Selected(object sender, RoutedEventArgs e)
        {
            InfoBlock.Text = "Info: Вдоль вектора, перпендикулярного ребру и проходящего через начало координат на расстояние, равное расстоянию от начала координат до ребра; \nУказание ребра";
            tbFirst.Visibility = Visibility.Visible;
            lFist.Visibility = Visibility.Visible;
            lFist.Content = "Первая точка ребра:";
            
            tbSecond.Visibility = Visibility.Visible;
            lSecond.Visibility = Visibility.Visible;
            lSecond.Content = "Вторая точка ребра:";
            
            tbThird.Visibility = Visibility.Hidden;
            lThird.Visibility = Visibility.Hidden;
            lThird.Content = "";
            
            tbFourth.Visibility = Visibility.Hidden;
            lFourth.Visibility = Visibility.Hidden;
            lFourth.Content = "";
        }
        
        private void CBItem_Scaling_Selected(object sender, RoutedEventArgs e)
        {
            InfoBlock.Text = "Info: Вдоль нормали к плоскости; \nВвод координат трёх точек плоскости; \nВвод масштабного коэффициента";
            tbFirst.Visibility = Visibility.Visible;
            lFist.Visibility = Visibility.Visible;
            lFist.Content = "Первая точка norm.:";
            
            tbSecond.Visibility = Visibility.Visible;
            lSecond.Visibility = Visibility.Visible;
            lSecond.Content = "Вторая точка norm.:";
            
            tbThird.Visibility = Visibility.Visible;
            lThird.Visibility = Visibility.Visible;
            lThird.Content = "Третья точка norm.:";
            
            tbFourth.Visibility = Visibility.Visible;
            lFourth.Visibility = Visibility.Visible;
            lFourth.Content = "Масштаб:";
        }

        private void CBItem_Reflection_Selected(object sender, RoutedEventArgs e)
        {
            InfoBlock.Text = "Info: Указание двух точек, через которые проходит прямая";
            tbFirst.Visibility = Visibility.Visible;
            lFist.Visibility = Visibility.Visible;
            lFist.Content = "Первая точка:";
            
            tbSecond.Visibility = Visibility.Visible;
            lSecond.Visibility = Visibility.Visible;
            lSecond.Content = "Вторая точка:";
            
            tbThird.Visibility = Visibility.Hidden;
            lThird.Visibility = Visibility.Hidden;
            lThird.Content = "";
            
            tbFourth.Visibility = Visibility.Hidden;
            lFourth.Visibility = Visibility.Hidden;
            lFourth.Content = "";

        }

        private void CBItem_Rotation_Selected(object sender, RoutedEventArgs e)
        {
            InfoBlock.Text = "Info: Указание точки P, ввод величины угла поворота";
            tbFirst.Visibility = Visibility.Visible;
            lFist.Visibility = Visibility.Visible;
            lFist.Content = "Точка P:";
            
            tbSecond.Visibility = Visibility.Visible;
            lSecond.Visibility = Visibility.Visible;
            lSecond.Content = "Угол поворота:";
            
            tbThird.Visibility = Visibility.Hidden;
            lThird.Visibility = Visibility.Hidden;
            lThird.Content = "";
            
            tbFourth.Visibility = Visibility.Hidden;
            lFourth.Visibility = Visibility.Hidden;
            lFourth.Content = "";
        }
        #endregion
        
        private void GridMatrix_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            gridMatrix.BeginEdit();
        }
    }
}