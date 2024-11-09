using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels;

public partial class CreateListingViewModel : ObservableRecipient
{
    private int _currentStageIndex;
    private ObservableCollection<string> _stages;
    public CreateListingViewModel()
    {
        _stages = new ObservableCollection<string>
            {
                "SelectPropertyType",
                "SelectLocation",
                "EnterDetails",
                "UploadPhotos",
                "ReviewAndSubmit"
            };
        _currentStageIndex = 0;

        GoForwardCommand = new RelayCommand(GoForward, () => CanGoForward);
        GoBackwardCommand = new RelayCommand(GoBackward, () => CanGoBackward);
    }

    public ICommand GoForwardCommand
    {
        get;
    }
    public ICommand GoBackwardCommand
    {
        get;
    }

    public bool CanGoForward => _currentStageIndex < _stages.Count - 1;
    public bool CanGoBackward => _currentStageIndex > 0;

    public void GoForward()
    {
        if (CanGoForward)
        {
            _currentStageIndex++;
            UpdateContentFrame();
        }
    }

    public void GoBackward()
    {
        if (CanGoBackward)
        {
            _currentStageIndex--;
            UpdateContentFrame();
        }
    }

    private void UpdateContentFrame()
    {
        // Logic to update the Frame content based on _currentStageIndex
        var currentStage = _stages[_currentStageIndex];
        // Assuming ContentFrame is set through a binding or code-behind
    }
}
