using System.Windows.Controls;

namespace IniConfigurator.Controls
{
    /// <summary>
    /// Interaction logic for IniConfigParam.xaml
    /// </summary>
    public partial class IniConfigParam : UserControl
    {
        public IniConfigParam()
        {
            InitializeComponent();
        }

        public void SetData(string name, string description, string value = "")
        {
            TitleLabel.Content = name;
            Description.Text = description;
            ParamTextBox.Text = value;
        }

        public (string Name, string Val) GetIniParam()
        {
            return (TitleLabel.Content.ToString(), ParamTextBox.Text);
        }
    }
}