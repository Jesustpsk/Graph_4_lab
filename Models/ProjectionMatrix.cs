using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
}