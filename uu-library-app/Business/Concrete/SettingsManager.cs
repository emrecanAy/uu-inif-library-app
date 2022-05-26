using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Abstract;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Concrete
{
    public class SettingsManager : ISettingsService
    {
        ISettingsDal _settings;
        public SettingsManager(ISettingsDal settings)
        {
            this._settings = settings;
        }

        public void AddOrUpdate(Settings settings)
        {
            _settings.AddOrUpdate(settings);
        }

        public Settings getSettings()
        {
            return _settings.getSettings();
        }

        public void Update(Settings settings)
        {
            _settings.Update(settings);
        }
    }
}
