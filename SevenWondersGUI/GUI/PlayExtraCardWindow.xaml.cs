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
    public partial class PlayExtraCardWindow : Window
    {

        List<Card> cards;
        Card cardToPlay;
        Grid parent;
        PlayerGameBoard view;
        int index = 0;
        PlayerState player;
        ResourceManager rm = ResourceManager.GetInstance();

        public PlayExtraCardWindow(PlayerState p, Grid g, PlayerGameBoard pgb,Card c)
        {
            player = p;
            parent = g;
            view = pgb;
            cardToPlay = c;

            InitializeComponent();

            image.Source = new BitmapImage(
                new Uri(@"pack://application:,,,/Images/" + cardToPlay.getName() + ".jpg", UriKind.RelativeOrAbsolute));
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            if (index >= 0 && index < cards.Count)
            {
                cardToPlay = cards.ElementAt(index);
 

                image.BeginInit();
                image.Source = new BitmapImage(
                    new Uri(@"pack://application:,,,/Images/" + cardToPlay.getName() + ".jpg", UriKind.RelativeOrAbsolute));
                image.EndInit();

                index += 1;
            }
            else
            {
                index = 0;
                cardToPlay = cards.ElementAt(index);

                //image.BeginInit();
                image.Source = new BitmapImage(
                    new Uri(@"pack://application:,,,/Images/" + cardToPlay.getName() + ".jpg", UriKind.RelativeOrAbsolute));
                //image.EndInit();

                index += 1;
            }
        }

        private void buttonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (index >= 0 && index < cards.Count)
            {
                cardToPlay = cards.ElementAt(index);

                image.BeginInit();
                image.Source = new BitmapImage(
                    new Uri(@"pack://application:,,,/Images/" + cardToPlay.getName() + ".jpg", UriKind.RelativeOrAbsolute));
                image.EndInit();

                index -= 1;
            }
            else
            {
                index = (cards.Count - 1);
                cardToPlay = cards.ElementAt(index);

                image.BeginInit();
                image.Source = new BitmapImage(
                    new Uri(@"pack://application:,,,/Images/" + cardToPlay.getName() + ".jpg", UriKind.RelativeOrAbsolute));
                image.EndInit();

                index -= 1;
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            if (rm.ValidateCard(player, cardToPlay))
            {
                player.addPlayedCards(cardToPlay);
                this.Close();
                PlayerGameBoard play = new PlayerGameBoard(parent, player, rm.getGameState());                
            }
            else
            {
                this.Close();
                ResourceManager manager = ResourceManager.GetInstance(rm.getGameState());
                manager.resetResources(player);

                ResourceBuying window = new ResourceBuying(ResourceManager.GetInstance().GetCombinedResources(player), cardToPlay, view, rm.getGameState());
                window.Show();
            }             
        }

        private void buttonSell_Click(object sender, RoutedEventArgs e)
        {
            player.updateCoins(3);
            PlayerGameBoard play = new PlayerGameBoard(parent, player, rm.getGameState());
            this.Close();           
        }

        private void wonder_Click(object sender, RoutedEventArgs e)
        {
            String name = image.Source.ToString();

            String subs = name.Split(',').Last();
            String last = subs.Split('/').Last();

            if (!last.Equals("BackOfWonderCards.png"))//make sure its not the placeholder
            {
                if (player.getPlayedACard() == false && (player.getBoard().getMaxWonderLevel() > player.getWonderCards().Count))
                {
                    if (rm.ValidateWonder(player))
                    {
                        player.getHand().Remove(cardToPlay);//remove from cards in hand of player 

                        player.setWonderCards(cardToPlay);
                        player.getBoard().incrementWonderLevel(player);

                        this.Close();
                        PlayerGameBoard play = new PlayerGameBoard(parent, player, rm.getGameState());
                    }
                    else
                    {
                        this.Close();
                        ResourceManager manager = ResourceManager.GetInstance(rm.getGameState());
                        manager.resetResources(player);

                        WonderBuyingWindow window = new WonderBuyingWindow(ResourceManager.GetInstance().GetCombinedResources(player), cardToPlay, view, rm.getGameState());
                        window.Show();
                    }           
                }            
            }
        }




    }
}
