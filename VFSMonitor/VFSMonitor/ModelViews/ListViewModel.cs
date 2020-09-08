using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VFSMonitor.Intefaces;
using VFSMonitor.Interfaces;
using VFSMonitor.Models;

namespace VFSMonitor.ModelViews
{
    class ListViewModel : BaseViewModel
    {
        private IMonitorApiGetSessions monitorApiGetSessions;
        private IMonitorApiUserSessions monitorApiUserSessions;
        private List<Session> _SessionsList;
        public List<Session> SessionsList
        {
            get => _SessionsList;
            set => SetProperty(ref _SessionsList, value);
        }
        private List<Session> _UniqueUserSessionsList;
        public List<Session> UniqueUserSessionsList
        {
            get => _UniqueUserSessionsList;
            set => SetProperty(ref _UniqueUserSessionsList, value);
        }
        private bool _IsBusy;
        public bool IsBusy
        {
            get => _IsBusy;
            set => SetProperty(ref _IsBusy, value);
        }
        private bool _BtnVisible;
        public bool BtnVisible
        {
            get => _BtnVisible;
            set => SetProperty(ref _BtnVisible, value);
        }
        private string _Title;
        public string Title
        {
            get => _Title;
            set => SetProperty(ref _Title, value);
        }

        public ListViewModel(string userId)
        {
            GetSessions(userId);
        }

        async void GetSessions(string userId)
        {
            IsBusy = true;
            if (userId == "all")
            {
                BtnVisible = false;
                Title = "SESJE";
                monitorApiGetSessions = RestService.For<IMonitorApiGetSessions>(App.url);
                SessionsList = await monitorApiGetSessions.GetSessions();
            }
            else
            {
                BtnVisible = true;
                Title = userId;
                monitorApiUserSessions = RestService.For<IMonitorApiUserSessions>(App.url);
                SessionsList = await monitorApiUserSessions.GetUserSessions(userId);
            }
            UniqueUserSessionsList = SessionsList.GroupBy(x => x.UserId).Select(x => x.First()).ToList();
            IsBusy = false;

        }

        public string SaveToCSVControl()
        {
            try { if (BtnVisible) SaveToCSV(UniqueUserSessionsList); else SaveToCSV(SessionsList); }
            catch (Exception e) { return "Wystąpił błąd: " + e.Message; }
            return "Eksport zakończony!";
        }

        private void SaveToCSV(List<Session> sessions)
        {
            List<PageObject> pages = new List<PageObject>();
            List<CartItemObject> cartItems = new List<CartItemObject>();
            List<BuyedItemObject> buyedItems = new List<BuyedItemObject>();

            string date = DateTime.Now.ToShortDateString();
            date = date.Replace("/", "_");

            string fileNameMain = Path.Combine(App.path, date+"VeganShopSesje.csv");

            using (var writer = File.CreateText(fileNameMain))
            {
                writer.WriteLine("Id Sesji,Id Użytkownika,Ip Użytkownika,Licznik Wizyt,Data Wizyty,Urządzenie,Przeglądarka,Lokacja,Polecenie,Czy się Kontaktował,Czy Zalogowany");
                foreach(Session ss in sessions)
                {
                    writer.WriteLine($"{ss.SessionId},{ss.UserId},{ss.UserIp},{ss.VisitCounter},{ss.VisitDate}, {ss.Device},{ss.Browser},{ss.Location},{ss.Reffer},{ss.DidContacted},{ss.DidLogged}");

                    foreach (Models.Page page in ss.Pages)
                        pages.Add(new PageObject
                        {
                            SessionId = ss.SessionId,
                            Id = page.Id,
                            Name = page.Name,
                            TimeOn = page.TimeOn
                        });
                    foreach (CartItem cartItem in ss.CartItems)
                        cartItems.Add(new CartItemObject
                        {
                            SessionId = ss.SessionId,
                            Id = cartItem.Id,
                            ItemName = cartItem.ItemName,
                            ItemAction = cartItem.ItemAction
                        });
                    foreach (BuyedItem buyedItem in ss.BuyedItems)
                        buyedItems.Add(new BuyedItemObject
                        {
                            SessionId = ss.SessionId,
                            Id = buyedItem.Id,
                            ItemName = buyedItem.ItemName,
                            ItemQuantity = buyedItem.ItemQuantity
                        });
                }
            }
            SavePagesToCSV(pages);
            SaveCartItemsToCSV(cartItems);
            SaveBuyedItemsToCSV(buyedItems);
        }

        private void SavePagesToCSV(List<PageObject> pages)
        {
            string date = DateTime.Now.ToShortDateString();
            date = date.Replace("/", "_");
            string fileNamePages = Path.Combine(App.path, date+"VeganShopStrony.csv");

            using (var writer = File.CreateText(fileNamePages))
            {
                writer.WriteLine("Id,Id Sesji,Nazwa,Czas na Stronie");
                foreach (PageObject page in pages)
                    writer.WriteLine($"{page.Id},{page.SessionId},{page.Name},{page.TimeOn}");
            }
        }

        private void SaveCartItemsToCSV(List<CartItemObject> cartItems)
        {
            string date = DateTime.Now.ToShortDateString();
            date = date.Replace("/", "_");
            string fileNameCartItems = Path.Combine(App.path, date+"VeganShopKoszyk.csv");

            using (var writer = File.CreateText(fileNameCartItems))
            {
                writer.WriteLine("ItemId,Id Sesji,Nazwa Przedmiotu,Akcja na Przedmiocie");
                foreach (CartItemObject cartItem in cartItems)
                    writer.WriteLine($"{cartItem.Id},{cartItem.SessionId},{cartItem.ItemName},{cartItem.ItemAction}");
            }
        }

        private void SaveBuyedItemsToCSV(List<BuyedItemObject> buyedItems)
        {
            string date = DateTime.Now.ToShortDateString();
            date = date.Replace("/", "_");
            string fileNameBuyedItems = Path.Combine(App.path, date+"VeganShopKupione.csv");

            using (var writer = File.CreateText(fileNameBuyedItems))
            {
                writer.WriteLine("ItemId,Id Sesji,Nazwa Przedmiotu,Ilość");
                foreach (BuyedItemObject buyedItem in buyedItems)
                    writer.WriteLine($"{buyedItem.Id},{buyedItem.SessionId},{buyedItem.ItemName},{buyedItem.ItemQuantity}");
            }
        }
    }
}
