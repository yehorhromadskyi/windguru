using ReactiveUI;
using System.Windows.Input;
using System;

namespace Windguru.Core.ViewModels
{
    public class FirstViewModel : ReactiveObject
    {
        string _text;
        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }

        public ICommand ClearCommand { get; private set; }

        public FirstViewModel()
        {
            var canClear = this.WhenAnyValue(vm => vm.Text,
                                             t => !string.IsNullOrEmpty(t));

            ClearCommand = ReactiveCommand.Create(ClearText, canClear);
        }

        private void ClearText()
        {
            Text = "";
        }
    }
}
