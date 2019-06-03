using System;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using CRUDApp.Helpers;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.Login
{
    public partial class LoginView : UIView
    {
        public LoginView (IntPtr handle) : base (handle)
        {
        }

        public LoginView()
        {
            InitView();
        }

        private void InitView()
        {
            var arr = NSBundle.MainBundle.LoadNib(nameof(LoginView), this, null);
            var rootView = ObjCRuntime.Runtime.GetNSObject(arr.ValueAt(0)) as LoginView;

            TitleLabel = rootView.titleLabel;
            LoginButton = rootView.loginButton;
            LoginTextField = rootView.loginTextField;
            PasswordTextField = rootView.passwordTextField;
            ConfirmPasswordLabel = rootView.confirmPasswordLabel;
            ConfirmPasswordTextField = rootView.confirmPasswordTextField;
            RegisterButton = rootView.registerButton;

            SubscribeOnEvents();

            AddSubview(rootView);
            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            this.AddConstraints(
                rootView.WithSameTop(this),
                rootView.WithSameBottom(this),
                rootView.WithSameLeft(this),
                rootView.WithSameRight(this));
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            if (ConfirmPasswordLabel != null)
            {
                ConfirmPasswordLabel.Hidden = true;
            }
            if (ConfirmPasswordTextField != null)
            {
                ConfirmPasswordTextField.Hidden = true;
            }
        }

        private void SubscribeOnEvents()
        {
            RegisterButton.TouchUpInside += RegisterButton_OnTouchUpInside;
        }

        private void UnsubscribeOnEvents()
        {
            RegisterButton.TouchUpInside -= RegisterButton_OnTouchUpInside;
        }

        public bool IsRegisterMode { get; private set; }
        public UILabel TitleLabel { get; private set; }
        public UIButton LoginButton { get; private set; }
        public UITextField LoginTextField { get; private set; }
        public UITextField PasswordTextField { get; private set; }
        public UITextField ConfirmPasswordTextField { get; private set; }
        public UILabel ConfirmPasswordLabel { get; private set; }
        public UIButton RegisterButton { get; private set; }

        private void RegisterButton_OnTouchUpInside(object sender, EventArgs args)
        {
            IsRegisterMode = !IsRegisterMode;

            if (IsRegisterMode)
            {
                TitleLabel.Text = ConstantsHelper.Register;

                ConfirmPasswordLabel.Hidden = false;
                ConfirmPasswordTextField.Hidden = false;

                LoginButton.SetTitle(ConstantsHelper.Register, UIControlState.Normal);
                RegisterButton.SetTitle(ConstantsHelper.AlreadyHaveAccountQuestion, UIControlState.Normal);
            }
            else
            {
                TitleLabel.Text = ConstantsHelper.Login;

                ConfirmPasswordLabel.Hidden = true;
                ConfirmPasswordTextField.Hidden = true;

                LoginButton.SetTitle(ConstantsHelper.Login, UIControlState.Normal);
                RegisterButton.SetTitle(ConstantsHelper.DontHaveAccountQuestion, UIControlState.Normal);
            }
        }
    }
}