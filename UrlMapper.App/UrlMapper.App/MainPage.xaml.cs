using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UrlMapper.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Matching.Clicked += Matching_Clicked;
        }

        void Matching_Clicked(object sender, EventArgs e)
        {
            var pattern = PatternInput.Text;
            var url = UrlInput.Text;

            var strBuilder = new SimpleStringParameterBuilder();
            var strParam = strBuilder.Parse(pattern);
            var result = strParam.IsMatched(url);
            var dicToStoreResults = new Dictionary<string, string>();
            strParam.ExtractVariables(url, dicToStoreResults);

            Result.Text = result? "Matched" : "Not Match";
            RoutParams.Text = dicToStoreResults.ToString();

        }

    }
}
