using System;
using System.Collections.Generic;

namespace Assets.Scripts.Interface
{
    internal interface IDameReceiver
    {
        public void receive<T>(IDameSender dameSender) where T: IDameSender;

        public List<Type> ListAcceptedSender();
        
    }
}
