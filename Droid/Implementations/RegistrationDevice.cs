﻿using Xamarin.Forms;

[assembly: Dependency(typeof(Credigas.Droid.Implementations.RegistrationDevice))]

namespace Credigas.Droid.Implementations
{
    using Android.Util;
    //using Gcm.Client;
    using Interfaces;

    public class RegistrationDevice : IRegisterDevice
    {
        #region Methods
        public void RegisterDevice()
        {
            //var mainActivity = MainActivity.GetInstance();
            //GcmClient.CheckDevice(mainActivity);
            //GcmClient.CheckManifest(mainActivity);

            Log.Info("MainActivity", "Registering...");
            //GcmClient.Register(mainActivity, Droid.Constants.SenderID);
        }
        #endregion
    }
}
