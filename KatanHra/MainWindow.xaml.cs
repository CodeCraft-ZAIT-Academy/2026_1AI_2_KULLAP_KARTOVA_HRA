using KatanHra;
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


namespace KatanHra
{
    public partial class MainWindow : Window
    {
        const int Goal = 50;

        Random random = new Random();
        List<Card> cards = new List<Card>();
        int packs;

        public MainWindow()
        {
            InitializeComponent();
            StartGame();
        }

        void StartGame()
        {
            cards.Clear();
            packs = 0;
            lblMessage.Text = "";
            lblCard.Text = "";
            btnOpen.IsEnabled = true;
            Refresh();
        }

        public Card RandomCard()
        {
            int roll = random.Next(100);

            if (roll < 70)
            {
                return new Card.Common();
            }
            else if (roll < 95)
            {
                return new Card.Epic();
            }
            else
            {
                return new Card.Legendary();
            }
        }

        void Refresh()
        {
            int totalPoints = cards.Sum(c => c.Points);

            lblScore.Content = $"Body: {totalPoints} / {Goal}";
            lblPacks.Content = $"Otvorených balíčkov: {packs}";
            lblCard.Text = string.Join("\n", cards.Select(card => card.Icon + " " + card.Name + " (+" + card.Points + ")"));

            if (totalPoints >= Goal)
            {
                btnOpen.IsEnabled = false;
                lblMessage.Text = $"Vyhral si! Stačilo ti {packs} balíčkov.";
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            packs++;
            cards.Add(RandomCard());
            Refresh();
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
    }
}
