using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windguru.Core.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private string _login;
        public string Login
        {
            get { return _login; }
            set { this.RaiseAndSetIfChanged(ref _login, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }

        public ReactiveCommand LoginCommand { get; private set; }

        public MainViewModel()
        {
            Login = "Aloha";

            //var canLogin = this.WhenAnyValue(
            //    vm => vm.Login,
            //    vm => vm.Password,
            //    (l, p) => !string.IsNullOrEmpty(l) && !string.IsNullOrEmpty(p));

            //LoginCommand = ReactiveCommand.Create(() =>
            //{
            //    System.Diagnostics.Debug.WriteLine("LoginCommand");
            //}, canLogin);
        }
    }
}
