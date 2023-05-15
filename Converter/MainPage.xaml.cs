using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Flurl.Http;

namespace Converter;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
    }
}

public class ValuteItem
{
    public string ID { get; set; }
    public string NumCode { get; set; }
    public string CharCode { get; set; }
    public int Nominal { get; set; }
    public string Name { get; set; }
    public double Value { get; set; }
    public double Previous { get; set; }
    public override string ToString()
    {
        return Name;
    }
}

public class Root
{
    public DateTime Date { get; set; }
    public DateTime PreviousDate { get; set; }
    public string PreviousURL { get; set; }
    public DateTime Timestamp { get; set; }
    public Valute Valute { get; set; }
}

public class Valute
{
    public ValuteItem AUD { get; set; }
    public ValuteItem AZN { get; set; }
    public ValuteItem GBP { get; set; }
    public ValuteItem AMD { get; set; }
    public ValuteItem BYN { get; set; }
    public ValuteItem BGN { get; set; }
    public ValuteItem BRL { get; set; }
    public ValuteItem HUF { get; set; }
    public ValuteItem VND { get; set; }
    public ValuteItem HKD { get; set; }
    public ValuteItem GEL { get; set; }
    public ValuteItem DKK { get; set; }
    public ValuteItem AED { get; set; }
    public ValuteItem USD { get; set; }
    public ValuteItem EUR { get; set; }
    public ValuteItem EGP { get; set; }
    public ValuteItem INR { get; set; }
    public ValuteItem IDR { get; set; }
    public ValuteItem KZT { get; set; }
    public ValuteItem CAD { get; set; }
    public ValuteItem QAR { get; set; }
    public ValuteItem KGS { get; set; }
    public ValuteItem CNY { get; set; }
    public ValuteItem MDL { get; set; }
    public ValuteItem NZD { get; set; }
    public ValuteItem NOK { get; set; }
    public ValuteItem PLN { get; set; }
    public ValuteItem RON { get; set; }
    public ValuteItem XDR { get; set; }
    public ValuteItem SGD { get; set; }
    public ValuteItem TJS { get; set; }
    public ValuteItem THB { get; set; }
    public ValuteItem TRY { get; set; }
    public ValuteItem TMT { get; set; }
    public ValuteItem UZS { get; set; }
    public ValuteItem UAH { get; set; }
    public ValuteItem CZK { get; set; }
    public ValuteItem SEK { get; set; }
    public ValuteItem CHF { get; set; }
    public ValuteItem RSD { get; set; }
    public ValuteItem ZAR { get; set; }
    public ValuteItem KRW { get; set; }
    public ValuteItem JPY { get; set; }
}

class ViewModel : INotifyPropertyChanged
{
    private string _inputValue;
    private ValuteItem _choiceFirstValute;
    private ValuteItem _choiceLastValute;
    private DateTime _choiceDate;

    public string Value
    {
        get => _inputValue;
        set
        {
            if (Equals(value, _inputValue)) return;
            _inputValue = value;
            OnPropertyChanged(nameof(Value));
            OnPropertyChanged(nameof(Transfer));
        }
    }

    public ValuteItem ChoiceFirstValute
    {
        get => _choiceFirstValute;
        set
        {
            if (Equals(value, _choiceFirstValute)) return;
            _choiceFirstValute = value;
            OnPropertyChanged(nameof(ChoiceFirstValute));
            OnPropertyChanged(nameof(Transfer));
        }
    }

    public ValuteItem ChoiceLastValute
    {
        get => _choiceLastValute;
        set
        {
            if (Equals(value, _choiceLastValute)) return;
            _choiceLastValute = value;
            OnPropertyChanged(nameof(ChoiceLastValute));
            OnPropertyChanged(nameof(Transfer));
        }
    }

    public DateTime ChoiceDate
    {
        get => _choiceDate;
        set
        {
            if (Equals(value, _choiceDate)) return;
            _choiceDate = value;
            OnPropertyChanged(nameof(ChoiceDate));
            SetDate();
        }
    }
    public ObservableCollection<ValuteItem> ValuteItems { get; set; } = new ObservableCollection<ValuteItem>();
    public ViewModel()
    {
        ChoiceDate = DateTime.Today;
        Value = "1";
    }

