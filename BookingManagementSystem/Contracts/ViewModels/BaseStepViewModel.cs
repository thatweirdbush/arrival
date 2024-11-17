using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.Contracts.ViewModels;
public abstract partial class BaseStepViewModel : ObservableObject
{
    [ObservableProperty]
    public bool isStepCompleted = false;
    public abstract void ValidateProcess();
    public abstract void SaveProcess();
}
