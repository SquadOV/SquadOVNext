//
//  Copyright (C) 2022 Michael Bao
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SquadOV.Views.Dialogs
{
    public partial class ProfilePictureChooser : ReactiveWindow<ViewModels.Dialogs.ProfilePictureChooserViewModel>
    {
        public ProfilePictureChooser()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                ViewModel!.CancelCommand.Subscribe(v =>
                {
                    Close(null);
                }).DisposeWith(disposables);

                ViewModel!.SaveCommand.Subscribe(v =>
                {
                    Close(ViewModel!.Picture);
                }).DisposeWith(disposables);

                ViewModel!.SelectProfilePictureFilesystemInteraction.RegisterHandler(ShowSelectProfilePictureDialog).DisposeWith(disposables);
            });

            AddHandler(DragDrop.DropEvent, DropHandler);
            AddHandler(DragDrop.DragOverEvent, DragOverHandler);
        }

        private async Task ShowSelectProfilePictureDialog(InteractionContext<Unit, string?> interaction)
        {
            var dialog = new OpenFileDialog()
            {
                AllowMultiple = false,
                Filters = new List<FileDialogFilter>()
                {
                    new FileDialogFilter()
                    {
                        Extensions = new List<string>()
                        {
                            "bmp",
                            "png",
                            "jpg",
                            "jpeg"
                        },
                        Name = "Images",
                    }
                },
            };

            var files = await dialog.ShowAsync(((IClassicDesktopStyleApplicationLifetime)Application.Current!.ApplicationLifetime!).MainWindow);
            if (files == null || files.Length == 0)
            {
                interaction.SetOutput(null);
            }
            else
            {
                interaction.SetOutput(files[0]);
            }
            
        }

        private void DragOverHandler(object? sender, DragEventArgs args)
        {
            if (!args.Data.Contains(DataFormats.FileNames))
            {
                args.DragEffects = DragDropEffects.None;
            }
            else
            {
                args.DragEffects = DragDropEffects.Copy;
            }
        }

        private void DropHandler(object? sender, DragEventArgs args)
        {
            if (!args.Data.Contains(DataFormats.FileNames))
            {
                return;
            }

            args.DragEffects = args.DragEffects & DragDropEffects.Copy;
            ViewModel!.ChangePictureFromFilename(args.Data.GetFileNames()!.First());
        }
    }
}
