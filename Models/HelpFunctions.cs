using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Graph_4_lab.Models;

public class HelpFunctions
{
    public static Point3D RoundUp(Point3D p, int digits)
    {
        var factor = Convert.ToDecimal(Math.Pow(10, digits));
        double x = p.X, y = p.Y, z = p.Z;
        if (p.X > 1)
            x = Convert.ToDouble(Math.Ceiling(Convert.ToDecimal(p.X) * factor) / factor);
        if (p.Y > 1)
            y = Convert.ToDouble(Math.Ceiling(Convert.ToDecimal(p.Y) * factor) / factor);
        if (p.Z > 1)
            z = Convert.ToDouble(Math.Ceiling(Convert.ToDecimal(p.Z) * factor) / factor);
        return new Point3D(x, y, z);
    }

    public static Point3D TextToPoint(string text)
    {
        var list = text.Split(',');
        if (list.Length > 3) return new Point3D(0, 0, 0);
        var dlist = list.Select(s => Convert.ToDouble(s.Replace('.', ','))).ToList();
        return new Point3D(dlist[0], dlist[1], dlist[2]);
    }
    
    public Vector3D GetPerpendicularVectorThroughOrigin(Point3D edgePoint1, Point3D edgePoint2)
    {
        // Находим вектор, представляющий ребро
        Vector3D edgeVector = new Vector3D(edgePoint2.X - edgePoint1.X, edgePoint2.Y - edgePoint1.Y, edgePoint2.Z - edgePoint1.Z);

        // Нормализуем вектор
        edgeVector.Normalize();

        // Определяем перпендикулярный вектор, лежащий в плоскости, несущей максимальную компоненту ребра
        Vector3D perpendicularVector;

        if (Math.Abs(edgeVector.X) >= Math.Abs(edgeVector.Y) && Math.Abs(edgeVector.X) >= Math.Abs(edgeVector.Z))
        {
            perpendicularVector = new Vector3D(0, -edgeVector.Z, edgeVector.Y);
        }
        else if (Math.Abs(edgeVector.Y) >= Math.Abs(edgeVector.X) && Math.Abs(edgeVector.Y) >= Math.Abs(edgeVector.Z))
        {
            perpendicularVector = new Vector3D(-edgeVector.Z, 0, edgeVector.X);
        }
        else
        {
            perpendicularVector = new Vector3D(-edgeVector.Y, edgeVector.X, 0);
        }

        return perpendicularVector;
    }
}