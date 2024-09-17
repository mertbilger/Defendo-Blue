using System;

namespace Defendo_Blue.Models
{
    public class ScannedFileDB
    {
        public int Id { get; set; } // Otomatik artan anahtar
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsMalicious { get; set; }
        public string ScanId { get; set; }
        public DateTime ScanDate { get; set; }
    }
}
