﻿using buff_ject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using System.ComponentModel;

namespace buff_ject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {

        public int UserStr;
        public int UserAgi;
        public int UserVit;

        public string BossName;
        public int BossHp;
        public string BossUrl;

        static int TurnLimit ;
        static int overall;


        public List<string> ranList = new List<string>();
        static Random rnd = new Random();
        public Page1()
        {
            InitializeComponent();
            addList();
            BindingContext = this;
            
        }
        void addList()
        {
            ranList.Add("Crit");
            ranList.Add("Miss");
            ranList.Add("Normal");
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // User Section
            var GetProfile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);
            UserStr = GetProfile.StrUser;
            UserAgi = GetProfile.AgiUser;
            UserVit = GetProfile.VitUser;
            TurnLimit = GetProfile.turnTime;
            // Boss Section
            pulling();

            // Replace later
            overall = UserAgi + UserVit + UserStr;
            overallStat.Text = overall.ToString();
            limit.Text = TurnLimit.ToString() + " Turn Left";

            // Placing data
            //nameBoss.Text = BossName;
            //healthBoss.Text = BossHp.ToString();
            //BossPic.Source = BossUrl;
        }





        // Attack button

        // Exit Button
        private async void backButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void attackButton_Clicked(object sender, EventArgs e)
        {
            var GetProfile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);
            TurnLimit = GetProfile.turnTime;
            int r = rnd.Next(ranList.Count);
            int damageSet = overall;
            
            if (TurnLimit > 0)
            {
                TurnLimit = TurnLimit - 1;

                limit.Text = TurnLimit.ToString() + " Turn Left";

                Profile UpdateDraw = new Profile()
                {
                    Username = GetProfile.Username,
                    CharactorURL = GetProfile.CharactorURL,
                    NameCharactor = GetProfile.NameCharactor,
                    Password = GetProfile.Password,
                    StrUser = GetProfile.StrUser,
                    AgiUser = GetProfile.AgiUser,
                    VitUser = GetProfile.VitUser,
                    BuffCoin = LoginPage.SetBuffCoins,
                    Email = GetProfile.Email,
                    Id = GetProfile.Id,
                    drawTime = GetProfile.drawTime,
                    turnTime = TurnLimit
                };
                await BaseViewModel.DataStore.UpdateItemAsync(UpdateDraw);

                if (ranList[r] == "Crit")
                {
                    damageSet = overall * 2;
                    BossHp = BossHp - damageSet;

                    RaidBoss update = new RaidBoss()
                    {
                        NameBoss = BossName,
                        HpBoss = BossHp,
                        ImageBoss = BossUrl,
                    };

                    var UpdateBoss = await BaseViewModel.DataStoreRaidBoss.UpdateItemAsyncCollec(update);
                    pulling();
                    //limit.Text = TurnLimit.ToString() + " Turn Left";

                }
                else if (ranList[r] == "Miss")
                {
                    await DisplayAlert("Attack alert", "Miss", "Try again!!");
                    limit.Text = TurnLimit.ToString() + " Turn Left";
                }
                else if (ranList[r] == "Normal")
                {
                    BossHp = BossHp - damageSet;

                    RaidBoss update = new RaidBoss()
                    {
                        NameBoss = BossName,
                        HpBoss = BossHp,
                        ImageBoss = BossUrl,
                    };
                    var UpdateBoss = await BaseViewModel.DataStoreRaidBoss.UpdateItemAsyncCollec(update);
                    pulling();
                    //limit.Text = TurnLimit.ToString() + " Turn Left";
                }
            }
            else
            {
                return;
            }

        }
        private async void pulling()
        {
            var GetBoss = await BaseViewModel.DataStoreRaidBoss.GetItemAsyncCollec("Warwick");
            BossName = GetBoss.NameBoss;
            BossHp = GetBoss.HpBoss;
            BossUrl = GetBoss.ImageBoss;

            nameBoss.Text = BossName;
            healthBoss.Text = BossHp.ToString();
            BossPic.Source = BossUrl;


        }
        private void getTurnTime_Clicked(object sender, EventArgs e)
        {

        }

        // Get more turn button


    }
}