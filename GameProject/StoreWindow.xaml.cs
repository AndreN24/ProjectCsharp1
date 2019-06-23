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


/// <summary>
/// By André Normann
/// C# 2 game project
/// 2019-06-04
/// </summary>
namespace GameProject
{
    /// <summary>
    /// Used as a storewindow
    /// </summary>
    public partial class StoreWindow : Window
    {

        private int price;
        private int damage;
        private bool boughtItem = false;
        Item buyItem;
        public int Gold { get; set; }


        /// <summary>
        /// Initializes listbox
        /// </summary>
        public StoreWindow()
        {
            InitializeComponent();

            string[] items = Enum.GetNames(typeof(Items)); //checks whats inside the enums and fills it with whats in it with a foreach loop
            foreach (var item in items)
                listItems.Items.Add(item);
            lblGold.Content = Gold;
        }

        /// <summary>
        /// Buys an item if gold  > price of item and
        /// only allows for one item to be bought at a time
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (price <= Gold && listItems.SelectedIndex >= 0)
            {
                if (!boughtItem)
                {
                    boughtItem = true;
                    Gold -= price;
                    try
                    {
                        buyItem = new Item(listItems.SelectedItem.ToString(), damage);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Select an item", "error");
                    }
                }
                else
                {
                    MessageBox.Show("You can only buy one item at a time", "error");
                }
            }
            else
            {
                MessageBox.Show("You don't have enough gold to buy this item.", "error");
            }
            lblGold.Content = Gold;

        }

        /// <summary>
        /// Getter so the game window knows what item was bought.
        /// </summary>
        /// <returns></returns>
        public Item GetItem()
        {
            return buyItem;
        }

        /// <summary>
        /// returns to game window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// The prices and damage values for all the items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ListItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Items boughtItem = (Items)Enum.Parse(typeof(Items), listItems.SelectedItem.ToString());
            switch(boughtItem)
            {
                case Items.Basic_Dagger:
                    price = 3000;
                    damage = 5;
                    break;
                case Items.Basic_Sword:
                    price = 1000;
                    damage = 3;
                    break;
                case Items.Basic_Wand:
                    price = 2000;
                    damage = 4;
                    break;
                case Items.Deadly_Broadsword:
                    price = 3000;
                    damage = 5;
                    break;
                case Items.Magical_Staff:
                    price = 5000;
                    damage = 6;
                    break;
                case Items.Poisonous_Dirk:
                    price = 6000;
                    damage = 7;
                    break;
                case Items.Powerful_Staff:
                    price = 13000;
                    damage = 8;
                    break;
                case Items.Sharp_Dagger:
                    price = 20000;
                    damage = 10;
                    break;
                case Items.Strong_Axe:
                    price = 8000;
                    damage = 7;
                    break;
                case Items.Super_Weapon:
                    price = 100000;
                    damage = 100;
                    break;
                default:
                    break;

            }
            lblPrice.Content = price;
            lblItem.Content = listItems.SelectedItem;
            lblDamage.Content = damage;
        }
    }
}
