using System.Drawing;
using UIKit;

namespace CRUDApp
{
    public class LoginViewController : UIViewController
    {
        private UILabel _titleLabel;
        private UILabel _loginLabel;
        private UITextField _loginTextField;

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
            _loginLabel.TopAnchor.ConstraintEqualTo(_titleLabel.BottomAnchor, 200f).Active = true;

            _loginTextField = new UITextField
            {
                Placeholder = "Enter user name",
                Font = UIFont.SystemFontOfSize(14),
                TextColor = UIColor.Black,
                TextAlignment = UITextAlignment.Left
            };
            _loginTextField.Layer.BorderWidth = 1.0f;
            _loginTextField.BorderStyle = UITextBorderStyle.RoundedRect;
            _loginTextField.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= 40;
            };
            var paddingView = new UIView(new RectangleF(15,15,15,15));
            _loginTextField.LeftView = paddingView;
            _loginTextField.LeftViewMode = UITextFieldViewMode.Always;
            View.AddSubview(_loginTextField);
            _loginTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            _loginTextField.TopAnchor.ConstraintEqualTo(_loginLabel.BottomAnchor, 5f).Active = true;
            _loginTextField.WidthAnchor.ConstraintEqualTo(View.WidthAnchor, 50f).Active = true;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }
    }
}