using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Refit;
using System;
using VFSMonitor.Intefaces;
using VFSMonitor.Models;

namespace VFSMonitor.ModelViews
{
    class AverageViewModel : BaseViewModel
    {
        private IMonitorApiGlobal monitorApiGlobal;
        private IMonitorApiUserAverage monitorApiUserAverage;

        public AverageViewModel(string userId)
        {
            GetGlobal(userId);
        }

        async void GetGlobal(string userId)
        {
            IsBusy = true;
            IsVisible = false;
            if (userId == "all")
            {
                Title = "GLOBALNE";
                IsExtraDataVisible = false;
                monitorApiGlobal = RestService.For<IMonitorApiGlobal>(App.url);
                AverageData = await monitorApiGlobal.GetGlobal();
                MostUsedDevice = AverageData.MostUsedDevice;
                MostUsedBrowser = AverageData.MostUsedBrowser;
                MostPopularLocation = AverageData.MostPopularLocation;
                MostPopularReffer = AverageData.MostPopularReffer;
                AverageTimeOnPages = AverageData.AverageTimeOnPages;
                AvItemBuy = AverageData.AvItemBuy;
                MostlyLogged = AverageData.MostlyLogged;
            }
            else
            {
                Title = userId;
                IsExtraDataVisible = true;
                monitorApiUserAverage = RestService.For<IMonitorApiUserAverage>(App.url);
                UserAverageData = await monitorApiUserAverage.GetUserAverage(userId);
                UserId = UserAverageData.UserId;
                UserIp = UserAverageData.UserIp;
                MostUsedDevice = UserAverageData.MostUsedDevice;
                MostUsedBrowser = UserAverageData.MostUsedBrowser;
                MostPopularLocation = UserAverageData.MostPopularLocation;
                MostPopularReffer = UserAverageData.MostPopularReffer;
                AverageTimeOnPages = UserAverageData.AverageTimeOnPages;
                AverageCartAction = UserAverageData.AvCartAction;
                AvItemBuy = UserAverageData.AvItemBuy;
                MostlyLogged = UserAverageData.MostlyLogged;
            }
            IsVisible = false;
            IsBusy = false;
        }

        public string SaveToExcelControl()
        {
            try { SaveToExcel(); }
            catch (Exception e) { return "Wystąpił błąd: " + e.Message; }
            return "Eksport zakończony!";
        }

        private void SaveToExcel()
        {
            string date = DateTime.Now.ToShortDateString();
            date = date.Replace("/", "_");
            string fileName;
            if (IsExtraDataVisible) fileName = System.IO.Path.Combine(App.path, date + "VeganShopSrednia" + UserId + "Detale.xlsx");
            else fileName = System.IO.Path.Combine(App.path, date + "VeganShopGlobalnaSredniaDetale.xlsx");

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorksheetPart worksheetPart;
                if(IsExtraDataVisible) worksheetPart = createWorkSheet(document, UserId + "Srednia");
                else worksheetPart = createWorkSheet(document, "GlobalnaSrednia");
                
                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                Row Row;

                if(IsExtraDataVisible)
                {
                    Row = new Row();
                    Row.Append(
                        ConstructCell("Id Użytkownika", CellValues.String),
                        ConstructCell(UserId, CellValues.String));
                    sheetData.AppendChild(Row);

                    Row = new Row();
                    Row.Append(
                        ConstructCell("Ip Użytkownika", CellValues.String),
                        ConstructCell(UserIp, CellValues.String));
                    sheetData.AppendChild(Row);

                    Row = new Row();
                    Row.Append(
                        ConstructCell("Średnie akcje w koszyku", CellValues.String),
                        ConstructCell(AverageCartAction, CellValues.String));
                    sheetData.AppendChild(Row);
                }

                Row = new Row();
                Row.Append(
                    ConstructCell("Najpopularniesze Urządzenie", CellValues.String),
                    ConstructCell(MostUsedDevice, CellValues.String));
                sheetData.AppendChild(Row);
                Row = new Row();
                Row.Append(
                    ConstructCell("Najpopularniejsza Przeglądarka", CellValues.String),
                    ConstructCell(MostUsedBrowser, CellValues.String));
                sheetData.AppendChild(Row);
                Row = new Row();
                Row.Append(
                    ConstructCell("Najpopularniejsza Lokacja", CellValues.String),
                    ConstructCell(MostPopularLocation, CellValues.String));
                sheetData.AppendChild(Row);
                Row = new Row();
                Row.Append(
                    ConstructCell("Najpopularniejsze Polecenie", CellValues.String),
                    ConstructCell(MostPopularReffer, CellValues.String));
                sheetData.AppendChild(Row);
                Row = new Row();
                Row.Append(
                    ConstructCell("Średni Czas na Stronach", CellValues.String),
                    ConstructCell(AverageTimeOnPages.ToString(), CellValues.String));
                sheetData.AppendChild(Row);
                Row = new Row();
                Row.Append(
                    ConstructCell("Średnia Ilość Kupionych Przedmiotów", CellValues.String),
                    ConstructCell(AvItemBuy.ToString(), CellValues.String));
                sheetData.AppendChild(Row);
                Row = new Row();
                Row.Append(
                    ConstructCell("Przeważnie Zalogowany", CellValues.String),
                    ConstructCell(MostlyLogged.ToString(), CellValues.String));
                sheetData.AppendChild(Row);

                worksheetPart.Worksheet.Save();
            }
        }

