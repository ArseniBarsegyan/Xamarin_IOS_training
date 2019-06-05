using System;
using System.Collections.Generic;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using CRUDApp.Data.Entities;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.NoteEdit.NoteGallery;
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

        private UILabel _galleryHintLabel;

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
                Font = UIFont.SystemFontOfSize(14),
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
            _noteDescriptionTextView.Height().EqualTo(200)
            );
            #endregion

            ConfigureView();

            #region GalleryTitle
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
                _galleryHintLabel.Height().EqualTo(20f)
            );
            #endregion

            #region NoteItemsGallery
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
            #endregion

            #region BottomBar
            var bottomBar = new UIView
            {
                BackgroundColor = UIColor.FromRGB(50, 50, 50),
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            var pickImage = new UIImageView(UIImage.FromBundle(ConstantsHelper.AddIcon))
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            var cameraImage = new UIImageView(UIImage.FromBundle(ConstantsHelper.CameraIcon))
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            var videoImage = new UIImageView(UIImage.FromBundle(ConstantsHelper.VideoIcon))
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            bottomBar.AddSubviews(pickImage, cameraImage, videoImage);
            bottomBar.AddConstraints(pickImage.Height().EqualTo(30f),
                pickImage.AtLeftOf(bottomBar, 20f),
                pickImage.Width().EqualTo(30f),
                pickImage.WithSameCenterY(bottomBar),
                cameraImage.ToRightOf(pickImage, 40f),
                cameraImage.Height().EqualTo(30f),
                cameraImage.Width().EqualTo(30f),
                cameraImage.WithSameCenterY(bottomBar),
                videoImage.ToRightOf(cameraImage, 40f),
                videoImage.Height().EqualTo(30f),
                videoImage.Width().EqualTo(30f),
                videoImage.WithSameCenterY(bottomBar));

            View.AddSubview(bottomBar);
            View.AddConstraints(bottomBar.WithSameWidth(View),
                bottomBar.Height().EqualTo(70f),
                bottomBar.AtBottomOf(View));
            #endregion
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