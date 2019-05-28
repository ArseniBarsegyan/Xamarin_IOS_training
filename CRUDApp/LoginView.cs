using System;
using UIKit;

namespace CRUDApp
{
    public partial class LoginView : UIView
    {
        public LoginView (IntPtr handle) : base (handle)
        {
        }

        public bool IsRegisterMode { get; private set; }
        internal UIButton LoginButton => loginButton;
        internal UITextField LoginTextField => loginTextField;
        internal UITextField PasswordTextField => passwordTextField;
        internal UITextField ConfirmPasswordTextField => confirmPasswordTextField;

        partial void RegisterButton_OnTouchUpInside(UIButton sender)
        {
            IsRegisterMode = !IsRegisterMode;

            if (IsRegisterMode)
            {
                titleLabel.Text = "Register";

                confirmPasswordLabel.Hidden = false;
                confirmPasswordTextField.Hidden = false;

                LoginButton.SetTitle("Register", UIControlState.Normal);
                registerButton.SetTitle("Already have account? Login", UIControlState.Normal);
            }
            else
            {
                titleLabel.Text = "Login";

                confirmPasswordLabel.Hidden = true;
                confirmPasswordTextField.Hidden = true;

                LoginButton.SetTitle("Login", UIControlState.Normal);
                registerButton.SetTitle("No account? Register", UIControlState.Normal);
            }
        }
    }
}