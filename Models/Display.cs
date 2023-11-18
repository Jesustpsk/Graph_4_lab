using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Graph_4_lab.Models;

public class Display
{
    
    public static void SetAxis(HelixViewport3D helixViewport)
    {
        // Добавление координатных прямых
        var xAxis = new LinesVisual3D
        {
            Points = new Point3DCollection { new(0, 0, 0), new(5, 0, 0) },
            Color = Colors.Red
        };

        var yAxis = new LinesVisual3D
        {
            Points = new Point3DCollection { new (0, 0, 0), new(0, 5, 0) },
            Color = Colors.Green
        };

        var zAxis = new LinesVisual3D
        {
            Points = new Point3DCollection { new (0, 0, 0), new (0, 0, 5) },
            Color = Colors.Blue
        };
        helixViewport.Children.Add(xAxis);
        helixViewport.Children.Add(yAxis);
        helixViewport.Children.Add(zAxis);
        
        // Добавление подписей к осям
        var labelX = new BillboardTextVisual3D
        {
            Position = new Point3D(5.2, 0, 0),
            Text = "X",
            Foreground = Brushes.Red,
            FontSize = 10
        };
        
        var labelY = new BillboardTextVisual3D
        {
            Position = new Point3D(0, 5.2, 0),
            Text = "Y",
            Foreground = Brushes.Green,
            FontSize = 10
        };

        var labelZ = new BillboardTextVisual3D
        {
            Position = new Point3D(0, 0, 5.2),
            Text = "Z",
            Foreground = Brushes.Blue,
            FontSize = 10
        };

        var label0 = new BillboardTextVisual3D
        {
            Position = new Point3D(-0.1, -0.1, -0.1),
            Text = "0",
            Foreground = Brushes.Black,
            FontSize = 10
        };
        

        helixViewport.Children.Add(labelX);
        helixViewport.Children.Add(labelY);
        helixViewport.Children.Add(labelZ);
        helixViewport.Children.Add(label0);
    }
    
    public static List<Point3D> CreateFigure(List<Point3D> Points, MeshGeometryVisual3D meshVisual, HelixViewport3D helixViewport)
    {
        var meshBuilder = new MeshBuilder();
        foreach (var p in Points)
        {
            meshBuilder.Positions.Add(p);
        }

        var meshGeometry = meshBuilder.ToMesh();
        
        // Добавление линий, соединяющих вершины фигуры
        for (int i = 0; i < meshBuilder.Positions.Count - 1; i++)
        {
            var line = new TubeVisual3D
            {
                Diameter = 0.02,
                Path = new Point3DCollection { meshBuilder.Positions[i], meshBuilder.Positions[i + 1] },
                Fill = Brushes.Black
            };

            helixViewport.Children.Add(line);
        }

        var vertices = new List<Point3D>();
        var strPoints = Points3DtoStrings(Points);
        foreach (var p in Points)
        {
            if (!strPoints.Contains(Point3DtoString(p))) continue;
            var labelC = new BillboardTextVisual3D
            {
                Position = new Point3D(p.X + 0.1, p.Y + 0.1, p.Z + 0.1),
                Text = Point3DtoString(p),
                Foreground = Brushes.Black,
                FontSize = 8
            };
            helixViewport.Children.Add(labelC);
            vertices.Add(p);
        }
        meshVisual.MeshGeometry = meshGeometry;
        return vertices;
    }
    
    public static List<Point3D> CreateList3D(string dots)
    {
        List<Point3D> output = new();
        foreach (var line in dots.Split('\n'))
        {
            output.Add(new Point3D(Convert.ToDouble(line.Split(',')[0].Replace('.',',')), 
                Convert.ToDouble(line.Split(',')[1].Replace('.',',')), 
                Convert.ToDouble(line.Split(',')[2].Replace('.',','))));
        }

        return output;
    }
    private static List<string> Points3DtoStrings(List<Point3D> Points)
    {
        var output = new List<string>();

        foreach (var p in Points)
        {
            output.Add("(" + p.X + ";" + p.Y + ";" + p.Z + ")");
        }

        return output.Distinct().ToList();
    }

    private static string Point3DtoString(Point3D p)
    {
        return "(" + p.X + ";" + p.Y + ";" + p.Z + ")";
    }

    public static void FillListView(ListView listView, List<Point3D> point3Ds)
    {
        var items = point3Ds.Distinct().Select((t, i) => new MyPoint() 
            { N = (i + 1).ToString().PadLeft(15), X = t.X.ToString(CultureInfo.InvariantCulture).PadLeft(15), 
                Y = t.Y.ToString(CultureInfo.InvariantCulture).PadLeft(15), 
                Z = t.Z.ToString(CultureInfo.InvariantCulture).PadLeft(15) }).ToList();
        listView.ItemsSource = items;
    }
}

public class MyPoint
{
    public string N { get; set; }
    public string X { get; set; }
    public string Y { get; set; }
    public string Z { get; set; }
}