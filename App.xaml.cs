using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Firebase.Auth.UI;
using Firebase.Database;

namespace SecretMessage
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider? ServiceProvider { get; set; }
        public App()
        {
            FirebaseUI.Initialize(new FirebaseUIConfig
            {
                ApiKey = "AIzaSyD-4uKG9_0RUTlnZKqMHtF7vo6Etwj1eqk",
                AuthDomain = "secret-message-6a1d7.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new GoogleProvider(),
                    new FacebookProvider(),
                    new GithubProvider()
                },
                PrivacyPolicyUrl = "https://github.com/step-up-labs/firebase-authentication-dotnet",
                TermsOfServiceUrl = "https://github.com/step-up-labs/firebase-database-dotnet",
                AutoUpgradeAnonymousUsers = true,
                UserRepository = new FileUserRepository("FirebaseSample"),
                AnonymousUpgradeConflict = conflict => conflict.SignInWithPendingCredentialAsync(true)
            });
        }

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    var serviceCollection = new ServiceCollection();
        //    serviceCollection.AddTransient<MainWindow>();
        //    serviceCollection.AddScoped<FirebaseClient>();
        //    ServiceProvider = serviceCollection.BuildServiceProvider();
        //    ServiceProvider.GetRequiredService<MainWindow>().Show();

        //}
    }
}
