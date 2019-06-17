using System;
using System.Linq;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using CRUDApp.Helpers;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.Login
{
    public partial class LoginView : UIView
    {
        private LoginView _rootView;

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
            _rootView = rootView;
            TitleLabel = rootView.titleLabel;
            LoginButton = rootView.loginButton;
            LoginTextField = rootView.loginTextField;
            PasswordTextField = rootView.passwordTextField;
            ConfirmPasswordLabel = rootView.confirmPasswordLabel;
            ConfirmPasswordTextField = rootView.confirmPasswordTextField;
            RegisterButton = rootView.registerButton;
            QuestionLabel = rootView.questionLabel;

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

        public void StartAnimation()
        {
            LayoutIfNeeded();
            Animate(1.5, () =>
            {
                _rootView.RemoveConstraint(_rootView.Constraints.ElementAt(19));

                this.AddConstraints(RegisterButton.AtBottomOf(this, 50f),
                    QuestionLabel.Above(RegisterButton, 25f));
                LayoutIfNeeded();
            });
        }

        private void SubscribeOnEvents()
        {
            RegisterButton.TouchUpInside += RegisterButton_OnTouchUpInside;
        }

        private void UnsubscribeFromEvents()
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
        public UILabel QuestionLabel { get; private set; }

        private void RegisterButton_OnTouchUpInside(object sender, EventArgs args)
        {
            IsRegisterMode = !IsRegisterMode;

            Animate(0.5, () =>
            {
                if (IsRegisterMode)
                {
                    TitleLabel.Text = ConstantsHelper.Register;

                    ConfirmPasswordLabel.Hidden = false;
                    ConfirmPasswordTextField.Hidden = false;

                    LoginButton.SetTitle(ConstantsHelper.Register, UIControlState.Normal);
                    LoginButton.BackgroundColor = UIColor.FromRGB(111, 201,84);
                    QuestionLabel.Text = ConstantsHelper.AlreadyHaveAccountQuestion;
                    RegisterButton.SetTitle(ConstantsHelper.Login, UIControlState.Normal);
                }
                else
                {
                    TitleLabel.Text = ConstantsHelper.Login;

                    ConfirmPasswordLabel.Hidden = true;
                    ConfirmPasswordTextField.Hidden = true;

                    LoginButton.SetTitle(ConstantsHelper.Login, UIControlState.Normal);
                    LoginButton.BackgroundColor = UIColor.FromRGB(17, 117, 240);
                    QuestionLabel.Text = ConstantsHelper.DontHaveAccountQuestion;
                    RegisterButton.SetTitle(ConstantsHelper.Register, UIControlState.Normal);
                }
            });
        }
    }
}