using Microsoft.Maui.Dispatching;
using System.Linq;

namespace Memory;

public partial class MainPage : ContentPage
{
    private Dictionary<Button, Card> cardButtons = new Dictionary<Button, Card>();
    private Button lastClickedButton;
    private Button lastLastClickedButton;

    public MainPage()
    {
        InitializeComponent();
        SetUpGrid();
        List<Card> cards = GenerateCards();
        FillGrid(grid, cards);
    }

    private void SetUpGrid()
    {
        for (int i = 0; i < 4; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
        }
    }

    private List<Card> GenerateCards()
    {
        var numbers = new List<int>();
        Random rand = new Random();
        var notAllowedNumbersList = new List<int>();
        for (int i = 0; i < 8; i++)
        {
            var randomNumber = rand.Next(1, 9);
            if (notAllowedNumbersList.Contains(randomNumber))
            {
                i--;
                continue;
            }
            notAllowedNumbersList.Add(randomNumber);
            numbers.Add(randomNumber);
            numbers.Add(randomNumber);
        }

        List<Card> cards = new List<Card>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var randomIndex = rand.Next(0, numbers.Count);
                var newCard = new Card(i, j, numbers[randomIndex]);
                numbers.RemoveAt(randomIndex);
                cards.Add(newCard);
            }
        }
        return cards;
    }

    private void FillGrid(Grid grid, List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards[i];
            var newButton = new Button();
            newButton.Clicked += OnCardButtonClick;
            grid.Add(newButton);
            grid.SetColumn(newButton, card.Column);
            grid.SetRow(newButton, card.Row);
            cardButtons.Add(newButton, card);
        }
    }

    private void OnCardButtonClick(object sender, EventArgs e)
    {
        var clickedButton = sender as Button;
        var card = cardButtons[clickedButton];
        if (!string.IsNullOrEmpty(clickedButton.Text)) return;
        clickedButton.Text = card.CardNumber.ToString();
        if (lastClickedButton != null)
        {
            
            if (lastLastClickedButton != null && !lastLastClickedButton.Text.Equals(""))
            {
                
                if (lastLastClickedButton.Text.Equals(lastClickedButton.Text))
                {
                    lastLastClickedButton = null;
                    lastClickedButton=null;
                }
                else
                {
                    lastLastClickedButton.Text = "";
                    lastClickedButton.Text = "";
                }
            }
            /*if (lastClickedButton.Text.Equals(clickedButton.Text))
            {
                lastClickedButton = clickedButton;
            }
            else
            {
                DisplayAlert("Number", "Clicked number: " + clickedButton.Text, "OK");
                clickedButton.Text = "";
                lastClickedButton.Text = "";
            }*/
            lastLastClickedButton = lastClickedButton;
        }
        
        lastClickedButton = clickedButton;
    }
}

