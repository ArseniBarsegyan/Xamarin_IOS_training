using System;
using UIKit;

namespace CRUDApp
{
    public class LoginViewController : UIViewController
    {
        private bool _isRegisterMode;
        private UILabel _titleLabel;

        private UILabel _loginLabel;
        private UITextField _loginTextField;

        private UILabel _passwordLabel;
        private UITextField _passwordTextField;
        
        private UILabel _confirmPasswordLabel;
        private UITextField _confirmPasswordTextField;

        private UIButton _loginButton;

        private UIButton _registerButton;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;
            _titleLabel = new UILabel
            {
                Text = "Log in",
                TextColor = UIColor.Black,
                Font = UIFont.SystemFontOfSize(20),
                TextAlignment = UITextAlignment.Center
            };
            View.AddSubview(_titleLabel);
            _titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            _titleLabel.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;
            _titleLabel.TopAnchor.ConstraintEqualTo(View.TopAnchor, 50f).Active = true;

            _loginLabel = new UILabel
            {
                Text = "User name:",
                TextColor = UIColor.Black,
                Font = UIFont.SystemFontOfSize(14),
                TextAlignment = UITextAlignment.Center
            };
            View.AddSubview(_loginLabel);
            _loginLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            _loginLabel.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 25f).Active = true;
            _loginLabel.TopAnchor.ConstraintEqualTo(_titleLabel.BottomAnchor, 150f).Active = true;

            _loginTextField = new UITextField
            {
                Placeholder = "Enter user name",
                Font = UIFont.SystemFontOfSize(14),
                TextColor = UIColor.Black,
                TextAlignment = UITextAlignment.Left
            };
            _loginTextField.Layer.BorderWidth = 1.0f;
            _loginTextField.Layer.BorderColor = UIColor.LightGray.CGColor;
            _loginTextField.Layer.CornerRadius = 10.0f;
            _loginTextField.BorderStyle = UITextBorderStyle.RoundedRect;
            _loginTextField.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= 40;
            };
            View.AddSubview(_loginTextField);
            _loginTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            _loginTextField.TopAnchor.ConstraintEqualTo(_loginLabel.BottomAnchor, 5f).Active = true;
            _loginTextField.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 25f).Active = true;
            _loginTextField.RightAnchor.ConstraintEqualTo(View.RightAnchor, -25f).Active = true;

            _passwordLabel = new UILabel
            {
                Text = "Password:",
                TextColor = UIColor.Black,
                Font = UIFont.SystemFontOfSize(14),
                TextAlignment = UITextAlignment.Center
            };
            View.AddSubview(_passwordLabel);
            _passwordLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            _passwordLabel.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 25f).Active = true;
            _passwordLabel.TopAnchor.ConstraintEqualTo(_loginTextField.BottomAnchor, 25f).Active = true;

            _passwordTextField = new UITextField
            {
                Placeholder = "Password...",
                Font = UIFont.SystemFontOfSize(14),
                TextColor = UIColor.Black,
                TextAlignment = UITextAlignment.Left,
                SecureTextEntry = true
            };
            _passwordTextField.Layer.BorderWidth = 1.0f;
            _passwordTextField.Layer.BorderColor = UIColor.LightGray.CGColor;
            _passwordTextField.Layer.CornerRadius = 10.0f;
            _passwordTextField.BorderStyle = UITextBorderStyle.RoundedRect;
            _passwordTextField.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= 40;
            };
            View.AddSubview(_passwordTextField);
            _passwordTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            _passwordTextField.TopAnchor.ConstraintEqualTo(_passwordLabel.BottomAnchor, 5f).Active = true;
            _passwordTextField.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 25f).Active = true;
            _passwordTextField.RightAnchor.ConstraintEqualTo(View.RightAnchor, -25f).Active = true;
            
            _confirmPasswordLabel = new UILabel
            {
                Text = "Confirm password: ",
                TextColor = UIColor.Black,
                Font = UIFont.SystemFontOfSize(14),
                TextAlignment = UITextAlignment.Center,
                Hidden = true
            };
            View.AddSubview(_confirmPasswordLabel);
            _confirmPasswordLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            _confirmPasswordLabel.TopAnchor.ConstraintEqualTo(_passwordTextField.BottomAnchor, 25f).Active = true;
            _confirmPasswordLabel.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 25f).Active = true;
            
            _confirmPasswordTextField = new UITextField
            {
                Placeholder = "Confirm password...",
                Font = UIFont.SystemFontOfSize(14),
                TextColor = UIColor.Black,
                TextAlignment = UITextAlignment.Left,
                SecureTextEntry = true,
                Hidden = true
            };
            _confirmPasswordTextField.Layer.BorderWidth = 1.0f;
            _confirmPasswordTextField.Layer.BorderColor = UIColor.LightGray.CGColor;
            _confirmPasswordTextField.Layer.CornerRadius = 10.0f;
            _confirmPasswordTextField.BorderStyle = UITextBorderStyle.RoundedRect;
            _confirmPasswordTextField.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= 40;
            };
            View.AddSubview(_confirmPasswordTextField);
            _confirmPasswordTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            _confirmPasswordTextField.TopAnchor.ConstraintEqualTo(_confirmPasswordLabel.BottomAnchor, 5f).Active = true;
            _confirmPasswordTextField.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 25f).Active = true;
            _confirmPasswordTextField.RightAnchor.ConstraintEqualTo(View.RightAnchor, -25f).Active = true;
            
            _loginButton = new UIButton();
            _loginButton.SetTitle("Login", UIControlState.Normal);
            _loginButton.BackgroundColor = UIColor.FromRGB(48, 114, 176);
            View.AddSubview(_loginButton);
            _loginButton.TranslatesAutoresizingMaskIntoConstraints = false;
            _loginButton.TopAnchor.ConstraintEqualTo(_confirmPasswordTextField.BottomAnchor, 30f).Active = true;
            _loginButton.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 25f).Active = true;
            _loginButton.RightAnchor.ConstraintEqualTo(View.RightAnchor, -25f).Active = true;

            _registerButton = new UIButton();
            _registerButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            _registerButton.SetTitle("No account? Register", UIControlState.Normal);
            View.AddSubview(_registerButton);
            _registerButton.TranslatesAutoresizingMaskIntoConstraints = false;
            _registerButton.TopAnchor.ConstraintEqualTo(_loginButton.BottomAnchor, 25f).Active = true;
            _registerButton.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 25f).Active = true;
            _registerButton.RightAnchor.ConstraintEqualTo(View.RightAnchor, -25f).Active = true;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _registerButton.TouchUpInside += RegisterButtonOnTouchUpInside;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _registerButton.TouchUpInside -= RegisterButtonOnTouchUpInside;
        }

        private void RegisterButtonOnTouchUpInside(object sender, EventArgs e)
        {
            _isRegisterMode = !_isRegisterMode;

            if (_isRegisterMode)
            {
                _titleLabel.Text = "Register";

                _confirmPasswordLabel.Hidden = false;
                _confirmPasswordTextField.Hidden = false;

                _loginButton.SetTitle("Register", UIControlState.Normal);
                _registerButton.SetTitle("Already have account? Login", UIControlState.Normal);
            }
            else
            {
                _titleLabel.Text = "Login";

                _confirmPasswordLabel.Hidden = true;
                _confirmPasswordTextField.Hidden = true;

                _loginButton.SetTitle("Login", UIControlState.Normal);
                _registerButton.SetTitle("No account? Register", UIControlState.Normal);
            }
        }
    }
}