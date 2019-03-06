using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SevenWondersGUI
{
    /// <summary>
    /// Interaction logic for DiscardsWindow.xaml
    /// </summary>
    public partial class DiscardsWindow : Window
    {
        
        List<Card> discards;
        Card cardToPlay;
        Grid parent;
        int  index = 0;
        PlayerState player;
        ResourceManager rm = ResourceManager.GetInstance();

        public DiscardsWindow(PlayerState p, Grid g)
        {
            player = p;
            parent = g;
            discards = rm.getGameState().getDiscards();

            InitializeComponent();            
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            if (index >= 0 && index < discards.Count)
            {
                Card c = discards.ElementAt(index);
                cardToPlay = c;

                image.BeginInit();
                image.Source = new BitmapImage(
                    new Uri(@"pack://application:,,,/Images/" + c.getName() + ".jpg", UriKind.RelativeOrAbsolute));
                image.EndInit();

                index+=1;
            }
            else {
                index = 0;
                Card c = discards.ElementAt(index);
                cardToPlay = c;

                image.BeginInit();
                image.Source = new BitmapImage(
                    new Uri(@"pack://application:,,,/Images/" + c.getName() + ".jpg", UriKind.RelativeOrAbsolute));
                image.EndInit();

                index+=1;                  
            }           
        }

        private void buttonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (index >= 0 && index < discards.Count )
            {
                Card c = discards.ElementAt(index);
                cardToPlay = c;
                image.BeginInit();
                image.Source = new BitmapImage(
                    new Uri(@"pack://application:,,,/Images/" + c.getName() + ".jpg", UriKind.RelativeOrAbsolute));
                image.EndInit();

                index-=1;
            }
            else {             
                index = (discards.Count - 1);
                Card c = discards.ElementAt(index);
                cardToPlay = c;
                image.BeginInit();
                image.Source = new BitmapImage(
                    new Uri(@"pack://application:,,,/Images/" + c.getName() + ".jpg", UriKind.RelativeOrAbsolute));
                image.EndInit();

                index-=1;
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            String name = image.Source.ToString();
           
            String subs = name.Split(',').Last();
            String last = subs.Split('/').Last();

            if (!last.Equals("BackOfWonderCards.png"))//make sure its not the placeholder
            {
                player.addPlayedCards(cardToPlay);//add to the playerState                               
                discards.Remove(cardToPlay);//remove the card from discards

                rm.getGameState().incrementTurn();
                this.Close();
                //now redraw the window
                PlayerGameBoard play = new PlayerGameBoard(parent, player, rm.getGameState());
            }
        }

    }
}
