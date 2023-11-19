using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Graph_4_lab.Models;

public class InputBox
{
    private readonly Window _box = new();// window for the inputbox
    private readonly FontFamily _font = new("Tahoma");// font for the whole inputbox
    private readonly int _fontSize=30;// fontsize for the input
    private readonly StackPanel _sp1=new();// items container
    private readonly string _title = "InputBox";// title as heading
    private readonly string _boxcontent;// title
    private readonly string _defaulttext = "Example: 0 0 0 0";// default textbox content
    private const string Errormessage = "Invalid answer"; // error messagebox content
    private const string Errortitle = "Error"; // error messagebox heading title
    private const string Okbuttontext = "OK"; // Ok button content
    private readonly Brush _boxBackgroundColor = Brushes.White;// Window Background
    private readonly Brush _inputBackgroundColor = Brushes.White;// Textbox Background
    private bool _clicked = false;
    private readonly TextBox _input = new();
    private readonly Button _ok = new();
    private bool _inputreset = false;

    public InputBox(string content)
    {
        try
        {
            _boxcontent = content;
        }
        catch { _boxcontent = "Error!"; }
        Windowdef();
    }

    public InputBox(string content,string Htitle, string DefaultText)
    {
        try
        {
            _boxcontent = content;
        }
        catch { _boxcontent = "Error!"; }
        try
        {
            _title = Htitle;
        }
        catch 
        {
            _title = "Error!";
        }
        try
        {
            _defaulttext = DefaultText;
        }
        catch
        {
            DefaultText = "Error!";
        }
        Windowdef();
    }

    public InputBox(string content, string Htitle, string Font, int Fontsize)
    {
        try
        {
            _boxcontent = content;
        }
        catch { _boxcontent = "Error!"; }
        try
        {
            _font = new FontFamily(Font);
        }
        catch { _font = new FontFamily("Tahoma"); }
        try
        {
            _title = Htitle;
        }
        catch
        {
            _title = "Error!";
        }
        if (Fontsize >= 1)
            _fontSize = Fontsize;
        Windowdef();
    }

    private void Windowdef()// window building - check only for window size
    {
        _box.Height = 150;// Box Height
        _box.Width = 300;// Box Width
        _box.Background = _boxBackgroundColor;
        _box.Title = _title;
        _box.Content = _sp1;
        _box.Closing += Box_Closing!;
        var content = new TextBlock
        {
            TextWrapping = TextWrapping.Wrap,
            Background = null,
            HorizontalAlignment = HorizontalAlignment.Center,
            Text = _boxcontent,
            FontFamily = _font,
            FontSize = _fontSize
        };
        _sp1.Children.Add(content);

        _input.Background = _inputBackgroundColor;
        _input.FontFamily = _font;
        _input.FontSize = _fontSize;
        _input.HorizontalAlignment = HorizontalAlignment.Center;
        _input.Text = _defaulttext;
        _input.MinWidth = 200;
        _input.MouseEnter += input_MouseDown;
        _sp1.Children.Add(_input);
        _ok.Width=70;
        _ok.Height=30;
        _ok.Click += ok_Click;
        _ok.Content = Okbuttontext;
        _ok.HorizontalAlignment = HorizontalAlignment.Center;
        _sp1.Children.Add(_ok);

    }

    private void Box_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if(!_clicked)
            e.Cancel = true;
    }

    private void input_MouseDown(object sender, MouseEventArgs e)
    {
        if ((sender as TextBox)?.Text != _defaulttext || _inputreset) return;
        ((sender as TextBox)!).Text = null;
        _inputreset = true;
    }

    private void ok_Click(object sender, RoutedEventArgs e)
    {
        _clicked = true;
        if (_input.Text == _defaulttext||_input.Text == "")
            MessageBox.Show(Errormessage,Errortitle);
        else
        {
            _box.Close();
        }
        _clicked = false;
    }

    public string ShowDialog()
    {
        _box.ShowDialog();
        return _input.Text;
    }
}