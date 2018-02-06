namespace Credigas.Popups
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoneyInputCancellableView : ContentView
    {
        private double money = 0.0;


        // public event handler to expose 
        // the Save button's click event
        public EventHandler SaveButtonEventHandler { get; set; }

        // public event handler to expose 
        // the Cancel button's click event
        public EventHandler CancelButtonEventHandler { get; set; }

        // public string to expose the 
        // text Entry input's value
        public double MoneyInputResult { get; set; }

        public static readonly BindableProperty IsValidationLabelVisibleProperty =
            BindableProperty.Create(
                nameof(IsValidationLabelVisible),
                typeof(bool),
                typeof(MoneyInputCancellableView),
                false, BindingMode.OneWay, null,
                (bindable, value, newValue) =>
                {
                    if ((bool)newValue)
                    {
                ((MoneyInputCancellableView)bindable).ValidationLabel
                            .IsVisible = true;
                    }
                    else
                    {
                ((MoneyInputCancellableView)bindable).ValidationLabel
                            .IsVisible = false;
                    }
                });

        /// <summary>
        /// Gets or Sets if the ValidationLabel is visible
        /// </summary>
        public bool IsValidationLabelVisible
        {
            get
            {
                return (bool)GetValue(IsValidationLabelVisibleProperty);
            }
            set
            {
                SetValue(IsValidationLabelVisibleProperty, value);
            }
        }

        public MoneyInputCancellableView(string titleText, string placeHolderText,
            string saveButtonText, string cancelButtonText, string validationText)
        {
            InitializeComponent();

            // update the Element's textual values
            TitleLabel.Text = titleText;
            InputEntry.Placeholder = placeHolderText;
            SaveButton.Text = saveButtonText;
            CancelButton.Text = cancelButtonText;
            ValidationLabel.Text = validationText;

            // handling events to expose to public
            SaveButton.Clicked += SaveButton_Clicked;
            CancelButton.Clicked += CancelButton_Clicked;
            InputEntry.TextChanged += InputEntry_TextChanged;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            // invoke the event handler if its being subscribed
            SaveButtonEventHandler?.Invoke(this, e);
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            // invoke the event handler if its being subscribed
            CancelButtonEventHandler?.Invoke(this, e);
        }

        private void InputEntry_TextChanged(object sender,
            TextChangedEventArgs e)
        {
            // update the public string value 
            // accordingly to the text Entry's value

            double newMoney = 0.0;

            if (double.TryParse(InputEntry.Text, out newMoney))
            {
                money = newMoney;
                MoneyInputResult = newMoney;
            }
            else{
                InputEntry.Text = money.ToString();
                MoneyInputResult = money;
            }
        }
    }
}
