using System;
using CRUDApp.Data.Entities;
using CRUDApp.Data.Repositories;
using UIKit;

namespace CRUDApp.Controllers
{
    public partial class NoteEditViewController : UIViewController
    {
        private NoteRepository _repository;
        private DataSource _dataSource;

        private UITextView _noteDescriptionTextView;
        private UILabel _noteDescriptionHintLabel;
        private UIButton _toGalleryButton;

        public NoteEditViewController()
        {
        }

        public NoteEditViewController(IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, AddNewItem)
            {
                AccessibilityLabel = "confirmButton"
            };
            View.BackgroundColor = UIColor.White;
            Title = "New note";
            NavigationItem.RightBarButtonItem = addButton;

            #region LabelForEditor

            _noteDescriptionHintLabel = new UILabel
            {
                Text = "Note:",
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.DarkGray,
                Font = UIFont.SystemFontOfSize(16),
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            View.AddSubview(_noteDescriptionHintLabel);
            _noteDescriptionHintLabel.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            _noteDescriptionHintLabel.TopAnchor.ConstraintEqualTo(View.TopAnchor, 80f).Active = true;
            _noteDescriptionHintLabel.WidthAnchor.ConstraintEqualTo(100f).Active = true;
            _noteDescriptionHintLabel.HeightAnchor.ConstraintEqualTo(20f).Active = true;
            #endregion

            #region EditorForDescription

            _noteDescriptionTextView = new UITextView
            {
                Text = "New note",
                Font = UIFont.SystemFontOfSize(16),
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            View.AddSubview(_noteDescriptionTextView);
            _noteDescriptionTextView.LeadingAnchor.ConstraintEqualTo(_noteDescriptionHintLabel.LeadingAnchor).Active = true;
            _noteDescriptionTextView.TopAnchor.ConstraintEqualTo(_noteDescriptionHintLabel.BottomAnchor, 5f).Active = true;
            _noteDescriptionTextView.WidthAnchor.ConstraintEqualTo(View.WidthAnchor).Active = true;
            _noteDescriptionTextView.HeightAnchor.ConstraintEqualTo(300).Active = true;
            #endregion

            #region ToGalleryLink
            _toGalleryButton = new UIButton();
            _toGalleryButton.SetTitle("View gallery", UIControlState.Normal);            
            View.AddSubview(_toGalleryButton);
            _toGalleryButton.TopAnchor.ConstraintEqualTo(_noteDescriptionTextView.BottomAnchor, 10f).Active = true;
            _toGalleryButton.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor).Active = true;
            #endregion
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _toGalleryButton.TouchUpInside += ToGalleryButton_TouchUpInside;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _toGalleryButton.TouchUpInside -= ToGalleryButton_TouchUpInside;
        }

        private void ToGalleryButton_TouchUpInside(object sender, EventArgs e)
        {
            var galleryController = new NoteGalleryViewController(new GalleryCollectionViewLayout());
        }

        private void AddNewItem(object sender, EventArgs args)
        {
            var noteCreationDate = DateTime.Now;
            var note = new Note { Description = _noteDescriptionTextView.Text, CreationDate = noteCreationDate, EditDate = noteCreationDate };
            _repository.Save(note);
            _dataSource.Notes.Add(note);
            NavigationController.PopViewController(true);
        }

        public void SetRepository(NoteRepository repository)
        {
            _repository = repository;
        }

        public void SetDataSource(DataSource dataSource)
        {
            _dataSource = dataSource;
        }
    }
}