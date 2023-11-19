using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using static Graph_4_lab.Models.HelpFunctions;

namespace Graph_4_lab.Models;

public class ProjectionMatrix : INotifyPropertyChanged
{
    public string R1 { get; set; }
    public string R2 { get; set; }
    public string R3 { get; set; }
    public string R4 { get; set; }
    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Set<T>(ref T field, T value,
        [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;

        field = value;
        RaisePropertyChanged(propertyName);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public static void SetMatrix(List<List<double>>? matrix, ListView ProjMatrix)
    {
        var items = new List<ProjectionMatrix>()
        {
            
            new()
            {
                R1 = Convert.ToDecimal(matrix[0][0]).ToString(CultureInfo.InvariantCulture), R2 = Convert.ToDecimal(matrix[0][1]).ToString(CultureInfo.InvariantCulture),
                R3 = Convert.ToDecimal(matrix[0][2]).ToString(CultureInfo.InvariantCulture), R4 = Convert.ToDecimal(matrix[0][3]).ToString(CultureInfo.InvariantCulture)
            },
            new(){
                R1 = Convert.ToDecimal(matrix[1][0]).ToString(CultureInfo.InvariantCulture), R2 = Convert.ToDecimal(matrix[1][1]).ToString(CultureInfo.InvariantCulture),
                R3 = Convert.ToDecimal(matrix[1][2]).ToString(CultureInfo.InvariantCulture), R4 = Convert.ToDecimal(matrix[1][3]).ToString(CultureInfo.InvariantCulture)
            },
            new(){
                R1 = Convert.ToDecimal(matrix[2][0]).ToString(CultureInfo.InvariantCulture), R2 = Convert.ToDecimal(matrix[2][1]).ToString(CultureInfo.InvariantCulture),
                R3 = Convert.ToDecimal(matrix[2][2]).ToString(CultureInfo.InvariantCulture), R4 = Convert.ToDecimal(matrix[2][3]).ToString(CultureInfo.InvariantCulture)
            },
            new(){
                R1 = Convert.ToDecimal(matrix[3][0]).ToString(CultureInfo.InvariantCulture), R2 = Convert.ToDecimal(matrix[3][1]).ToString(CultureInfo.InvariantCulture),
                R3 = Convert.ToDecimal(matrix[3][2]).ToString(CultureInfo.InvariantCulture), R4 = Convert.ToDecimal(matrix[3][3]).ToString(CultureInfo.InvariantCulture)
            }
        };
        ProjMatrix.ItemsSource = items;
    }

    public static List<string> GetMatrix(ListView ProjList)
    {
        return new List<string>()
        {
            (ProjList.Items[0] as ProjectionMatrix)!.R1 + " " + (ProjList.Items[0] as ProjectionMatrix)!.R2 + " " + (ProjList.Items[0] as ProjectionMatrix)!.R3 + " " + (ProjList.Items[0] as ProjectionMatrix)!.R4,
            (ProjList.Items[1] as ProjectionMatrix)!.R1 + " " + (ProjList.Items[1] as ProjectionMatrix)!.R2 + " " + (ProjList.Items[1] as ProjectionMatrix)!.R3 + " " + (ProjList.Items[1] as ProjectionMatrix)!.R4,
            (ProjList.Items[2] as ProjectionMatrix)!.R1 + " " + (ProjList.Items[2] as ProjectionMatrix)!.R2 + " " + (ProjList.Items[2] as ProjectionMatrix)!.R3 + " " + (ProjList.Items[2] as ProjectionMatrix)!.R4,
            (ProjList.Items[3] as ProjectionMatrix)!.R1 + " " + (ProjList.Items[3] as ProjectionMatrix)!.R2 + " " + (ProjList.Items[3] as ProjectionMatrix)!.R3 + " " + (ProjList.Items[3] as ProjectionMatrix)!.R4
         };
    }

    public static void ChangeValue(ListView ProjList, string NewValue, int Index, List<List<double>>? matrix)
    {
        (ProjList.Items[Index] as ProjectionMatrix)!.R1 = NewValue.Split(' ')[0];
        (ProjList.Items[Index] as ProjectionMatrix)!.R2 = NewValue.Split(' ')[1];
        (ProjList.Items[Index] as ProjectionMatrix)!.R3 = NewValue.Split(' ')[2];
        (ProjList.Items[Index] as ProjectionMatrix)!.R4 = NewValue.Split(' ')[3];

        for (var i = 0; i < 4; i++)
        {
            var r1 = (ProjList.Items[i] as ProjectionMatrix)!.R1;
            var r2 = (ProjList.Items[i] as ProjectionMatrix)!.R2;
            var r3 = (ProjList.Items[i] as ProjectionMatrix)!.R3;
            var r4 = (ProjList.Items[i] as ProjectionMatrix)!.R4;
            matrix[i][0] = Convert.ToDouble(r1);
            matrix[i][1] = Convert.ToDouble(r2);
            matrix[i][2] = Convert.ToDouble(r3);
            matrix[i][3] = Convert.ToDouble(r4);
        }
    }
}