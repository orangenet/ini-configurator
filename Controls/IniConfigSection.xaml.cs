using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;

namespace IniConfigurator.Controls
{
    /// <summary>
    /// Interaction logic for IniConfigSection.xaml
    /// </summary>
    public partial class IniConfigSection : UserControl
    {
        private List<IniConfigParam> iniConfigParams;
        public IniConfigSection()
        {
            InitializeComponent();

            iniConfigParams = new List<IniConfigParam>();
        }

        public void SetData(string sectionName)
        {
            TitleLabel.Content = sectionName;
        }

        public void AddParam(string name, string description, string value = "")
        {
            var iniConfigParam = new IniConfigParam();
            iniConfigParam.SetData(name, description, value);
            iniConfigParams.Add(iniConfigParam);
            ParamsPanel.Children.Add(iniConfigParam);
        }

        public List<(string Section, (string Name, string Val) Param)> GetIniParams()
        {
            return iniConfigParams.Select(x => (TitleLabel.Content.ToString(), x.GetIniParam())).ToList();
        }
    }
}