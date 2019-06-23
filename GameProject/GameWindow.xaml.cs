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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Threading;

/// <summary>
/// By André Normann
/// C# 2 Game Project
/// 2019-06-04
/// </summary>
namespace GameProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Random rnd = new Random();
        private Classes classes;
        private Enemy enemies;
        private Thread t;

        /// <summary>
        /// All these are needed to serialize
        /// </summary>
        private int lvl = 1;
        private int experience = 0;
        private int defence = 0;
        private int gold = 0;
        private int poison = 0;
        private List<int> weaponDamage = new List<int>();
        private List<string> adjectives;
        private int enemiesSlain = 0;
        private bool save = false;

        private bool isDead = false;
        private bool defend = false;
        private int waitTime = 0;
        private int specialWaitTime = 0;
        public GameWindow()
        {
            InitializeComponent();
            lblLevel.Content = lvl;
            lblExperience.Content = experience;
            adjectives = new List<string>() {"evil", "living", "little", "mythical", "poor", "strange", "beautiful", "wild", "rational", //list used for adjectives for the monsters 
                "social", "legendary", "good", "mysterious", "curious", "magical", "dangerous",
                "mythological", "bizarre", "monstrous", "unhappy", "huge", "lowly",
                "ugly", "happy","strange", "unique", "odd", "weird", "demonic", "divine", "imaginary", "hideous" };
           
        }


        /// <summary>
        /// Getters and setters
        /// </summary>
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        public List<int> WeaponDamage
        {
            get { return weaponDamage; }
            set { weaponDamage = value; }
        }

        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }

        public int Lvl
        {
            get { return lvl; }
            set { lvl = value; }
        }
        /// <summary>
        /// binds the classes.cs with the class that is chosen and then it uses the values inside that class 
        /// to monitor the game and adds to labels
        /// </summary>
        /// <param name="selected"></param>
        public void InitializeGUI(string selected, bool loadingProfile, List<int> savedStats)
        {

            switch (selected)
            {
                case "Knight":
                    classes = new Knight();
                    break;

                case "Mage":
                    classes = new Mage();
                    break;

                case "Rogue":
                    classes = new Rogue();
                    break;

                default:
                    MessageBox.Show("Failed initalization");
                    break;
            }
            if (!loadingProfile)
            {
                lblAttack.Content = ((Classes)classes).GetDamage().ToString();
                lblHealth.Content = ((Classes)classes).GetHealth().ToString();
                lblDefense.Content = ((Classes)classes).GetDefense().ToString();
            }

            foreach (var item in savedStats)
            {
                Console.WriteLine(item);
            }
            if (loadingProfile)
            {
                lblAttack.Content = savedStats[0];
                lblHealth.Content = savedStats[1];
                lblDefense.Content = savedStats[2];

                specialWaitTime = savedStats[3];
                waitTime = savedStats[4];

                lblSpecialWaitTime.Content = "Wait time: " + specialWaitTime;
                lblWaitTime.Content = "Wait time: " + waitTime;

            }

            ((Classes)classes).SetDamage(((Classes)classes).GetDamage());
            lblCurrentClass.Content = ((Classes)classes).GetName();

            lblGold.Content = gold;
            lblExperience.Content = experience;
            lblLevel.Content = lvl;
            ConsoleMessage("Started as a " + selected);
            NewEnemy();

            
        }

        /// <summary>
        /// spawns a new enemy based on a random number between 0 and 3 
        /// then binds the enemy.cs to the monster chosen and fills labels etc.
        /// </summary>
        public void NewEnemy()
        {
            int number = rnd.Next(0, 3);
            string adjectiveName = adjectives[rnd.Next(0, adjectives.Count)];
            switch (number)
            {
                case 0:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/GameProject;component/Resources/Snake.jpg", UriKind.Absolute));
                    enemies = new Snake();
                    ConsoleMessage("You encounter a " + adjectiveName + " " + ((Enemy)enemies).GetName());
                    //Spawn Snake
                    break;
                case 1:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/GameProject;component/Resources/Troll.jpeg", UriKind.Absolute));
                    enemies = new Troll();
                    ConsoleMessage("You encounter a " + adjectiveName + " " + ((Enemy)enemies).GetName());
                    //Spawn Troll
                    break;
                case 2:
                    //Spawn Wolf
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/GameProject;component/Resources/Wolf.jpg", UriKind.Absolute));
                    enemies = new Wolf();
                    ConsoleMessage("You encounter a "+ adjectiveName + " " + ((Enemy)enemies).GetName());
                    break;
                default:
                    MessageBox.Show("Failed initalization of enemy");
                    break;
            }

            ((Enemy)enemies).SetDamage(((Enemy)enemies).GetBaseDamage() * ((lvl / 2) + 1));
            ((Enemy)enemies).SetHealth(((Enemy)enemies).GetBasehealth() * ((lvl / 2) + 1));


            lblEnemy.Content = char.ToUpper(adjectiveName[0]) + adjectiveName.Substring(1) +  " " +((Enemy)enemies).GetName();
            lblEnemyAttack.Content =  ((Enemy)enemies).GetDamage();
            lblEnemyHealth.Content = ((Enemy)enemies).GetHealth();
            
        }

        /// <summary>
        /// Puts a message into the console for information
        /// </summary>
        /// <param name="message"></param>
        public void ConsoleMessage(string message)
        {
            listConsole.Items.Add(message);
        }


        /// <summary>
        /// when you attack it calls the combat method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAttack_Click(object sender, RoutedEventArgs e)
        {
            Combat();
        }


        /// <summary>
        /// Disables all buttons so the player cant keep playing
        /// </summary>
        private void Die()
        {
            btnAttack.IsEnabled = false;
            btnDefend.IsEnabled = false;
            btnStore.IsEnabled = false;
            btnHeal.IsEnabled = false;
            btnSpecial.IsEnabled = false;
            menuSave.IsEnabled = false;

            isDead = true;
            string end = string.Format($"You were slain by a {lblEnemy.Content} \nYou killed {enemiesSlain} enemies");
            MessageBox.Show(end, "Game Over");
            
        }
        /// <summary>
        /// when you defend it sets defence to equal to the class defence and calls the combat method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDefend_Click(object sender, RoutedEventArgs e)
        {
            defence = ((Classes)classes).GetChangedDefence();
            defend = true;
            Combat();
            defend = false;
            defence = 0;
        }

        /// <summary>
        /// Opens the storewindow and gets the item if the player bought an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStore_Click(object sender, RoutedEventArgs e)
        {
            StoreWindow store = new StoreWindow();
            store.Gold = gold;

            store.ShowDialog();
            try
            {
                if (store.GetItem() != null)
                { 
                    cmbItem.Items.Add(store.GetItem().ToString());
                    weaponDamage.Add(store.GetItem().Damage);
                    gold = store.Gold;
                    lblGold.Content = gold;
                }

            }
            catch (Exception) { }

            
        }

        /// <summary>
        /// Combat method that does the combat automatically based on atk and defense on both you and the enemy.
        /// </summary>
        private void Combat()
        {
            string message;

            if (!defend)
            {
                Gamehandler.DoDamage(classes, enemies, lblEnemy.Content.ToString(), poison, out message);
                ConsoleMessage(message);
                if (poison > 0)
                    poison--;
            }
            Gamehandler.TakeDamage(classes, enemies, lblEnemy.Content.ToString(),defence,  out message);
            ConsoleMessage(message);

            Rewards();
            listConsole.SelectedIndex = listConsole.Items.Count - 1;
        }

        /// <summary>
        /// Gives rewards if you defeat the enemy. else if you have less than 0 health you die.
        /// </summary>
        private void Rewards()
        {

            if (((Classes)classes).GetChangedHealth() <= 0) // IF YOU DIE
            {
                ConsoleMessage("You died.");
                Die();
            }
            else if (((Enemy)enemies).GetHealth() <= 0) // if you lived and kiled the enemy
            {
                int gainedExperience = rnd.Next(5, 10);
                int gainedGold = rnd.Next(20, 50);

                ConsoleMessage("You slay a " + lblEnemy.Content + " and earn " + gainedExperience + " experience" + "\n" + " + " + gainedGold + "gold"); // slaying enemy and getting  rewards

                experience += gainedExperience; // experience and gold gianed
                gold += gainedGold * lvl;

                lblExperience.Content = experience;
                lblGold.Content = gold;
                NewEnemy(); // spawn new enemy
                enemiesSlain++;


            }

            //After attack and defend
            if (experience > 150) // if you  level up
                LevelUp();

            if (waitTime > 0)
            {
                waitTime--;
                lblWaitTime.Content = "Wait time: " + waitTime;
            }
            if (specialWaitTime > 0)
            {
                specialWaitTime--;
                lblSpecialWaitTime.Content = "Wait time: " + specialWaitTime;
            }

            lblHealth.Content = ((Classes)classes).GetChangedHealth();
            lblEnemyHealth.Content = ((Enemy)enemies).GetHealth();

        }

        /// <summary>
        /// levels up and sets stats to base stats * level
        /// </summary>
        private void LevelUp()
        {

                experience = 0;
                lblExperience.Content = 0;
                lvl++;
                lblLevel.Content = lvl;

            ((Classes)classes).SetDamage(((Classes)classes).GetDamage() * lvl );
            ((Classes)classes).SetHealth(((Classes)classes).GetHealth() * lvl);
            ((Classes)classes).SetDefence(((Classes)classes).GetDefense() * lvl);


             lblAttack.Content = ((Classes)classes).GetChangedDamage() ;
             lblHealth.Content = ((Classes)classes).GetChangedHealth() ;
             lblDefense.Content = ((Classes)classes).GetChangedDefence();
            
        }

        /// <summary>
        /// Sets all health values to max
        /// and also sets a timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblHeal_Click(object sender, RoutedEventArgs e)
        {
            Heal();

        }

        private void Heal()
        {
            if (waitTime <= 0)
            {
                ((Classes)classes).SetHealth(((Classes)classes).GetHealth() * lvl);

                lblHealth.Content = ((Classes)classes).GetChangedHealth();

                waitTime = 8;
                lblWaitTime.Content = "Wait time: " + waitTime;

            }
            else
            {
                lblWaitTime.Content = "Wait time: " + waitTime;
            }
        }


        /// <summary>
        /// Sets damage equal to base damage + weapon damage if an item is selected inside the combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (weaponDamage.Count >= 0 && cmbItem.SelectedIndex >= 0)
            {

                ((Classes)classes).SetDamage(Convert.ToInt32(weaponDamage[cmbItem.SelectedIndex].ToString()) + ((Classes)classes).GetChangedDamage());

                lblAttack.Content = ((Classes)classes).GetChangedDamage() * lvl;
            }


        }

        /// <summary>
        /// I decided to save it in different ways, with both TXT and XML. I save the weapons to the xml-file and all the other game status informations to a textfile
        /// This method takes all information and saves it in different ways.
        /// </summary>
        private void Serialize()
        {

            List<string> saver = new List<string>();
            saver.Add(((Classes)classes).GetChangedDamage().ToString()); //TODO CHANGE TO CLASSES HEALTH AND DMDG 
            saver.Add(((Classes)classes).GetChangedHealth().ToString());
            saver.Add(((Classes)classes).GetChangedDefence().ToString());
            saver.Add(specialWaitTime.ToString());
            saver.Add(waitTime.ToString());

            saver.Add(cmbItem.Items.Count.ToString());
            for (int i = 0; i < cmbItem.Items.Count; i++)
            {
                saver.Add(cmbItem.Items.GetItemAt(i).ToString());
            }

            saver.Add(lvl.ToString());
            saver.Add(experience.ToString());
            saver.Add(gold.ToString());



            saver.Add(((Classes)classes).GetName());
            for (int i = 0; i < listConsole.Items.Count; i++)
            {
                saver.Add(listConsole.Items.GetItemAt(i).ToString());
            }
            
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.ShowDialog();
            string fileName = saveFile.FileName;
            Gamehandler.TextFileSerialize<string>(fileName + ".txt", saver);

            Gamehandler.WriteToXMLFile<List<int>>(fileName + ".xml", weaponDamage);




        }


        /// <summary>
        /// Used when deserializing 
        /// </summary>
        /// <param name="add"></param>
        public void AddToComboBox(string add)
        {
            cmbItem.Items.Add(add);
        }


        /// <summary>
        /// when you close the window it opens a prompt that asks if you want to save.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!save && !isDead)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save before exiting?", "Exit", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Serialize();
                }
            }
        }

        /// <summary>
        /// handles the interactions with the special attack button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BtnSpecial_Click(object sender, RoutedEventArgs e)
        {
            if (specialWaitTime <= 0)
            {
                string attack = ((Classes)classes).GetSpecialAbillity();
                ConsoleMessage("You used the special attack " + attack);
                switch (attack)
                {
                    case "Polymorph":
                        lblEnemy.Content = "Sheep";
                        ((Enemy)enemies).SetDamage(1);
                        lblEnemyAttack.Content = ((Enemy)enemies).GetDamage();
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/GameProject;component/Resources/sheep.jpg", UriKind.Absolute));
                        specialWaitTime = 6;

                        break;
                    case "Retaliate":
                        defence = 100;
                        specialWaitTime = 7;
                        Combat();
                        break;
                    case "Poison":
                        poison = 5;
                        specialWaitTime = 8;

                        break;

                    default:
                        break;
                }
            }
            else
            {
                lblSpecialWaitTime.Content = "Wait time: "+ specialWaitTime;
            }

        }


        /// <summary>
        /// menu item to start new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to start a new game? All unsaved data will be lost.", "Exit", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                save = true;
                Window1 start = new Window1();
                start.Show();
                this.Close();
            }
        }

        /// <summary>
        /// menu item to close game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// menu item to save game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Serialize();
        }


        /// <summary>
        /// handles all the keybinds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.H)
            {
                Heal();
            }
            else if (e.Key == Key.Enter)
            {
                BtnAttack_Click(this, e);
            }
            else if (e.Key == Key.D)
            {
                BtnDefend_Click(this, e);
            }
            else if (e.Key == Key.S)
            {
                BtnSpecial_Click(this, e);
            }

        }
    }
}
