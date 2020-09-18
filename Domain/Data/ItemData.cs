using System;

namespace Domain.Data
{
    public class ItemData
    {
        public enum ContainerType { Inventory, Equipment }
        public Guid Id { get; set; }
        public int TypeReferenceId { get; set; }
        public int SubTypeReferenceId { get; set; }
        public ContainerType Container { get; set; }
    }
}