    public string Transfer
    {
        get
        {
            if (ChoiceFirstValute != null && ChoiceLastValute != null && double.TryParse(_inputValue, out var result))
            {
                return ((result * ChoiceFirstValute.Value / ChoiceFirstValute.Nominal) *
                        (ChoiceLastValute.Nominal / ChoiceLastValute.Value)).ToString("#0.000");
            }
            return "";
        }
    }
    private async Task SetDate()
    {
        try
        {
            string dateString = _choiceDate.ToString("yyyy/MM/dd").Replace(".","/");
            var res = await $"https://www.cbr-xml-daily.ru/archive/{dateString}/daily_json.js".GetJsonAsync<Root>();
            ValuteItems.Clear();
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.AUD.Name, Nominal = res.Valute.AUD.Nominal, Value = res.Valute.AUD.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.AZN.Name, Nominal = res.Valute.AZN.Nominal, Value = res.Valute.AZN.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.GBP.Name, Nominal = res.Valute.GBP.Nominal, Value = res.Valute.GBP.Value });
            ValuteItems.Add(new ValuteItem()
                    { Name = res.Valute.AMD.Name, Nominal = res.Valute.AMD.Nominal, Value = res.Valute.AMD.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.BYN.Name, Nominal = res.Valute.BYN.Nominal, Value = res.Valute.BYN.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.BGN.Name, Nominal = res.Valute.BGN.Nominal, Value = res.Valute.BGN.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.BRL.Name, Nominal = res.Valute.BRL.Nominal, Value = res.Valute.BRL.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.HUF.Name, Nominal = res.Valute.HUF.Nominal, Value = res.Valute.HUF.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.VND.Name, Nominal = res.Valute.VND.Nominal, Value = res.Valute.VND.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.HKD.Name, Nominal = res.Valute.HKD.Nominal, Value = res.Valute.HKD.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.GEL.Name, Nominal = res.Valute.GEL.Nominal, Value = res.Valute.GEL.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.DKK.Name, Nominal = res.Valute.DKK.Nominal, Value = res.Valute.DKK.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.AED.Name, Nominal = res.Valute.AED.Nominal, Value = res.Valute.AED.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.USD.Name, Nominal = res.Valute.USD.Nominal, Value = res.Valute.USD.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.EUR.Name, Nominal = res.Valute.EUR.Nominal, Value = res.Valute.EUR.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.EGP.Name, Nominal = res.Valute.EGP.Nominal, Value = res.Valute.EGP.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.INR.Name, Nominal = res.Valute.INR.Nominal, Value = res.Valute.INR.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.IDR.Name, Nominal = res.Valute.IDR.Nominal, Value = res.Valute.IDR.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.KZT.Name, Nominal = res.Valute.KZT.Nominal, Value = res.Valute.KZT.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.CAD.Name, Nominal = res.Valute.CAD.Nominal, Value = res.Valute.CAD.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.QAR.Name, Nominal = res.Valute.QAR.Nominal, Value = res.Valute.QAR.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.KGS.Name, Nominal = res.Valute.KGS.Nominal, Value = res.Valute.KGS.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.CNY.Name, Nominal = res.Valute.CNY.Nominal, Value = res.Valute.CNY.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.MDL.Name, Nominal = res.Valute.MDL.Nominal, Value = res.Valute.MDL.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.NZD.Name, Nominal = res.Valute.NZD.Nominal, Value = res.Valute.NZD.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.NOK.Name, Nominal = res.Valute.NOK.Nominal, Value = res.Valute.NOK.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.PLN.Name, Nominal = res.Valute.PLN.Nominal, Value = res.Valute.PLN.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.RON.Name, Nominal = res.Valute.RON.Nominal, Value = res.Valute.RON.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.XDR.Name, Nominal = res.Valute.XDR.Nominal, Value = res.Valute.XDR.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.SGD.Name, Nominal = res.Valute.SGD.Nominal, Value = res.Valute.SGD.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.TJS.Name, Nominal = res.Valute.TJS.Nominal, Value = res.Valute.TJS.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.THB.Name, Nominal = res.Valute.THB.Nominal, Value = res.Valute.THB.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.TRY.Name, Nominal = res.Valute.TRY.Nominal, Value = res.Valute.TRY.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.TMT.Name, Nominal = res.Valute.TMT.Nominal, Value = res.Valute.TMT.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.UZS.Name, Nominal = res.Valute.UZS.Nominal, Value = res.Valute.UZS.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.UAH.Name, Nominal = res.Valute.UAH.Nominal, Value = res.Valute.UAH.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.CZK.Name, Nominal = res.Valute.CZK.Nominal, Value = res.Valute.CZK.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.SEK.Name, Nominal = res.Valute.SEK.Nominal, Value = res.Valute.SEK.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.CHF.Name, Nominal = res.Valute.CHF.Nominal, Value = res.Valute.CHF.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.RSD.Name, Nominal = res.Valute.RSD.Nominal, Value = res.Valute.RSD.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.ZAR.Name, Nominal = res.Valute.ZAR.Nominal, Value = res.Valute.ZAR.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.KRW.Name, Nominal = res.Valute.KRW.Nominal, Value = res.Valute.KRW.Value });
            ValuteItems.Add(new ValuteItem()
                { Name = res.Valute.JPY.Name, Nominal = res.Valute.JPY.Nominal, Value = res.Valute.JPY.Value });
            ValuteItems.Add(new ValuteItem() { Name = "Российский рубль", Nominal = 1, Value = 1 });
        }
        catch(Exception ex)
        {
            ChoiceDate = ChoiceDate.AddDays(-1);
        }
        ChoiceFirstValute = ValuteItems?[13];
        ChoiceLastValute = ValuteItems?[43];
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}



