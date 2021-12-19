using System;

namespace FileTransferApi.Provider
{
    public abstract class ProviderConfiguration
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        protected ProviderConfiguration(string id, string name, string description)
        {
            Guid guid;
            Id = Guid.TryParse(id, out guid) ? id : throw new ArgumentException("The string is not a valid GUID", nameof(id));
            Name = name ?? throw new ArgumentException("Provide a valid provider name", nameof(name));
            Description = description;
        }
    }
}
