using System.Text;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.Notes;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.Pin
{
    public class PinViewPresenter
    {
        private readonly StringBuilder _pinBuilder;
        private PinViewController _controller;

        public PinViewPresenter(PinViewController controller)
        {
            _pinBuilder = new StringBuilder();
            _controller = controller;
        }

        public int CurrentCount { get; set; }

        public int PinLength
        {
            get => _pinBuilder.Length;
            set => _pinBuilder.Length = value;
        }

        public void CheckPin(string number)
        {
            CurrentCount++;
            _pinBuilder.Append(number);
        }

        public void DecreasePinLength()
        {
            if (PinLength > 0)
            {
                PinLength--;
            }
        }

        public void DeleteLastPinNumber()
        {
            if (CurrentCount > 0)
            {
                CurrentCount--;
            }
        }

        public void ResetCount()
        {
            _pinBuilder.Length = 0;
            CurrentCount = 0;
        }

        public void Login()
        {
            if (CurrentCount == 4)
            {
                NSUserDefaults preferences = NSUserDefaults.StandardUserDefaults;
                var userPin = preferences.StringForKey(ConstantsHelper.UserPin);

                if (_pinBuilder.ToString() == userPin)
                {
                    NavigateToInitialSection();
                }
            }
        }

        private void NavigateToInitialSection()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            UIStoryboard helloWorldStoryboard = UIStoryboard.FromName(nameof(NotesController), null);
            var initialViewController = helloWorldStoryboard.InstantiateInitialViewController();
            window.RootViewController = new UINavigationController(initialViewController);
        }
    }
}