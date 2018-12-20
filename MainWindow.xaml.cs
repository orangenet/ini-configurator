using IniConfigurator.Controls;
using IniParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;

namespace IniConfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<IniConfigSection> iniConfigSections;

        public MainWindow()
        {
            InitializeComponent();

            iniConfigSections = new List<IniConfigSection>();

            try
            {
                var currentDicrectory = Directory.GetCurrentDirectory();
                var parentDirectory = Directory.GetParent(currentDicrectory)?.FullName;
                const string iniFileName = "configurator.ini";
                var iniPath = Path.Combine(parentDirectory, iniFileName);
                if (!File.Exists(iniPath)) {
                    iniPath = Path.Combine(currentDicrectory, iniFileName);
                }

                if (!File.Exists(iniPath))
                {
                    throw new Exception();
                }

                LoadConfiguratorIni(iniPath);
            }
            catch
            {
                MessageBox.Show("Unable to load configurator.ini file");
                Environment.Exit(0);
            }
        }

        private void ButtonConfigure_Click(object sender, RoutedEventArgs e)
        {
            ConfigureSubDirectories();
        }

        private void LoadConfiguratorIni(string iniPath)
        {
            var parser = new FileIniDataParser();
            var data = parser.ReadFile(iniPath);

            iniConfigSections.Clear();
            SectionsStackPanel.Children.Clear();

            foreach (var section in data.Sections)
            {
                var iniConfigSection = new IniConfigSection();
                foreach (var param in section.Keys)
                {
                    iniConfigSection.SetData(section.SectionName);
                    iniConfigSection.AddParam(param.KeyName, string.Join(".", param.Comments), param.Value);
                }
                iniConfigSections.Add(iniConfigSection);
                SectionsStackPanel.Children.Add(iniConfigSection);
            }
        }

        private void ConfigureSubDirectories()
        {

            var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory());
            // Select files to configure
            var iniFiles = new DirectoryInfo(parentDirectory.FullName)
                            .GetFiles("*.ini", SearchOption.AllDirectories)
                            .Where(x => !(x.Name == "configurator.ini"
                                        && x.Directory.FullName == parentDirectory.FullName))
                            .ToList();

            if(iniFiles.Count == 0)
            {
                MessageBox.Show("No file to configure, please put this application in the main folder");
                return;
            }

            // Get Data
            var parameters = iniConfigSections.SelectMany(x => x.GetIniParams());

            var updatedFiles = 0;
            try
            {
                foreach (var iniFile in iniFiles)
                {
                    var parser = new FileIniDataParser();
                    var data = parser.ReadFile(iniFile.FullName);

                    var updated = false;

                    foreach (var parameter in parameters)
                    {
                        if (data.Sections.ContainsSection(parameter.Section)
                            && data.Sections[parameter.Section].ContainsKey(parameter.Param.Name))
                        {
                            updated = true;
                            data[parameter.Section][parameter.Param.Name] = parameter.Param.Val;
                        }
                    }

                    if(updated)
                    {
                        updatedFiles++;
                        parser.WriteFile(iniFile.FullName, data);
                    }
                }

                MessageBox.Show($"Configuration files updated successfully\nConfigured files number: {updatedFiles}");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}