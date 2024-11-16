using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.Contracts.ViewModels;
public abstract class BaseStepViewModel : ObservableObject
{
    public bool IsStepCompleted
    {
        get; protected set;
    } = false;

    public abstract void ValidateStep();
}
