﻿namespace MauiTquillaSunrise.Pages;
public class MainPage : BasePage<MainViewModel>
{
    public MainPage(MainViewModel vm) : base(vm)
    {
        Title = vm.Title;
        this.Behaviors(new EventToCommandBehavior()
        {
            EventName = nameof(this.Loaded),
            Command = vm.PageLoadedCommand
        });
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }

    protected override void Build()
    {
        Content = new ScrollView()
        {
            BackgroundColor = ResourceColors.TquillaBackground,
            Content = new Grid()
            {
                RowDefinitions = Rows.Define(
                    (Row.UserEntryControls, Star),
                    (Row.UserServerControls, Star),
                    (Row.CollectionView, Star)),

                RowSpacing = 8,
                Padding = 8,
                Margin = 8,

                Children =
                {
                    new UserEntryControls()
                    .Row(Row.UserEntryControls),

                    new UserServerControls()
                        .Row(Row.UserServerControls),

                    new CustomBorder()
                    {
                            Padding = 20,
                            Content = new Grid()
                            {
                                ColumnDefinitions = Columns.Define(
                                (Column.CollectionView, Auto),
                                (Column.RemoveButton, Auto)),
                        
                                Children = 
                                {
                                    new CollectionView()
                                    {
                                        SelectionMode = SelectionMode.Multiple,
                                        MaximumWidthRequest = 250,
                                    }
                                        .Assign(out CollectionView serverCv)
                                        .Column(Column.CollectionView)
                                        .EmptyView("Vault currently empty")
                                        .Top()
                                        .Center()
                                        .Bind(CollectionView.ItemsSourceProperty, getter:(MainViewModel vm) => vm.Servers)
                                        .Bind(CollectionView.SelectionChangedCommandParameterProperty, source:serverCv)
                                        .Bind(CollectionView.SelectionChangedCommandProperty, getter:(MainViewModel vm) => vm.ServerSelectedCommand)
                                        .ItemTemplate(new ServerModelDataTemplate()),

                                    new CustomImageButton()
                                        .Column(Column.RemoveButton)
                                        .Center()
                                        .Margin(new Thickness(5, 0, 0, 0))
                                        .Bind(ImageButton.SourceProperty, getter:(MainViewModel vm) => vm.RemoveServerIcon)
                                        .Bind(ImageButton.IsVisibleProperty, getter: (MainViewModel vm) => vm.IsServerSelected)
                                        .Bind(ImageButton.CommandProperty, getter: (MainViewModel vm) => vm.RemoveServerCommand)
                                }
                            }.Center()
                    }
                    .Row(Row.CollectionView)
                    .Center()
                    .Fill()
                }
            }.Top()
        }.Bind(ScrollView.IsEnabledProperty, getter:(MainViewModel vm) => vm.EnableControls);

    }
    private enum Row { UserEntryControls, UserServerControls, CollectionView}
    private enum Column { CollectionView, RemoveButton }
    //private enum Row { UsernameButton, PasswordButton, ServerCount, Picker, ServerCollectionView }
    //private enum Column { EntryLabels, AddHideButtons, ServerAddRemoveUpdateButtons, AddButton }
}
