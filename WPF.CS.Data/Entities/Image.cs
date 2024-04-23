using WPF.CS.Data.Entities.BaseEntity;

namespace WPF.CS.Data.Entities
{
    public class Image : Entity<Guid>
    {
        public byte[] Data { get; set; }

        public string FileName { get; set; }

        public string Text { get; set; }
    }
}