using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace SquadOV.ViewModels
{
    public delegate void SetupFinishedHandler();
    public class SetupWindowViewModel : ReactiveObject
    {
        public event SetupFinishedHandler? SetupFinished;

        public SetupWindowViewModel()
        {
            // Start 
        }
        public void StartLoad()
        {

        }

        protected void OnSetupFinished()
        {
            if (SetupFinished != null)
            {
                SetupFinished.Invoke();
            }
            
        }
    }
}
