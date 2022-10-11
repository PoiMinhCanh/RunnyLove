namespace Assets.Scripts.Interface
{
    internal interface IDameSender
    {
        public void SendDame(IDameReceiver dameReceiver);

        public void calculateDame();
    }
}
