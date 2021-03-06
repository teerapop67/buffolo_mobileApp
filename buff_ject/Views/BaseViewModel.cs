using buff_ject.Models;
using buff_ject.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace buff_ject.Views
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public static IDataStore<Profile> DataStore => DependencyService.Get<IDataStore<Profile>>();
        public static IDataStoreCollection<Collection> DataStoreCollect => DependencyService.Get<IDataStoreCollection<Collection>>();

        public static IDataStoreRaidBoss<RaidBoss> DataStoreRaidBoss => DependencyService.Get<IDataStoreRaidBoss<RaidBoss>>();

        bool isBusy = false;
      

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
