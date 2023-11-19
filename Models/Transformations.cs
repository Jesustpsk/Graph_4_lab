using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Graph_4_lab.Models;

public class Transformations
{
    public static List<Point3D> _points = new();
    public static List<Point3D> TempPoints = new();
    public static List<Point3D> vertices = new();
    public static void Translate(DataGrid Matrix, ListView CordListAfter, MeshGeometryVisual3D outputMeshVisual, HelixViewport3D outputViewport, Point3D p1, Point3D p2)
    {
        outputViewport.Children.Clear();
        outputMeshVisual.Children.Clear();
        Display.SetAxis(outputViewport);
        TempPoints = _points;
        var matrix = MatrixBuild.TranslateAlongEdge(p1,p2);
        MyMatrix._matrix = matrix;
        MyMatrix.InitializeMatrix(Matrix);
        TempPoints = MatrixBuild.ApplyTransformation(TempPoints, matrix);
        var vertices = Display.CreateFigure(TempPoints, outputMeshVisual, outputViewport);
        Display.FillListView(CordListAfter, vertices);
    }

    public static void Scale(DataGrid Matrix, ListView CordListAfter, MeshGeometryVisual3D outputMeshVisual, HelixViewport3D outputViewport, Point3D planePoint1, Point3D planePoint2, Point3D planePoint3, double scaleFactor)
    {
        outputViewport.Children.Clear();
        outputMeshVisual.Children.Clear();
        Display.SetAxis(outputViewport);
        TempPoints = _points;
        var matrix = MatrixBuild.ScaleAboutPlane(planePoint1, planePoint2, planePoint3, scaleFactor);
        MyMatrix._matrix = matrix;
        MyMatrix.InitializeMatrix(Matrix);
        TempPoints = MatrixBuild.ApplyTransformation(TempPoints, matrix);
        var vertices = Display.CreateFigure(TempPoints, outputMeshVisual, outputViewport);
        Display.FillListView(CordListAfter, vertices);
    }

    public static void Reflect(DataGrid Matrix, ListView CordListAfter, MeshGeometryVisual3D outputMeshVisual, HelixViewport3D outputViewport, Point3D linePoint1, Point3D linePoint2)
    {
        outputViewport.Children.Clear();
        outputMeshVisual.Children.Clear();
        Display.SetAxis(outputViewport);
        TempPoints = _points;
        var matrix = MatrixBuild.ReflectAboutLine(linePoint1, linePoint2);
        MyMatrix._matrix = matrix;
        MyMatrix.InitializeMatrix(Matrix);
        TempPoints = MatrixBuild.ApplyTransformation(TempPoints, matrix);
        var vertices = Display.CreateFigure(TempPoints, outputMeshVisual, outputViewport);
        Display.FillListView(CordListAfter, vertices);
    }

    public static void Rotate(DataGrid Matrix, ListView CordListAfter, MeshGeometryVisual3D outputMeshVisual, HelixViewport3D outputViewport, Point3D pointP, double angle)
    {
        outputViewport.Children.Clear();
        outputMeshVisual.Children.Clear();
        Display.SetAxis(outputViewport);
        TempPoints = _points;
        var matrix = MatrixBuild.RotateAroundLine(pointP, angle);
        MyMatrix._matrix = matrix;
        MyMatrix.InitializeMatrix(Matrix);
        TempPoints = MatrixBuild.ApplyTransformation(TempPoints, matrix);
        var vertices = Display.CreateFigure(TempPoints, outputMeshVisual, outputViewport);
        Display.FillListView(CordListAfter, vertices);
    }
    
    
    
    
    public static void Translate(DataGrid Matrix, ListView CordListAfter, MeshGeometryVisual3D outputMeshVisual, HelixViewport3D outputViewport)
    {
        outputViewport.Children.Clear();
        outputMeshVisual.Children.Clear();
        Display.SetAxis(outputViewport);
        TempPoints = _points;
        MyMatrix.GetMatrixFromGrid(Matrix);
        TempPoints = MatrixBuild.ApplyTransformation(TempPoints, MyMatrix._matrix);
        var vertices = Display.CreateFigure(TempPoints, outputMeshVisual, outputViewport);
        Display.FillListView(CordListAfter, vertices);
    }

    public static void Scale(DataGrid Matrix, ListView CordListAfter, MeshGeometryVisual3D outputMeshVisual, HelixViewport3D outputViewport)
    {
        outputViewport.Children.Clear();
        outputMeshVisual.Children.Clear();
        Display.SetAxis(outputViewport);
        TempPoints = _points;
        MyMatrix.GetMatrixFromGrid(Matrix);
        TempPoints = MatrixBuild.ApplyTransformation(TempPoints, MyMatrix._matrix);
        var vertices = Display.CreateFigure(TempPoints, outputMeshVisual, outputViewport);
        Display.FillListView(CordListAfter, vertices);
    }

    public static void Reflect(DataGrid Matrix, ListView CordListAfter, MeshGeometryVisual3D outputMeshVisual, HelixViewport3D outputViewport)
    {
        outputViewport.Children.Clear();
        outputMeshVisual.Children.Clear();
        Display.SetAxis(outputViewport);
        TempPoints = _points;
        MyMatrix.GetMatrixFromGrid(Matrix);
        TempPoints = MatrixBuild.ApplyTransformation(TempPoints, MyMatrix._matrix);
        var vertices = Display.CreateFigure(TempPoints, outputMeshVisual, outputViewport);
        Display.FillListView(CordListAfter, vertices);
    }

    public static void Rotate(DataGrid Matrix, ListView CordListAfter, MeshGeometryVisual3D outputMeshVisual, HelixViewport3D outputViewport)
    {
        outputViewport.Children.Clear();
        outputMeshVisual.Children.Clear();
        Display.SetAxis(outputViewport);
        TempPoints = _points;
        MyMatrix.GetMatrixFromGrid(Matrix);
        TempPoints = MatrixBuild.ApplyTransformation(TempPoints, MyMatrix._matrix);
        var vertices = Display.CreateFigure(TempPoints, outputMeshVisual, outputViewport);
        Display.FillListView(CordListAfter, vertices);
    }
}