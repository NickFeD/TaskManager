﻿namespace TaskManager.Client.WinUi.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
