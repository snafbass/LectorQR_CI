using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ControlCSA.Models;
using ControlCSA.Views;
using ControlCSA.WS;
using ZXing.Net.Mobile.Forms;
using Android.Content.Res;
using System.Collections.Generic;

namespace ControlCSA.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        private List<AgendaRis> ListaAgenda { get; set; } //Lista para cargar a ListView en el View ItemPage.xaml Agenda RIS
        public List<AgendaRis> AgendaRis { get { return ListaAgenda; } set { ListaAgenda = value; } }
        private List<ReservaClini> ListaAgendaCliniCloud { get; set; }
        public List<ReservaClini> Reservas { get { return ListaAgendaCliniCloud; } set { ListaAgendaCliniCloud = value; } }
        public ItemsViewModel()
        {
            Title = "Consulta Agenda CSA " + DateTime.Today.Year.ToString();
            ListaAgenda = new List<AgendaRis>();

            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }
        public void LoadAgenda(string Rut) //Metodo para Obtener indicadores del WS_indicadores_cama
        {
            WsClient client = new WsClient(); //Instancia la conexion al WS
            try
            {
                List<AgendaRis> lista_original = new List<AgendaRis>();
                List<AgendaRis> lista_view = new List<AgendaRis>();
                lista_original = client.obtenerAgendaRis(Rut);
                foreach (AgendaRis agenda in lista_original)
                {
                    if (agenda.RUT != "")
                    {
                        lista_view.Add(agenda);
                    }
                }
                ListaAgenda = lista_view;//Carga la Lista de los indicadores obtenidos
            }
            catch
            {
                throw;
            }
        }
        public void LoadAgendaCliniCloud(string Rut, string Fecha_ini, string Fecha_fin) //Metodo para Obtener indicadores del WS_indicadores_cama
        {
            WsClient client = new WsClient(); //Instancia la conexion al WS
            try
            {
                List<ReservaClini> lista_original = new List<ReservaClini>();
                List<ReservaClini> lista_view = new List<ReservaClini>();
                //lista_original = client.obtener_indicadores();
                lista_original = client.ObtenerAgendaClinicloud(Rut,Fecha_ini,Fecha_fin);
                foreach (ReservaClini agenda in lista_original)
                {
                    if (agenda.CliId != "")
                    {
                        lista_view.Add(agenda);
                    }
                }
                ListaAgendaCliniCloud = lista_view;//client.obtenerAgenda(); //Carga la Lista de los indicadores obtenidos
                
            }
            catch
            {
                throw;
            }
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}