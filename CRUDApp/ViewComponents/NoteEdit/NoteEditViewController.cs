using System;
using Cirrious.FluentLayouts.Touch;
using CRUDApp.Data.Entities;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.NoteGallery;
using CRUDApp.ViewComponents.Notes;
using UIKit;

namespace CRUDApp.ViewComponents.NoteEdit
{
    public class NoteEditViewController : UIViewController
    {
        private Note _noteEditModel;
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
            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, SaveChanges)
            {
                AccessibilityLabel = ConstantsHelper.ConfirmButtonAccessibilityLabel
            };
            View.BackgroundColor = UIColor.White;
            Title = ConstantsHelper.NewNote;
            NavigationItem.RightBarButtonItem = addButton;

            #region LabelForEditor

            _noteDescriptionHintLabel = new UILabel
            {
                Text = ConstantsHelper.Note,
                TextAlignment = UITextAlignment.Left,
                TextColor = UIColor.DarkGray,
                Font = UIFont.SystemFontOfSize(16),
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            View.AddSubview(_noteDescriptionHintLabel);
            View.AddConstraints(_noteDescriptionHintLabel.AtLeftOf(View, 10f),
                _noteDescriptionHintLabel.AtTopOf(View, 80f),
                _noteDescriptionHintLabel.Width().EqualTo(100f),
                _noteDescriptionHintLabel.Height().EqualTo(20f)
                );
            #endregion

            #region EditorForDescription

            _noteDescriptionTextView = new UITextView
            {
                Text = ConstantsHelper.NewNote,
                Font = UIFont.SystemFontOfSize(16),
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            View.AddSubview(_noteDescriptionTextView);
            View.AddConstraints(
            _noteDescriptionTextView.WithSameLeft(_noteDescriptionHintLabel),
            _noteDescriptionTextView.Below(_noteDescriptionHintLabel, 10f),
            _noteDescriptionTextView.WithSameWidth(View),
            _noteDescriptionTextView.Height().EqualTo(300)
            );
            #endregion

            #region ToGalleryLink
            _toGalleryButton = new UIButton(UIButtonType.Custom)
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            _toGalleryButton.SetTitle(ConstantsHelper.ViewGallery, UIControlState.Normal);
            _toGalleryButton.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            View.AddSubview(_toGalleryButton);
            View.AddConstraints(_toGalleryButton.WithSameLeft(_noteDescriptionTextView),
                _toGalleryButton.AtTrailingOf(View, 10f),
                _toGalleryButton.Below(_noteDescriptionTextView, 50f),
                _toGalleryButton.Height().EqualTo(75f));
            #endregion

            ConfigureView();
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
            NavigationController.PushViewController(galleryController, true);
        }

        private void SaveChanges(object sender, EventArgs args)
        {
            var isEditMode = _noteEditModel == null;
            var noteCreationDate = DateTime.Now;
            if (_noteEditModel == null)
            {
                _noteEditModel = new Note
                {
                    CreationDate = noteCreationDate
                };
            }
            _noteEditModel.Description = _noteDescriptionTextView.Text;
            _noteEditModel.EditDate = noteCreationDate;

            _repository.Save(_noteEditModel);
            if (!isEditMode)
            {
                _dataSource.Notes.Add(_noteEditModel);
            }
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

        public void SetDetailItem(Note note)
        {
            if (_noteEditModel != note)
            {
                _noteEditModel = note;
                ConfigureView();
            }
        }

        private void ConfigureView()
        {
            if (IsViewLoaded && _noteEditModel != null)
            {
                _noteDescriptionTextView.Text = _noteEditModel.Description;
            }
        }
    }
}