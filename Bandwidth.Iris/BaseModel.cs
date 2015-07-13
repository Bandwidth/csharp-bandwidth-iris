namespace Bandwidth.Iris
{
    public class BaseModel
    {
        private Client _client;
        public virtual string Id { get; set; }

        internal Client Client
        {
            get
            {
#if !PCL
                if (_client == null)
                {
                    _client = Client.GetInstance();
                }
#endif
                return _client;
            }
            set { _client = value; }
        }

        public void SetClient(Client client)
        {
            _client = client;
        }
    }
}