        private string _Title;
        public string Title
        {
            get => _Title;
            set => SetProperty(ref _Title, value);
        }
        private GlobalAverageData _AverageData;
        public GlobalAverageData AverageData
        {
            get => _AverageData;
            set => SetProperty(ref _AverageData, value);
        }
        private UserAverageData _UserAverageData;
        public UserAverageData UserAverageData
        {
            get => _UserAverageData;
            set => SetProperty(ref _UserAverageData, value);
        }
        private bool _IsBusy;
        public bool IsBusy
        {
            get => _IsBusy;
            set => SetProperty(ref _IsBusy, value);
        }
        private bool _IsVisible;
        public bool IsVisible
        {
            get => _IsVisible;
            set => SetProperty(ref _IsVisible, value);
        }

        private string _UserId;
        public string UserId
        {
            get => _UserId;
            set => SetProperty(ref _UserId, value);
        }
        private string _UserIp;
        public string UserIp
        {
            get => _UserIp;
            set => SetProperty(ref _UserIp, value);
        }
        private string _MostUsedDevice;
        public string MostUsedDevice
        {
            get => _MostUsedDevice;
            set => SetProperty(ref _MostUsedDevice, value);
        }
        private string _MostUsedBrowser;
        public string MostUsedBrowser
        {
            get => _MostUsedBrowser;
            set => SetProperty(ref _MostUsedBrowser, value);
        }
        private string _MostPopularLocation;
        public string MostPopularLocation
        {
            get => _MostPopularLocation;
            set => SetProperty(ref _MostPopularLocation, value);
        }
        private string _MostPopularReffer;
        public string MostPopularReffer
        {
            get => _MostPopularReffer;
            set => SetProperty(ref _MostPopularReffer, value);
        }
        private decimal _AverageTimeOnPages;
        public decimal AverageTimeOnPages
        {
            get => _AverageTimeOnPages;
            set => SetProperty(ref _AverageTimeOnPages, value);
        }
        private string _AverageCartAction;
        public string AverageCartAction
        {
            get => _AverageCartAction;
            set => SetProperty(ref _AverageCartAction, value);
        }
        private decimal _AvItemBuy;
        public decimal AvItemBuy
        {
            get => _AvItemBuy;
            set => SetProperty(ref _AvItemBuy, value);
        }
        private bool _MostlyLogged;
        public bool MostlyLogged
        {
            get => _MostlyLogged;
            set => SetProperty(ref _MostlyLogged, value);
        }
        private bool _IsExtraDataVisible;
        public bool IsExtraDataVisible
        {
            get => _IsExtraDataVisible;
            set => SetProperty(ref _IsExtraDataVisible, value);
        }
    }
}
