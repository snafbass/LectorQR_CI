using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ControlCSA.Models;
using ControlCSA.Views;
using ControlCSA.ViewModels;
using ZXing.Net.Mobile.Forms;
using Plugin.Connectivity;

namespace ControlCSA.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Scanner();  
            }
            else
            {
                DisplayAlert("Conexión a internet", "Su dispositivo no está conectado a internet, revise su conexión y vuelva a intentarlo", "Ok");
            }
            
        }
        async void Scanner()
        {
            var ScannerPage = new ZXingScannerPage();
            ScannerPage.Title = "Leer Cédula de ID";
            ScannerPage.OnScanResult += (result) =>
            {

                ScannerPage.IsScanning = true;

                Device.BeginInvokeOnMainThread(()=>
                {
                    try
                    {
                        Navigation.PopAsync();
                        var url = result.Text;
                        var inicio = url.IndexOf("RUN=") + 4;
                        var fin = url.IndexOf("&type");
                        var largo = fin - inicio;
                        var rut = url.Substring(inicio, largo);
                        viewModel.LoadAgendaCliniCloud(rut, DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                        labelrut.Text = "Rut: "+rut;
                        listviewclinicloud.ItemsSource = viewModel.Reservas;
                        viewModel.LoadAgenda(rut);
                        listviewris.ItemsSource = viewModel.AgendaRis;
                        if((viewModel.Reservas.Count != 0) || (viewModel.AgendaRis.Count != 0))
                        {
                            DisplayAlert("Agenda Paciente", "Rut: " + rut.ToString(), "Ok");
                        }
                        else 
                        {
                            DisplayAlert("Alerta - Agenda Paciente", "Rut: " + rut.ToString() +" No tiene Agenda para Hoy", "Ok");
                        }
                        
                        
                        
                    }
                    catch
                    { 
                    
                    }

                });
            };
            await Navigation.PushAsync(ScannerPage);
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }

    }
}