using buff_ject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace buff_ject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FightPage : ContentPage
    {

        public int UserStr;
        public int UserAgi;
        public int UserVit;
        public int UserScore;

        public string BossName;
        public int BossHp;
        public string BossUrl;

        static int TurnLimit ;
        static int overall;


        public List<string> ranList = new List<string>();
        static Random rnd = new Random();
        public FightPage()
        {
            InitializeComponent();
            addList();
            BindingContext = this;
            
        }

        Stream GetStreamFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("buff_ject."+filename);
            return stream;
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
            UserScore = GetProfile.score;
            // Boss Section
            pulling();

            // Replace later
            overall = UserAgi + UserVit + UserStr;
            buff.Text = $"{LoginPage.SetBuffCoins} BUFF";
            limit.Text = TurnLimit.ToString() + " Turn Left";
            score.Text = "Total cb: " + UserScore.ToString();
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

            var stream = GetStreamFile("atk.mp3");
            var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            audio.Load(stream);
            audio.Play();

            if (BossHp > 0)
            {
                if (TurnLimit > 0)
                {
                    TurnLimit = TurnLimit - 1;

                    limit.Text = TurnLimit.ToString() + " Turn Left";

                    if (ranList[r] == "Crit")
                    {
                        damageSet = overall * 2;
                        
                        BossHp = BossHp - damageSet;
                        if (BossHp < 0)
                        {
                            BossHp = 0;
                        }
                        RaidBoss update = new RaidBoss()
                        {
                            NameBoss = BossName,
                            HpBoss = BossHp,
                            ImageBoss = BossUrl,
                        };

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
                            turnTime = TurnLimit,
                            score = GetProfile.score + damageSet
                        };
                        await BaseViewModel.DataStore.UpdateItemAsync(UpdateDraw);

                        score.Text = "Total cb: " + UpdateDraw.score.ToString();
                        var UpdateBoss = await BaseViewModel.DataStoreRaidBoss.UpdateItemAsyncCollec(update);
                        pulling();
                        animation();
                        //limit.Text = TurnLimit.ToString() + " Turn Left";

                    }
                    else if (ranList[r] == "Miss")
                    {
                        await DisplayAlert("Attack alert", "Miss", "Try again!!");
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
                            turnTime = TurnLimit,
                            score = GetProfile.score
                        };
                        await BaseViewModel.DataStore.UpdateItemAsync(UpdateDraw);

                        score.Text = "Total cb: " + UpdateDraw.score.ToString();

                        limit.Text = TurnLimit.ToString() + " Turn Left";
                    }
                    else if (ranList[r] == "Normal")
                    {
                        BossHp = BossHp - damageSet;
                        if (BossHp < 0)
                        {
                            BossHp = 0;
                        }
                        RaidBoss update = new RaidBoss()
                        {
                            NameBoss = BossName,
                            HpBoss = BossHp,
                            ImageBoss = BossUrl,
                        };
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
                            turnTime = TurnLimit,
                            score = GetProfile.score + damageSet
                        };
                        await BaseViewModel.DataStore.UpdateItemAsync(UpdateDraw);

                        score.Text = "Total cb: " + UpdateDraw.score.ToString();
                        var UpdateBoss = await BaseViewModel.DataStoreRaidBoss.UpdateItemAsyncCollec(update);
                        pulling();
                        animation();
                        //limit.Text = TurnLimit.ToString() + " Turn Left";
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                await DisplayAlert("Raid Status", "Boss has been eliminated. Wait for update and reward.", "OK");
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
        private async void getTurnTime_Clicked(object sender, EventArgs e)
        {
            var Getprofile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);
            //int currentBuff = Getprofile.BuffCoin;
            LoginPage.SetBuffCoins = Getprofile.BuffCoin;
            if (LoginPage.SetBuffCoins >= 5)
            {
                LoginPage.SetBuffCoins -= 5;
                Profile UpdateDraw = new Profile()
                {
                    Username = Getprofile.Username,
                    CharactorURL = Getprofile.CharactorURL,
                    NameCharactor = Getprofile.NameCharactor,
                    Password = Getprofile.Password,
                    StrUser = Getprofile.StrUser,
                    AgiUser = Getprofile.AgiUser,
                    VitUser = Getprofile.VitUser,
                    BuffCoin = LoginPage.SetBuffCoins,
                    Email = Getprofile.Email,
                    Id = Getprofile.Id,
                    drawTime = Getprofile.drawTime,
                    turnTime = Getprofile.turnTime + 1,
                    score = Getprofile.score,
                };
                buff.Text = $"{LoginPage.SetBuffCoins} BUFF";
                limit.Text = UpdateDraw.turnTime.ToString() + " Draw Left";
                await BaseViewModel.DataStore.UpdateItemAsync(UpdateDraw);

            }
            else
            {
                await DisplayAlert("Alert", "Need 5 coin", "OK");
            }
        }

        // Get more turn button
        
        async void checkdied()
        {
            var getBoss = await BaseViewModel.DataStoreRaidBoss.GetItemAsyncCollec("Warwick");
            if (getBoss.HpBoss > 0)
            {
                return;
            }
            else
            {
                await DisplayAlert("Raid Status", "Boss has been eliminated. Wait for update and reward.", "OK");
            }
                    
        }

        async void animation()
        {
            BossPic.Opacity = 0;
            await BossPic.FadeTo(4, 4000);
        }
    }
}