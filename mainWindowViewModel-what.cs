using Devices;
using GalaSoft.MvvmLight;
using MusicData;
using Services;
using SonosController.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;

namespace SonosController
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private SonosController _sonosController;
        private string _selectedRoom;
        private List<string> _roomList;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> RoomList
        {
            get { return _roomList; }
            set
            {
                _roomList = value;
                OnPropertyChanged(nameof(RoomList));
            }
        }

        public string SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        public async Task Initialize()
        {
            try
            {
                _sonosController = new SonosController();
                RoomList = await _sonosController.GetRoomNamesAsync();
                if (RoomList != null && RoomList.Count > 0)
                {
                    SelectedRoom = RoomList.First();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, for example by logging or displaying an error message to the user
                Console.WriteLine($"An error occurred while initializing the Sonos controller: {ex.Message}");
            }
        }

        public async Task Play()
        {
            try
            {
                if (_sonosController != null && !string.IsNullOrEmpty(SelectedRoom))
                {
                    await _sonosController.PlayAsync(SelectedRoom);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, for example by logging or displaying an error message to the user
                Console.WriteLine($"An error occurred while playing in the {SelectedRoom} room: {ex.Message}");
            }
        }

        public async Task Pause()
        {
            try
            {
                if (_sonosController != null && !string.IsNullOrEmpty(SelectedRoom))
                {
                    await _sonosController.PauseAsync(SelectedRoom);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, for example by logging or displaying an error message to the user
                Console.WriteLine($"An error occurred while pausing in the {SelectedRoom} room: {ex.Message}");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
