﻿using MyEvents.Cloud;
using MyEvents.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MyEvents
{
    public partial class App : Application
    {
        public static IDataManager DataManager = AzureDataManager.DefaultManager;
        public static bool IsMobileServiceConfigured { get; set; } = Constants.ApplicationURL != "https://<yourappservicename>.azurewebsites.net";

        public App()
        {
            InitializeComponent();

            var mainPage = new TabbedPage();
            var sessionsPage = new NavigationPage(new SessionsPage()) { Title = "Sessions" };
            var speakersPage = new NavigationPage(new SpeakersPage()) { Title = "Speakers" };
            var aboutPage = new NavigationPage(new AboutPage()) { Title = "About" };

            Device.OnPlatform(iOS: () => {
                sessionsPage.Icon = "tab_feed.png";
                speakersPage.Icon = "tab_person.png";
                aboutPage.Icon = "tab_about.png";

            });
            mainPage.Children.Add(sessionsPage);
            mainPage.Children.Add(speakersPage);
            mainPage.Children.Add(aboutPage);


            MainPage = mainPage;


        }

        protected async override void OnStart()
        {
            if (!App.IsMobileServiceConfigured)
            {
                await Application.Current.MainPage.DisplayAlert("App not configured", "Edit the Constants.cs file.", "OK");
                return;
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
           
        }
    }
}
