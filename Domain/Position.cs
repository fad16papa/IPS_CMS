using System;

namespace Domain
{
    public class Position
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Deescription { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsEnable { get; set; }
    }
}