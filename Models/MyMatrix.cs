using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using static Graph_4_lab.Models.HelpFunctions;

namespace Graph_4_lab.Models;

public class MyMatrix
{
    public MyMatrix()
    {
    }

    public MyMatrix(double _r1, double _r2, double _r3, double _r4)
    {
        r1 = _r1;
        r2 = _r2;
        r3 = _r3;
        r4 = _r4;
        
    }
    
    public static double[,] _matrix = new double[4, 4];
    public double r1 { get; set; }
    public double r2 { get; set; }
    public double r3 { get; set; }
    public double r4 { get; set; }
    
    
    public static void InitializeMatrix(DataGrid Matrix)
    {
        Matrix.ItemsSource = SetItems();
    }

    public static void UpdateMatrix(DataGrid Matrix)
    {
        GetMatrixFromGrid(Matrix);
        var collection = new ObservableCollection<MyMatrix>
        {
            new() { r1 = _matrix[0, 0], r2 = _matrix[0, 1], r3 = _matrix[0, 2], r4 = _matrix[0, 3] },
            new() { r1 = _matrix[1, 0], r2 = _matrix[1, 1], r3 = _matrix[1, 2], r4 = _matrix[1, 3] },
            new() { r1 = _matrix[2, 0], r2 = _matrix[2, 1], r3 = _matrix[2, 2], r4 = _matrix[2, 3] },
            new() { r1 = _matrix[3, 0], r2 = _matrix[3, 1], r3 = _matrix[3, 2], r4 = _matrix[3, 3] }
        };
        Matrix.ItemsSource = collection;
    }

    public static ObservableCollection<MyMatrix> SetItems()
    {
        var collection = new ObservableCollection<MyMatrix>
        {
            new() { r1 = _matrix[0, 0], r2 = _matrix[0, 1], r3 = _matrix[0, 2], r4 = _matrix[0, 3] },
            new() { r1 = _matrix[1, 0], r2 = _matrix[1, 1], r3 = _matrix[1, 2], r4 = _matrix[1, 3] },
            new() { r1 = _matrix[2, 0], r2 = _matrix[2, 1], r3 = _matrix[2, 2], r4 = _matrix[2, 3] },
            new() { r1 = _matrix[3, 0], r2 = _matrix[3, 1], r3 = _matrix[3, 2], r4 = _matrix[3, 3] }
        };
        return collection;
    }
    
    public static void GetMatrixFromGrid(DataGrid Matrix)
    {
        _matrix = ParseMatrix(Matrix);
    }

    public static double[,] GetMatrix()
    {
        return _matrix;
    }

    public static double[,] ParseMatrix(DataGrid Matrix)
    {
        var output = new double[4, 4];
        var gridMatrix = Matrix.ItemsSource;
        var list = gridMatrix as ObservableCollection<MyMatrix>;
        for (var i = 0; i < 4; i++)
        {
            output[i, 0] = list[i].r1;
            output[i, 1] = list[i].r2;
            output[i, 2] = list[i].r3;
            output[i, 3] = list[i].r4;
        }
        return output;
    }
}