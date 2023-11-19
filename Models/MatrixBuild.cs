using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace Graph_4_lab.Models;

public class MatrixBuild
{
    public static double[,] TranslateAlongEdge(Point3D edgePoint1, Point3D edgePoint2)
    {
        Vector3D edgeVector = new Vector3D(edgePoint2.X - edgePoint1.X, edgePoint2.Y - edgePoint1.Y, edgePoint2.Z - edgePoint1.Z);

        double distance = Math.Sqrt(edgePoint1.X * edgePoint1.X + edgePoint1.Y * edgePoint1.Y + edgePoint1.Z * edgePoint1.Z);

        edgeVector.Normalize();

        Vector3D perpendicularVector = new Vector3D(-edgeVector.Y, edgeVector.X, 0);
        perpendicularVector.Normalize();

        double[,] translationMatrix = {
            {1, 0, 0, perpendicularVector.X * distance},
            {0, 1, 0, perpendicularVector.Y * distance},
            {0, 0, 1, perpendicularVector.Z * distance},
            {0, 0, 0, 1}
        };

        return translationMatrix;
    }

    public static double[,] ScaleAboutPlane(Point3D planePoint1, Point3D planePoint2, Point3D planePoint3, double scaleFactor)
    {
        Vector3D normal = Vector3D.CrossProduct(planePoint2 - planePoint1, planePoint3 - planePoint1);
        normal.Normalize();

        // Вычисляем центр плоскости
        Point3D center = new Point3D(
            (planePoint1.X + planePoint2.X + planePoint3.X) / 3,
            (planePoint1.Y + planePoint2.Y + planePoint3.Y) / 3,
            (planePoint1.Z + planePoint2.Z + planePoint3.Z) / 3
        );

        // Вычисляем смещение вдоль нормали
        double offset = scaleFactor - scaleFactor * (normal.X * center.X + normal.Y * center.Y + normal.Z * center.Z);

        double[,] scaleMatrix = {
            {scaleFactor + offset * normal.X * normal.X, offset * normal.X * normal.Y, offset * normal.X * normal.Z, 0},
            {offset * normal.Y * normal.X, scaleFactor + offset * normal.Y * normal.Y, offset * normal.Y * normal.Z, 0},
            {offset * normal.Z * normal.X, offset * normal.Z * normal.Y, scaleFactor + offset * normal.Z * normal.Z, 0},
            {0, 0, 0, 1}
        };

        return scaleMatrix;
    }

    public static double[,] ReflectAboutLine(Point3D linePoint1, Point3D linePoint2)
    {
        Vector3D lineVector = linePoint2 - linePoint1;
        lineVector.Normalize();

        double[,] reflectionMatrix = {
            {1 - 2 * lineVector.X * lineVector.X, -2 * lineVector.X * lineVector.Y, -2 * lineVector.X * lineVector.Z, 0},
            {-2 * lineVector.Y * lineVector.X, 1 - 2 * lineVector.Y * lineVector.Y, -2 * lineVector.Y * lineVector.Z, 0},
            {-2 * lineVector.Z * lineVector.X, -2 * lineVector.Z * lineVector.Y, 1 - 2 * lineVector.Z * lineVector.Z, 0},
            {0, 0, 0, 1}
        };

        return reflectionMatrix;
    }


    public static double[,] RotateAroundLine(Point3D pointP, double angle)
    {
        Vector3D OP = pointP - new Point3D(0, 0, 0);
        OP.Normalize();

        double cosTheta = Math.Cos(angle);
        double sinTheta = Math.Sin(angle);

        double[,] rotationMatrix = {
            {cosTheta + OP.X * OP.X * (1 - cosTheta), OP.X * OP.Y * (1 - cosTheta) - OP.Z * sinTheta, OP.X * OP.Z * (1 - cosTheta) + OP.Y * sinTheta, 0},
            {OP.Y * OP.X * (1 - cosTheta) + OP.Z * sinTheta, cosTheta + OP.Y * OP.Y * (1 - cosTheta), OP.Y * OP.Z * (1 - cosTheta) - OP.X * sinTheta, 0},
            {OP.Z * OP.X * (1 - cosTheta) - OP.Y * sinTheta, OP.Z * OP.Y * (1 - cosTheta) + OP.X * sinTheta, cosTheta + OP.Z * OP.Z * (1 - cosTheta), 0},
            {0, 0, 0, 1}
        };

        return rotationMatrix;
    }
    
    public static List<Point3D> ApplyTransformation(List<Point3D> figure, double[,] transformationMatrix)
    {
        var transformedFigure = new List<Point3D>();

        foreach (var point in figure)
        {
            double x = point.X * transformationMatrix[0, 0] + point.Y * transformationMatrix[0, 1] + point.Z * transformationMatrix[0, 2] + transformationMatrix[0, 3];
            double y = point.X * transformationMatrix[1, 0] + point.Y * transformationMatrix[1, 1] + point.Z * transformationMatrix[1, 2] + transformationMatrix[1, 3];
            double z = point.X * transformationMatrix[2, 0] + point.Y * transformationMatrix[2, 1] + point.Z * transformationMatrix[2, 2] + transformationMatrix[2, 3];

            transformedFigure.Add(new Point3D(x, y, z));
        }

        return transformedFigure;
    }
    
    public static Vector3D CalculateNormal(Point3D p1, Point3D p2, Point3D p3)
    {
        Vector3D v1 = p2 - p1;
        Vector3D v2 = p3 - p1;

        return Vector3D.CrossProduct(v1, v2);
    }
}