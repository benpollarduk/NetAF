using NetAF.Logic;
using NetAF.Serialization.AssetSerialization;
using System;

namespace NetAF.Serialization
{
    internal class Save
    {
        public GameSerialization GameSerialization { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }

        private Save() { }

        public static Save Create(string name, Game game) 
        {
            return new()
            {
                Name = name,
                CreationTime = DateTime.Now,
                GameSerialization = new(game)
            };
        }
    }
}
