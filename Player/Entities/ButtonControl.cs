using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Entities
{
    class ButtonControl
    {
        public  bool PlayStatus { get;  set; }
        
        public ButtonControl()
        {

        }

        public ButtonControl(bool playStatus)
        {
            PlayStatus = playStatus;
        }

        public  bool UpdatePlayStatus()
        {
           PlayStatus = PlayStatus == true ?  false :  true;
            return PlayStatus;
        }
    
    
    }
}
