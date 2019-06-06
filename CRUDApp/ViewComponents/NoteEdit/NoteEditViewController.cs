using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using CRUDApp.Data.Entities;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.NoteEdit.NoteGallery;
using CRUDApp.ViewComponents.Notes;
using Foundation;
using GMImagePicker;
using UIKit;

namespace CRUDApp.ViewComponents.NoteEdit
{
    public class NoteEditViewController : UIViewController
    {
        private UIImagePickerController _picker;

        private Note _noteEditModel;
        private NoteRepository _repository;
        private NotesDataSource _notesDataSource;

        private UITextView _noteDescriptionTextView;
        private UILabel _noteDescriptionHintLabel;

        private UILabel _galleryHintLabel;

        private UIImageView _pickImage;
        private UIImageView _cameraImage;
        private UIImageView _videoImage;

        private UITapGestureRecognizer _pickTapGestureRecognizer;
        private UITapGestureRecognizer _cameraTapGestureRecognizer;
        private UITapGestureRecognizer _videoTapGestureRecognizer;

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
            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.NewNote, ConstantsHelper.NewNote);
            NavigationItem.RightBarButtonItem = addButton;

            _noteDescriptionHintLabel = new UILabel
            {
                Text = ConstantsHelper.Note,
                TextAlignment = UITextAlignment.Left,
                TextColor = UIColor.DarkGray,
                Font = UIFont.SystemFontOfSize(14),
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            View.AddSubview(_noteDescriptionHintLabel);
            View.AddConstraints(_noteDescriptionHintLabel.AtLeftOf(View, 10f),
                _noteDescriptionHintLabel.AtTopOf(View, 80f),
                _noteDescriptionHintLabel.Width().EqualTo(100f),
                _noteDescriptionHintLabel.Height().EqualTo(20f));

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
            _noteDescriptionTextView.Height().EqualTo(200));

            ConfigureView();

