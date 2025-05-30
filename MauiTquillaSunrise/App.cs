﻿namespace MauiTquillaSunrise;

public class App : Application
{
    private readonly Shell _shell;
    public App(AppShell shell)
    {
        _shell = shell;
      
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(_shell)
        {
            Width = 900,
            Height = 1000,
            Title = "Maui T-Quilla Sunrise",
        };
        return window;
    }
}
