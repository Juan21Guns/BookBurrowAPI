using System.Collections;

namespace BookBurrowAPI.Models.GoogleBooksApi
{
    public class GBAModels
    {
        public int totalItems { get; set; }
        public List<GBAModel2>? items { get; set; }
    }
    public class GBAModel2
    {
        public string? id { get; set; }
        public string? selfLink { get; set; }
        public VolumeInfo? volumeInfo { get; set; }

    }
    public class VolumeInfo
    {
        public string? title { get; set; }
        public string? subtitle { get; set; }
        public List<string>? authors { get; set; }
        public string? publisher { get; set; }
        public string? publishedDate { get; set; }
        public string? description { get; set; }
        public List<IndustryIdentifiers>? industryIdentifiers { get; set; }
        public int pageCount { get; set; }
        public ImageLinks? imageLinks { get; set; }
        public string? previewLink {  get; set; }
    }
    public class IndustryIdentifiers
    {
        public string? type { get; set; }
        public string? identifier { get; set; }
    }
    public class ImageLinks
    {
        public string? smallThumbnail { get; set; }
        public string? thumbnail { get; set; }
        public string? large { get; set; }
        public string? extraLarge { get; set; }
    }
}
