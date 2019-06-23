using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

/// <summary>
/// By André Normann
/// C# 2 game project
/// 2019-06-04
/// </summary>
/// 
namespace GameProject
{

    public partial class Window1 : Window
    {
        private List<int> saveList = new List<int>();

        /// <summary>
        /// fills listbox with all available classes
        /// </summary>
        public Window1()
        {
            InitializeComponent();

            string[] Classes = Enum.GetNames(typeof(EnumClasses)); //checks whats inside the enums and fills it with whats in it with a foreach loop
            foreach (var item in Classes)
                cmbClasses.Items.Add(item);

        }

        /// <summary>
        /// Starts the game if a class is chosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (cmbClasses.SelectedIndex >= 0)
            {
                GameWindow main = new GameWindow();
                main.Show();
                main.InitializeGUI(cmbClasses.SelectedItem.ToString(), false, saveList);
                this.Close();
            }
            else
            {
                MessageBox.Show("Select a class inside the drop down menu");
            }
        }

        /// <summary>
        /// loads all information from the textfile and XML file to a list and then sends it off to the mainform inform of a list.
        /// 
        /// Using two different file openings is a choice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select a .txt file";
            open.Filter = "Text|*.txt|All|*.*";
            open.ShowDialog();
            string fileName = open.FileName;


            OpenFileDialog open2 = new OpenFileDialog();
            open2.Title = "Select a .xml file";
            open2.Filter = "XML|*.xml|All|*.*";
            open2.ShowDialog();
            string fileName2 = open2.FileName;
            List<string> textList = new List<string>();

            if (!string.IsNullOrEmpty(fileName) || !string.IsNullOrEmpty(fileName2))
            {
                textList = Gamehandler.ReadFromTextFile(fileName);
                try
                {
                    if (textList.Count > 4)
                    {
                        int x = 6;
                        GameWindow main = new GameWindow();
                        for (int i = 0; i < 5; i++)
                        {
                            saveList.Add(Convert.ToInt32(textList[i]));
                        }

                        int count = Convert.ToInt32(textList[5]);
                        for (int i = 0; i <= count - 1; i++)
                        {
                            main.AddToComboBox(textList[x]);
                            x++;
                        }
                        main.Lvl = Convert.ToInt32(textList[x]);
                        x++;
                        main.Experience = Convert.ToInt32(textList[x]);
                        x++;
                        main.Gold = Convert.ToInt32(textList[x]);
                        x++;
                        main.InitializeGUI(textList[x], true, saveList);
                        x++;

                        for (int i = x; i < textList.Count; i++)
                        {
                            main.ConsoleMessage(textList[i]);

                        }


                        main.WeaponDamage = Gamehandler.ReadXMLFile<List<int>>(fileName2);
                        main.Show();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Textfile does not contain proper syntax and could not be loaded", "error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Textfile does not contain proper syntax");
                }
            }
            else
            {
                MessageBox.Show("You did not select any files", "Error ");
            }

           
        }

        /// <summary>
        /// changes picture depending on what class is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cmbClasses.SelectedItem.ToString())
            {
                case "Rogue":
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/GameProject;component/Resources/Rogue.jpg", UriKind.Absolute));
                    break;
                case "Knight":
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/GameProject;component/Resources/Knight.jpg", UriKind.Absolute));
                    break;
                case "Mage":
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/GameProject;component/Resources/Mage.jpg", UriKind.Absolute));
                    break;
                default:
                    break;
            }
        }
    }
}
