namespace homework8
{
    internal class Connection
    {
        public string Server { get; init; }
        public string Database { get; init; }
        public string Login { get; init; }
        public string Password { get; init; }

        public override string ToString()
        {
            return $"Data source={Server}; initial catalog={Database}; user id={Login}; password={Password}";
        }
    }
}