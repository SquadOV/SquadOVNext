using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.IO;

namespace SquadOV.Models
{
    internal abstract class BaseConfigModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // This function allows us to add new config parameters and allow users with existing configs
        // to migrate over by generating the default values properly.
        public void FillInMissing(BaseConfigModel from)
        {
            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                var realType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (realType.IsSubclassOf(typeof(BaseConfigModel)))
                {
                    var propFrom = (BaseConfigModel?)prop.GetValue(from);
                    if (propFrom != null)
                    {
                        ((BaseConfigModel?)prop.GetValue(this))?.FillInMissing(propFrom);
                    }
                }
                else
                {
                    var selfValue = prop.GetValue(this);
                    var refValue = prop.GetValue(from);

                    if (refValue != null && selfValue == null)
                    {
                        prop.SetValue(this, refValue);
                    }
                }
            }
        }
    }

    internal class CoreConfigModel: BaseConfigModel
    {
        private string? _databasePath;
        public string? DatabasePath
        {
            get => _databasePath;
            set
            {
                _databasePath = value;
                NotifyPropertyChanged("DatabasePath");
            }
        }

        private string? _culture;
        public string? Culture
        {
            get => _culture;
            set
            {
                _culture = value;
                NotifyPropertyChanged("Culture");
            }
        }

        public static CoreConfigModel CreateDefault(string location)
        {
            return new CoreConfigModel()
            {
                DatabasePath = Path.Combine(location, "Database"),
                Culture = "en",
            };
        }
    }

    internal class ConfigModel: BaseConfigModel
    {
        public static ConfigModel CreateDefault(string location)
        {
            return new ConfigModel()
            {
                Core = CoreConfigModel.CreateDefault(location),
            };
        }

        private CoreConfigModel? _core;
        public CoreConfigModel? Core
        {
            get => _core;
            private set
            {
                if (_core != null)
                {
                    _core.PropertyChanged -= CoreChanged;
                }

                _core = value;

                if (_core != null)
                {
                    _core.PropertyChanged += CoreChanged;
                }

                void CoreChanged(object? sender, PropertyChangedEventArgs args) => NotifyPropertyChanged("Core");
                NotifyPropertyChanged("Core");
            }
        }
    }
}