            _galleryHintLabel = new UILabel
            {
                Text = ConstantsHelper.Gallery,
                TextAlignment = UITextAlignment.Left,
                TextColor = UIColor.DarkGray,
                Font = UIFont.SystemFontOfSize(14),
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            View.AddSubview(_galleryHintLabel);
            View.AddConstraints(_galleryHintLabel.AtLeftOf(View, 10f),
                _galleryHintLabel.Below(_noteDescriptionTextView, 10f),
                _galleryHintLabel.Width().EqualTo(100f),
                _galleryHintLabel.Height().EqualTo(20f));

            var collectionView = new UICollectionView(CGRect.Empty, new GalleryCollectionViewLayout())
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            View.AddSubview(collectionView);

            collectionView.BackgroundColor = UIColor.White;
            collectionView.AlwaysBounceVertical = true;
            collectionView.Bounces = true;
            collectionView.RegisterClassForCell(typeof(GalleryViewCell), nameof(GalleryViewCell));
            collectionView.Source = new GalleryCollectionViewSource(new List<GalleryItemModel>
            {
                new GalleryItemModel
                {
                    ImagePath = "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
                },
                new GalleryItemModel
                {
                    ImagePath = "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
                },
                new GalleryItemModel
                {
                    ImagePath = "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
                },
                new GalleryItemModel
                {
                    ImagePath = "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
                },
                new GalleryItemModel
                {
                    ImagePath = "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
                },
                new GalleryItemModel
                {
                    ImagePath = "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
                },
                new GalleryItemModel
                {
                    ImagePath = "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
                },
                new GalleryItemModel
                {
                    ImagePath = "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
                },
                new GalleryItemModel
                {
                    ImagePath = "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
                },
                new GalleryItemModel
                {
                    ImagePath = "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
                }
            });
            collectionView.Delegate = new GalleryCollectionViewDelegate(new UIEdgeInsets(5, 5, 5, 5));
            collectionView.ReloadData();

            View.AddConstraints(collectionView.Below(_galleryHintLabel, 30f),
                collectionView.WithSameWidth(View),
                collectionView.Below(_galleryHintLabel, 5f),
                collectionView.AtBottomOf(View, 70f));

            var bottomBar = new UIView
            {
                BackgroundColor = UIColor.FromRGB(50, 50, 50),
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            _pickImage = new UIImageView(UIImage.FromBundle(ConstantsHelper.AddIcon))
            {
                ContentMode = UIViewContentMode.Center,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            _cameraImage = new UIImageView(UIImage.FromBundle(ConstantsHelper.CameraIcon))
            {
                ContentMode = UIViewContentMode.Center,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            _videoImage = new UIImageView(UIImage.FromBundle(ConstantsHelper.VideoIcon))
            {
                ContentMode = UIViewContentMode.Center,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            bottomBar.AddSubviews(_pickImage, _cameraImage, _videoImage);
            bottomBar.AddConstraints(_pickImage.Height().EqualTo(70f),
                _pickImage.AtLeftOf(bottomBar),
                _pickImage.Width().EqualTo(70f),
                _pickImage.WithSameCenterY(bottomBar),
                _cameraImage.ToRightOf(_pickImage),
                _cameraImage.Height().EqualTo(70f),
                _cameraImage.Width().EqualTo(70f),
                _cameraImage.WithSameCenterY(bottomBar),
                _videoImage.ToRightOf(_cameraImage),
                _videoImage.Height().EqualTo(70f),
                _videoImage.Width().EqualTo(70f),
                _videoImage.WithSameCenterY(bottomBar));

            View.AddSubview(bottomBar);
            View.AddConstraints(bottomBar.WithSameWidth(View),
                bottomBar.Height().EqualTo(70f),
                bottomBar.AtBottomOf(View));
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _pickImage.UserInteractionEnabled = true;
            _cameraImage.UserInteractionEnabled = true;
            _videoImage.UserInteractionEnabled = true;
            _pickTapGestureRecognizer = new UITapGestureRecognizer(async() =>
            {
                AnimateButton(_pickImage);
                await PickMultipleImages();
            }) { NumberOfTapsRequired = 1 };
            _cameraTapGestureRecognizer = new UITapGestureRecognizer(async() =>
            {
                AnimateButton(_cameraImage);
                await TakePhoto();
            }) { NumberOfTapsRequired = 1 };
            _videoTapGestureRecognizer = new UITapGestureRecognizer(() =>
            {
                AnimateButton(_videoImage);
            }) {NumberOfTapsRequired = 1};

            _pickImage.AddGestureRecognizer(_pickTapGestureRecognizer);
            _cameraImage.AddGestureRecognizer(_cameraTapGestureRecognizer);
            _videoImage.AddGestureRecognizer(_videoTapGestureRecognizer);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _pickImage.RemoveGestureRecognizer(_pickTapGestureRecognizer);
            _cameraImage.RemoveGestureRecognizer(_cameraTapGestureRecognizer);
            _videoImage.RemoveGestureRecognizer(_videoTapGestureRecognizer);
        }

        private async Task TakePhoto()
        {
            if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
            {
                _picker = new UIImagePickerController
                {
                    Delegate = new CameraDelegate(),
                    SourceType = UIImagePickerControllerSourceType.Camera
                };
                await PresentViewControllerAsync(_picker, true);
            }
            else
            {
                var okAlertController = UIAlertController.Create(ConstantsHelper.CameraNotAvailableError, ConstantsHelper.CameraNotAvailableMessage,
                    UIAlertControllerStyle.Alert);
                okAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Ok, UIAlertActionStyle.Default, null));
                await PresentViewControllerAsync(okAlertController, true);
            }
        }

        private async Task PickMultipleImages()
        {
            var picker = new GMImagePickerController();
            picker.FinishedPickingAssets += (sender, args) =>
            {
                var photoAssets = args.Assets;

                Console.WriteLine("Finished picking");
            };
            await PresentViewControllerAsync(picker, true);
        }

        private void AnimateButton(UIImageView image)
        {
            UIView.Animate(0.3, () => { image.Layer.BackgroundColor = UIColor.FromRGB(68, 138, 255).CGColor; });
            UIView.Animate(0.15, () => { image.Layer.BackgroundColor = UIColor.Clear.CGColor; });
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
                _notesDataSource.Notes.Add(_noteEditModel);
            }
            NavigationController.PopViewController(true);
        }

        public void SetRepository(NoteRepository repository)
        {
            _repository = repository;
        }

        public void SetDataSource(NotesDataSource notesDataSource)
        {
            _notesDataSource = notesDataSource;
        }

        public void SetDetailItem(Note note)
        {
            if (_noteEditModel != note)
            {
                _noteEditModel = note;
                ConfigureView();
            }
        }

        private void ResetButtons()
        {
            _pickImage.BackgroundColor = UIColor.Clear;
            _cameraImage.BackgroundColor = UIColor.Clear;
            _videoImage.BackgroundColor = UIColor.Clear;
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